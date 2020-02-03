using Sales.App.Formatting;
using Sales.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sales.App
{
    partial class Program
    {
        private const string source = "./data/in";
        private const string processed = "./data/in/processed";
        private const string salesReport = "./data/out";

        private static async Task Main(string[] args)
        {
            var parameters = args.ToList();

            if (parameters.Any(p => p == "-sample"))
                await GenerateSampleFile();
            else
                StartListenSalesDirectory();
        }

        private static void ConfigureDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private static void StartListenSalesDirectory()
        {
            ConfigureDirectory(source);
            ConfigureDirectory(processed);
            ConfigureDirectory(salesReport);

            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = "./data/in/";
                watcher.NotifyFilter = NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName;

                watcher.Created += OnChanged;
                watcher.EnableRaisingEvents = true;

                Console.WriteLine("Press 'q' to quit the sample.");
                while (Console.Read() != 'q') ;
            }
        }

        private static async Task ProcessSalesContent(string fileFullPath)
        {
            try
            {
                var fileInfo = new FileInfo(fileFullPath);
                var salesAnalises = new SalesAnalises();

                Console.WriteLine($"Reading content of {fileInfo.Name}");

                await StreamService.LoadLinesOf(fileFullPath, (task) =>
                {
                    Console.WriteLine(task.Result);
                    var notifications = salesAnalises.Process(task.Result);

                    if (notifications.HasMessages)
                        notifications.Items.ToList().ForEach(Console.WriteLine);
                });

                salesAnalises.SaveChanges();

                File.Copy(fileFullPath, Path.Combine(processed, fileInfo.Name));
                File.Delete(fileFullPath);
                Console.WriteLine($"File {fileInfo.Name} has been processed with success!");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        static readonly SimpleCountDown countDown = new SimpleCountDown();

        private static async void OnChanged(object source, FileSystemEventArgs e)
        {
            if(File.Exists(Path.Combine(processed, e.Name)))
            {
                Console.WriteLine($"File {e.Name} has alredy been processed!");
                return;
            }

            await Task.Delay(200);

            countDown.AddCount();
            await ProcessSalesContent(e.FullPath);
            countDown.Decrement();
            
            if (countDown.CurrentCount == 0)
            {
                var report = new SalesReportFormatting().Format(new SalesAnalises().Report());
                await StreamService.WriteReport("./data/out/SalesReport.txt", report);

                Console.WriteLine(report);
                Console.WriteLine("SalesReport saved in data/out directory");
                Console.WriteLine("Press 'q' to quit the sample.");
            }
        }

        private static async Task GenerateSampleFile()
        {
            Console.WriteLine(@"
            This will create 3 Samples files in data directory
            Sample01.txt contain data of Vendors, Customers and Sales
            Sample02.txt contains Sales
            Sample03.txt more Sales
            ");

            Console.WriteLine("Please inform the quantity of lines to your file.");

            var userInput = Console.ReadLine();

            if (!int.TryParse(userInput, out int linesCount))
            {
                Console.WriteLine("You should inform a number.");
                return;
            }

            Console.WriteLine("Creating 3 Sample files in data directory");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await Task.Run(() => new SampleGenerator().CreateSalesFile(linesCount));

            Console.WriteLine("Sample files whas created with success in {0}", stopwatch.Elapsed.ToString(@"mm\:ss\:ff"));

            return;
        }
    }
}
