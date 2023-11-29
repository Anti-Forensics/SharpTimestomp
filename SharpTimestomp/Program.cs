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

        private SharpTimestomp(string filePath, string dateTime)
        {
            this.filePath = filePath;
            this.dateTime = DateTime.Parse(dateTime);
        }

        private bool setModified()
        {
            try
            {
                File.SetLastWriteTime(this.filePath, this.dateTime);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            return true;
        }

        private bool setCreated()
        {
            try
            {
                File.SetCreationTime(this.filePath, this.dateTime);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            return true;
        }
        static void Main(string[] args)
        {
            string path;
            string dateTime;
            string method;

            if (File.Exists(args[0]))
            {
                Console.WriteLine($"[+] Found file: {args[0]}");
                path = args[0];
            }
            else
            {
                Console.WriteLine($"File does not exist at current location: {args[0]}");
                System.Environment.Exit(1);
            }

            path = args[0];
            dateTime = args[1];
            method = args[2];

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