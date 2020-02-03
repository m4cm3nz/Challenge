using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Sales.App
{
    partial class Program
    {
        public class StreamService
        {
            private const int timeout = 100;
            static readonly ReaderWriterLock rwl = new ReaderWriterLock();

            public static async Task LoadLinesOf(string fileName, Action<Task<string>> processTask)
            {
                using var streamReader = new StreamReader(fileName);
                while (!streamReader.EndOfStream)
                    await streamReader.ReadLineAsync().ContinueWith(processTask);

                streamReader.Close();
            }

            public static Task WriteReport(string fileName, string content)
            {
                rwl.AcquireWriterLock(timeout);
                try
                {
                    using var streamWriter = new StreamWriter(fileName);
                    return streamWriter.WriteAsync(content);
                }
                finally
                {
                    rwl.ReleaseWriterLock();
                }
            }
        }
    }
}
