using System;


namespace Axle.Environment
{
    /***************************************\
     * ***************   OS ID  | Flavour  *
     * --------------- ---------+--------- * 
     *  Unknown         0 0 0 0 | 0 0 0 0  *
     *  All             1 1 1 1 | 0 0 0 0  *
     *[=============== ===================]*
     *  Unix            1 0 0 0 | 0 0 0 0  *
     *[=============== ===================]*
     *  Mac             0 1 0 0 | 0 0 0 0  *
     *[=============== ===================]*
     *  XBox            0 0 1 0 | 0 0 0 0  *
     *[=============== ===================]*
     *  Windows         0 0 0 1 | 0 0 0 0  *
     * --------------- ---------+--------- *
     *  WinCE           0 0 0 1 | 0 0 0 1  *
     *  Win32           0 0 0 1 | 1 0 0 0  *
     *  Win32NT         0 0 0 1 | 1 1 0 0  *
     *  Win32S          0 0 0 1 | 1 0 1 0  *
     *  Win32Windows    0 0 0 1 | 1 0 0 1  *
     *|===============-===================|*
     *[_]*********f      `\****F=*=\****[_]*
     *[__]*******i         [**]=   =[**[__]*
     *[_]********| ~~      [**]=   =[***[_]*
     *[__]*******| 8       [**\=====/**[__]*
     *[_]********|         [************[_]*
     *[__]*******\---------/***********[__]*
    \************\---------/****************/

/// <summary>
/// An enumeration of various operating system and platform identifiers, supported by an assembly. 
/// <list type="table">
///   <listheader>
///     <term>Value</term>
///     <description>Flags</description>
///   </listheader>
///   <item>
///     <term>
///       <para><see cref="Unknown"/></para>
///       <para><see cref="All"/></para>
///       <code> </code>
///     </term>
///     <description>
///       <para><b><c>0 0 0 0 </c></b><c>0 0 0 0</c></para>
///       <para><b><c>1 1 1 1 </c></b><c>0 0 0 0</c></para>
///       <para><see cref="Unix"/> | <see cref="Windows"/> | <see cref="Mac"/></para>
///     </description>
///   </item>
///   <item>
///     <term><see cref="Unix"/></term>
///     <description><b><c>1 0 0 0 </c></b><c>0 0 0 0</c></description>
///   </item>
///   <item>
///     <term><see cref="Mac"/> / <see cref="MacOS"/></term>
///     <description><b><c>0 1 0 0 </c></b><c>0 0 0 0</c></description>
///   </item>
///   <item>
///     <term><see cref="XBox"/></term>
///     <description><b><c>0 0 1 0 </c></b><c>0 0 0 0</c></description>
///   </item>
///   <item>
///     <term>
///       <para><see cref="Windows"/> / <see cref="Win"/></para>
///       <para><see cref="WinCE"/></para>
///       <para><see cref="Win32"/></para>
///       <para><see cref="Win32NT"/> / <see cref="WinNT"/></para>
///       <para><see cref="Win32S"/></para>
///       <para><see cref="Win32Windows"/></para>
///     </term>
///     <description>
///       <para><b><c>0 0 0 1 </c></b><c>0 0 0 0</c></para>
///       <para><b><c>0 0 0 1 </c></b><c>0 0 0 1</c></para>
///       <para><b><c>0 0 0 1 </c></b><c>1 0 0 0</c></para>
///       <para><b><c>0 0 0 1 </c></b><c>1 1 0 0</c></para>
///       <para><b><c>0 0 0 1 </c></b><c>1 0 1 0</c></para>
///       <para><b><c>0 0 0 1 </c></b><c>1 0 0 1</c></para>
///     </description>
///   </item>
/// </list>
/// </summary>
#if !NETSTANDARD
    [Serializable]
#endif
    [Flags]
    public enum OperatingSystemID : short
    {
        /// <summary>
        /// Used to identify unrecognized operating system. 
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Marks <b>any</b> Unix operating system. 
        /// </summary>
        Unix = 1 << 7,
        /// <summary>
        /// Identifies the Macintosh operating system. 
        /// </summary>
        MacOS = 1 << 6,
        /// <summary>
        /// Identifies the Xbox 360 platform. 
        /// </summary>
        XBox = 1 << 5,
        /// <summary>
        /// Marks <b>any</b> Windows operating system. 
        /// </summary>
        Windows = 1 << 4,
        /// <summary>
        /// Identifies a Windows CE operating system. 
        /// </summary>
        WinCE = Windows | 1,
        /// <summary>
        /// Marks <b>any</b> Win32 operating system. 
        /// </summary>
        Win32 = Windows | (1 << 3),
        /// <summary>
        /// Identifies a Win32s operating system. Win32s is a layer that runs on 16-bit versions of Windows to provide access to 32-bit applications. 
        /// </summary>
        Win32S = Win32 | (1 << 1),
        /// <summary>
        /// Identifies a Windows 95 operating system or later.
        /// </summary>
        Win32Windows = Win32 | 1,
        /// <summary>
        /// Identifies a Windows NT operating system or later. 
        /// </summary>
        Win32NT = Win32 | (1 << 2),
        /// <summary>
        /// Same as <see cref="Win32NT"/>
        /// </summary>
        /// <seealso cref="Win32NT"/>
        WinNT = Win32NT,
        /// <summary>
        /// Same as <see cref="Windows"/>
        /// </summary>
        /// <seealso cref="Windows"/>
        Win = Windows,
        /// <summary>
        /// Marks <b>any</b> Macintosh operating system. 
        /// </summary>
        Mac = MacOS,
        /// <summary>
        /// A flag combination for all operating systems represented by <see cref="OperatingSystemID">this enumeration</see>.
        /// </summary>
        All = Unix|Win|Mac,
    }
}