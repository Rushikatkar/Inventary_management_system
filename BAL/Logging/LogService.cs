using System;
using System.IO;
using System.Threading.Tasks;

namespace BAL.Logging
{
    public class LogService : ILogService
    {
        private readonly string _alertsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Alerts");

        public LogService()
        {
            // Ensure the Alerts folder exists
            if (!Directory.Exists(_alertsDirectory))
            {
                Directory.CreateDirectory(_alertsDirectory);
            }
        }

        public async Task LogLowInventoryAsync(string message)
        {
            try
            {
                string fileName = Path.Combine(_alertsDirectory, $"LowInventoryAlert_{DateTime.UtcNow:yyyyMMdd_HHmmss}.txt");

                // Write the message to the text file in the Alerts folder
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    await writer.WriteLineAsync($"{DateTime.UtcNow}: {message}");
                }

                Console.WriteLine($"Low inventory alert logged to file: {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while logging the alert: {ex.Message}");
            }
        }
    }
}
