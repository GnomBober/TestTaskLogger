using TestTaskLog.Abstraction;
using TestTaskLog.Services;

namespace TestTaskLog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.ReadLine();
            var options = new Options();
            

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--file-log":
                        options.LogFilePath = args[i + 1];
                        break;
                    case "--file-output":
                        options.OutputFilePath = args[i + 1];
                        break;
                    case "--address-start":
                        options.AddressStart = args[i + 1];
                        break;
                    case "--address-mask":
                        options.AddressMask = int.Parse(args[i + 1]);
                        break;
                    case "--time-start":
                        options.TimeStart = DateTime.ParseExact(args[i + 1], "yyyy-MM-dd HH:mm:ss", null);
                        break;
                    case "--time-end":
                        options.TimeEnd = DateTime.ParseExact(args[i + 1], "yyyy-MM-dd HH:mm:ss", null);
                        break;
                }
            }

            if (options.LogFilePath == null || options.OutputFilePath == null)
            {
                Console.WriteLine("No required arguments");
                return;
            }

            if (options.AddressMask != 32 && options.AddressStart == null)
            {
                Console.WriteLine("No start address");
                return;
            }

            try
            {
                List<string> logEntries = new List<string>();
                logEntries = FileProcessingService.ReadFile(options.LogFilePath);

                FileProcessingService.RecordToFile(options, logEntries);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occured while reading file: {e.Message}");
                return;
            }

            
        }
    }
}