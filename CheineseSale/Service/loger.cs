using Microsoft.Extensions.Logging;
using System;

using System.IO;

namespace CheineseSale.Service
{
    public class loger:ILoger
    {
        private string logFilePath;

        public loger(string filePath)
        {
            logFilePath = filePath;
        }

        public void Log(string message)
        {
            WriteLog($"INFO: {message}");
        }

        public void LogError(string message)
        {
            WriteLog($"ERROR: {message}");
        }

        public void LogWarning(string message)
        {
            WriteLog($"WARNING: {message}");
        }

        private void WriteLog(string message)
        {
            try
            {
                // פותחים את הקובץ לכתיבה ומוסיפים את ההודעה ללוג
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
                }
            }
            catch (Exception ex)
            {
                // במידה ויש שגיאה בכתיבה ללוג, נוכל להדפיס את השגיאה למסך
                Console.WriteLine($"Error logging message: {ex.Message}");
            }
        }
    }
}
