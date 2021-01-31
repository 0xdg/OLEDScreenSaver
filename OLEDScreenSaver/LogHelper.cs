using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OLEDScreenSaver
{
    public class LogHelper
    {
        public static void Log(String message)
        {
#if DEBUG
            var directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(directory + "\\log.txt", true))
            {
                file.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ": " + message);
            }
#endif
        }
    }
}
