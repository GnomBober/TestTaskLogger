
namespace TestTaskLog.Services
{
    internal class TextProcessingService
    {
        public static bool IsIpAddressInRange(string ipAddress, string addressStart, int addressMask)
        {
            var ipAddressBytes = ipAddress.Split('.').Select(byte.Parse).ToArray();
            var addressStartBytes = addressStart.Split('.').Select(byte.Parse).ToArray();

            for (int i = 0; i < 4; i++)
            {
                var maskedIpAddressByte = (byte)(ipAddressBytes[i] & (255 << (8 - addressMask)));
                var maskedAddressStartByte = (byte)(addressStartBytes[i] & (255 << (8 - addressMask)));

                if (maskedIpAddressByte != maskedAddressStartByte)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
