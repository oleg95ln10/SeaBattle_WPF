using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    /// <summary>
    /// Logger for quiet writting exceptions
    /// </summary>
    class QuietLogger : IDisposable
    {
        private static FileStream _fstream;

        static QuietLogger()
        {
            _fstream = new FileStream("ERROR_LOG.txt", FileMode.Append);
        }

        public static void LogQ(Exception e)
        {
            byte[] exception = new byte[e.Message.Length];
            _fstream.Write(exception, 0, exception.Length);
            Debug.WriteLine(e);
        }

        public void Dispose()
        {
            _fstream.Close();
            _fstream.Dispose();
        }
    }
}
