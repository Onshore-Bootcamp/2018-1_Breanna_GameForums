using System;
using System.Configuration;
using System.IO;

namespace CapstoneDAL.Logging
{
    class Logger
    {
        public static void Log(string level, string targetSite, string message, string stackTrace = null)
        {
            StreamWriter writer = null;
            string LogPath = ConfigurationManager.AppSettings.Get("DataAccessLog");
            writer = new StreamWriter(LogPath, true);
            string timeStamp = DateTime.Now.ToString();
            try
            {
                writer.Write("\n[{0}]\n" +
                    "{1} - {2} - {3} - {4}",
                             timeStamp, level, targetSite, message, stackTrace);
                if (stackTrace != null)
                {
                    writer.WriteLine(stackTrace);
                }
                writer.WriteLine();
            }
            catch (IOException ioEx)
            {
                Log("Fatal", ioEx.TargetSite.ToString(), ioEx.Message, ioEx.StackTrace);
                throw;
            }
            finally
            {
                writer.Close();
                writer.Dispose();
            }
        }
    }
}
