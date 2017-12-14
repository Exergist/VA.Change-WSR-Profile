using System.Management;

namespace SpeechPropSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            ///VA.SetBoolean("sPropStatus", false); // (Re)Set VoiceAttack boolean variable 

            ManagementClass mngmtClass = new ManagementClass("Win32_Process"); // Create new ManagementClass instance
            foreach (ManagementObject o in mngmtClass.GetInstances()) // Loop through processes
            {
                if (o["Name"].Equals("rundll32.exe")) // rundll32.exe is the process where the Windows Speech Recognition Properties functionality resides
                {
                    // For Windows 7 the Speech Properties window is associated with rundll32.exe Command Line "C:\Windows\System32\Speech\SpeechUX\sapi.cpl speech"
                    if (o["CommandLine"].ToString().Contains(@"SpeechUX\sapi.cpl") == true)
                    {
                        ///VA.SetBoolean("sPropStatus", true); // Set VoiceAttack boolean variable 
                        return; // Stop further processing of the code
                    }                    
                }
            }
        }
    }
}
