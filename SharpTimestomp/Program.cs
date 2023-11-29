using Microsoft.Win32.SafeHandles;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;

namespace SharpTimestomp
{
    internal class SharpTimestomp
    {
        private string filePath;
        private DateTime dateTime;

        private SharpTimestomp(string filePath, DateTime dateTime)
        {
            this.filePath = filePath;
            this.dateTime = dateTime;
        }

        private SharpTimestomp()
        {

        }

        private void setModified()
        {
            try
            {
                File.SetLastWriteTime(this.filePath, this.dateTime);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void setCreated()
        {
            try
            {
                File.SetCreationTime(this.filePath, this.dateTime);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void helpMessage()
        {
            Console.WriteLine("Usage: SharpTimestomp.exe <file path> <2/4/2022 10:00:00 AM> <created/modified>");
            System.Environment.Exit(1);
        }

        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                SharpTimestomp sts = new SharpTimestomp();
                sts.helpMessage();
            }

            string path = args[0];
            string method = args[2];
            DateTime dateTime;

            try
            {
                dateTime = DateTime.Parse(args[1]);
            }
            catch (FormatException e)
            {
                Console.WriteLine($"{e.ToString()}");
                System.Environment.Exit(1);
            }

            dateTime = DateTime.Parse(args[1]);

            if (File.Exists(path))
            {
                Console.WriteLine($"[+] Found file: {args[0]}");
            }
            else
            {
                Console.WriteLine($"File does not exist at current location: {args[0]}");
                System.Environment.Exit(1);
            }

            SharpTimestomp sharpTimestomp = new SharpTimestomp(path, dateTime);

            if (method == "modified")
            {
                Console.WriteLine($"[>>>]Setting FileModified Date/Time: {sharpTimestomp.dateTime}");
                sharpTimestomp.setModified();
            }
            
            if (method == "created")
            {
                Console.WriteLine($"[>>>] Setting FileCreated Date/Time: {sharpTimestomp.dateTime}");
                sharpTimestomp.setCreated();
            }

            Console.WriteLine($"[+] {path} - {method} date/time set successfully!");
        }
       
    }
}