using System;
using System.Configuration;
using System.IO;

namespace Capstone
{
    public class Logger
    {
        public static void Log(string level, string targetSite, string message, string stackTrace = null)
        {
            StreamWriter writer = null;
            string LogPath = ConfigurationManager.AppSettings.Get("PresentationLog");
            writer = new StreamWriter(LogPath, true);
            string timeStamp = DateTime.Now.ToString();
            try
            {
                writer.WriteLine("\n[{0}]\n" +
                    "{1} - {2} - {3} - {4}",
                             timeStamp, level, targetSite, message, stackTrace);
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