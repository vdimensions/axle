#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using Axle.Verification;

namespace Axle.Security.Extensions.SecureString
{
    /// <summary>
    /// A static class containing extension methods for the <see cref="SecureString"/> type.
    /// </summary>
    public static class SecureStringExtensions
    {
        // /// <summary>
        // /// Converts a <see cref="SecureString"/> instance to character array.
        // /// </summary>
        // /// <param name="secureString">
        // /// The <see cref="SecureString"/>
        // /// </param>
        // /// <returns></returns>
        // public static char[] ToCharArray(this System.Security.SecureString secureString)
        // {
        //     if (secureString == null)
        //     {
        //         return null;
        //     }
        //
        //     var chars = new char[secureString.Length];
        //     if (secureString.Length == 0)
        //     {
        //         return chars;
        //     }
        //
        //     // copy the SecureString data into an unmanaged char array and get a pointer to it
        //     var ptr = Marshal.SecureStringToCoTaskMemUnicode(secureString);
        //     try { } finally
        //     {
        //         try
        //         {
        //             // copy the unmanaged char array into a managed char array
        //             Marshal.Copy(ptr, chars, 0, secureString.Length);
        //         }
        //         finally
        //         {
        //             Marshal.ZeroFreeCoTaskMemUnicode(ptr);
        //         }
        //     }
        //     return chars;
        // }
        //
        // private static string MarshalToString(System.Security.SecureString sstr)
        // {
        //     if (sstr == null)
        //     {
        //         return null;
        //     }
        //     var result = string.Empty;
        //     if (sstr.Length == 0)
        //     {
        //         return result;
        //     }
        //
        //     var ptr = IntPtr.Zero;
        //     try { } finally
        //     {
        //         try
        //         {
        //             ptr = Marshal.SecureStringToGlobalAllocUnicode(sstr);
        //             result = Marshal.PtrToStringUni(ptr);
        //         }
        //         finally
        //         {
        //             if (ptr != IntPtr.Zero)
        //             {
        //                 Marshal.ZeroFreeGlobalAllocUnicode(ptr);
        //             }
        //         }
        //     }
        //     return result;
        // }
        
        #if UNSAFE
        /// <summary>
        /// Temporarily copies a <see cref="SecureString"/> data to the managed memory into a <see cref="string"/>
        /// value, and invokes the provided <paramref name="action"/> delegate with the managed copy,
        /// and then de-allocates the managed memory.
        /// </summary>
        /// <param name="secureString">
        /// The <see cref="SecureString"/> to use.
        /// </param>
        /// <param name="action">
        /// An <see cref="Action{T}"/> delegate that accepts the managed copy of the secured string.
        /// <para>
        /// It is recommended that the action code exist as quickly as possible, to prevent the sensitive string data
        /// from being present in managed memory.
        /// It is strongly undesirable to make managed copies of the passed-in string value, as the current method
        /// will be unable to deallocate them.
        /// </para>
        /// </param>
        /// <remarks>
        /// <para>
        /// Parts of this method implementation are authored by Douglas Day and released under MIT license.
        /// Below is the original copyright and the source from where the code is obtained.
        /// <list type="none">
        /// <item>
        /// <term>Original copyright note</term>
        /// <description>
        /// <para>Copyright (C) 2010 Douglas Day</para>
        /// <para>All rights reserved.</para>
        /// <para>MIT-licensed: http://www.opensource.org/licenses/mit-license.php</para>
        /// </description>
        /// </item>
        /// <item>
        /// <term>Source</term>
        /// <description>Stack Overflow</description>
        /// </item>
        /// <item>
        /// <term>URL</term>
        /// <description>https://stackoverflow.com/a/3567531/795158</description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        [SuppressMessage("ReSharper", "SuggestVarOrType_Elsewhere")]
        [SuppressMessage("ReSharper", "SuggestVarOrType_BuiltInTypes")]
        [SuppressMessage("ReSharper", "SuggestVarOrType_SimpleTypes")]
        public static unsafe void With(this System.Security.SecureString secureString, Action<string> action)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(secureString, nameof(secureString)));
            Verifier.IsNotNull(Verifier.VerifyArgument(action, nameof(action)));
            if (secureString.Length == 0)
            {
                action(string.Empty);
            }

            Exception e = null;
            
            int length = secureString.Length;
            string str = new string('\0', length);

            GCHandle gcHandle = new GCHandle();
            IntPtr stringPtr = IntPtr.Zero;
            RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(
                delegate
                {
                    // Create a CER (Constrained Execution Region)
                    RuntimeHelpers.PrepareConstrainedRegions();
                    try { }
                    finally
                    {
                        // Pin our string, disallowing the garbage collector from moving it around.
                        gcHandle = GCHandle.Alloc(str, GCHandleType.Pinned);
                    }
                    
                    RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(
                        delegate
                        {
                            // Create a CER (Constrained Execution Region)
                            RuntimeHelpers.PrepareConstrainedRegions();
                            try { }
                            finally
                            {
                                stringPtr = Marshal.SecureStringToBSTR(secureString);
                            }

                            // Copy the SecureString content to our pinned string
                            char* pString = (char*) stringPtr;
                            char* pInsecureString = (char*) gcHandle.AddrOfPinnedObject();
                            for (int index = 0; index < length; index++)
                            {
                                pInsecureString[index] = pString[index];
                            }

                            try { }
                            finally
                            {
                                try
                                {
                                    action(str);
                                }
                                catch (Exception ex)
                                {
                                    // Catch any exception thrown by the delegate.
                                    // We will rethrow it later once we tidy up the memory and GC allocations.
                                    e = ex;
                                }
                            }
                        },
                        delegate
                        {
                            if (stringPtr != IntPtr.Zero)
                            {
                                // Free the SecureString BSTR that was generated
                                Marshal.ZeroFreeBSTR(stringPtr);
                            }
                        }, 
                        null);
                },
                delegate
                {
                    if (gcHandle.IsAllocated)
                    {
                        // Zero each character of the string.
                        char* pInsecureString = (char*) gcHandle.AddrOfPinnedObject();
                        for (int index = 0; index < length; index++)
                        {
                            pInsecureString[index] = '\0';
                        }

                        // Free the handle so the garbage collector
                        // can dispose of it properly.
                        gcHandle.Free();
                    }
                },
                null);
            
            if (e != null)
            {
                // throw the exception that happened while we invoked the action.
                throw e;
            }
        }
        #endif
    }
}
#endif