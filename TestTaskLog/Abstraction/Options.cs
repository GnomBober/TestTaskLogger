
namespace TestTaskLog.Abstraction
{
    internal class Options
    {
        public string LogFilePath { get; set; }
        public string OutputFilePath { get; set; }
        public string AddressStart { get; set; }

        public int AddressMask = 32;
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
    }
}
