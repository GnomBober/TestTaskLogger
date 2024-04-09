using TestTaskLog.Abstraction;

namespace TestTaskLog.Services
{
    internal class FileProcessingService
    {
        public static List<string> ReadFile(string path) => File.ReadAllLines(path).ToList();

        public static void RecordToFile(Options options, List<string> logEntries)
        {      
            if(options.TimeStart is null &&  options.TimeEnd is null)
            {
                Console.WriteLine("Start and end time arent set");
                return;
            }

            var filteredEntries = logEntries
                .Where(entry =>
                {
                    var parts = entry.Split(':');
                    var ipAddress = parts[0];
                    var dateTime = parts[1] + ":" + parts[2] + ":" + parts[3]; 
                    var timestamp = DateTime.ParseExact(dateTime.TrimStart(), "yyyy-MM-dd HH:mm:ss", null);

                    if(options.AddressStart is not null)
                    {
                        if(!TextProcessingService.IsIpAddressInRange(ipAddress, options.AddressStart, options.AddressMask))
                            return false;
                    }

                    return timestamp >= options.TimeStart && timestamp <= options.TimeEnd;
                })
                .GroupBy(entry => entry.Split(':')[0]) 
                .Select(group => new { Address = group.Key, Count = group.Count() })
                .ToList();

            try
            {
                using (StreamWriter writer = File.CreateText(options.OutputFilePath))
                {
                    foreach (var entry in filteredEntries)
                    {
                        writer.WriteLine($"{entry.Address}: {entry.Count}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occured while recording to file: {e.Message}");
                return;
            }

            Console.WriteLine("Analysis ended successfully. Result recorded to file");
        }
    }
}
