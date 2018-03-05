using Axle.Verification;


namespace Axle.Text.Extensions.Encoding
{
    public static class EncodingExtensions
    {
        public static string GetString(this System.Text.Encoding encoding, byte[] bytes)
        {
            return encoding.VerifyArgument(nameof(encoding)).IsNotNull().Value
                .GetString(bytes.VerifyArgument(nameof(bytes)).IsNotNull().Value, 0, bytes.Length);
        }
    }
}
