//https://stackoverflow.com/questions/17339243/display-the-devices-and-printers-window-via-c
//https://www.sevenforums.com/tutorials/66628-text-speech-shortcut-create.html

//https://stackoverflow.com/questions/28905415/how-to-get-command-line-args-of-a-process-by-id

using System;
using System.Diagnostics;
using System.Management;

namespace window_exsists
{
    static class Program
    {
        public static void Main(string[] args)
        {

            ////https://social.msdn.microsoft.com/Forums/en-US/669eeaeb-e6fa-403b-86fd-302b24c569fb/how-to-get-the-command-line-arguments-of-running-processes?forum=netfxbcl
            //ManagementClass mngmtClass = new ManagementClass("Win32_Process");
            //foreach (ManagementObject o in mngmtClass.GetInstances())
            //{
            //    if (o["Name"].Equals("rundll32.exe")) //rundll32.exe is where the Windows Speech Recognition Properties functionality resides
            //    {
            //        For Windows 7 the Speech Properties executable has Command Line that references "C:\Windows\System32\Speech\SpeechUX\sapi.cpl speech"
            //        if (o["CommandLine"].ToString().Contains("speech") == true)
            //        {
            //            Console.WriteLine(o["CommandLine"]);
            //            return;
            //        }                    
            //    }
            //}

            ////https://stackoverflow.com/questions/28905415/how-to-get-command-line-args-of-a-process-by-id
            //foreach (var process in Process.GetProcesses())
            //{
            //    if (process.ProcessName == "rundll32")
            //    {
            //        var q = string.Format("select CommandLine from Win32_Process where ProcessId='{0}'", process.Id);
            //        ManagementObjectSearcher searcher = new ManagementObjectSearcher(q);
            //        ManagementObjectCollection result = searcher.Get();
            //        foreach (ManagementObject obj in result)
            //        {
            //            if (obj["CommandLine"].ToString().Contains("speech") == true)
            //            {
            //                Console.WriteLine(obj["CommandLine"]);
            //                return;
            //            }
            //        }
            //    }
            //}

            ////https://stackoverflow.com/questions/16702073/get-processs-command-line-and-arguments-from-process-object
            //string query = "SELECT Name, CommandLine, ProcessId, Caption, ExecutablePath " + "FROM Win32_Process";
            //string wmiScope = @"\\W7USR90FYTD5L\root\cimv2";
            //ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmiScope, query);
            //foreach (ManagementObject mo in searcher.Get())
            //{
            //    string test = (string)mo["CommandLine"];
            //    if ((string)mo["CommandLine"] != null && mo["CommandLine"].ToString().Contains("speech") == true)
            //        Console.WriteLine(test + "\n");
            //}

            Console.WriteLine("Press key to continue");
            Console.ReadLine();
        }
    }
}