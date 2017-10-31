//References:
  //https://stackoverflow.com/questions/13719579/equivalent-code-of-createobject-in-c-sharp
  //https://msdn.microsoft.com/en-us/library/ee125457(v=vs.85).aspx
  //http://microsoft.public.speech-tech.narkive.com/QryVJKhs/switch-sapi-profiles-in-c
  //https://www.codeproject.com/Questions/699504/Where-i-can-get-this-interop-speechlib-dll-file-in

using System;
using System.Windows.Forms;
using SpeechLib;

namespace WSR_Profile_Change
{
    class Program
    {
        public static SpInprocRecognizer InProcRecognizer;
        public static ISpeechObjectTokens theRecognizers;

        static void Main(string[] args)
        {
            //Initialize variables
            int i = 0;
            SpObjectToken TokenObject = default(SpObjectToken);
            SpObjectToken currentProfile = default(SpObjectToken);
            string result = "";
            string profileList = "";
            string[] myProfiles = null;
            string switchProfile = "three"; //Profile name that user wants to activate

            //Get all profile information
            Type RecogType = Type.GetTypeFromProgID("SAPI.SpInProcRecognizer");
            dynamic InProcRecognizer = Activator.CreateInstance(RecogType);
            //InProcRecognizer = CreateObject("SAPI.SpInProcRecognizer");
            currentProfile = InProcRecognizer.Profile;
            theRecognizers = InProcRecognizer.GetProfiles;

            //Output current profile name
            Console.WriteLine("Current Profile = " + currentProfile.GetDescription().ToString());

            //Store all profile names
            myProfiles = new string[theRecognizers.Count];
            for (i = 0; i <= theRecognizers.Count - 1; i++)
            {
                TokenObject = theRecognizers.Item(i);
                myProfiles[i] = TokenObject.GetDescription().ToString();
                profileList += myProfiles[i] + "  ";
            }
            Console.WriteLine("Available Profiles = " + profileList);

            //Switch profile
            if (switchProfile == currentProfile.GetDescription())
                result = "Requested profile already installed";
            else
            {
                for (i = 0; i <= theRecognizers.Count - 1; i++)
                {
                    TokenObject = theRecognizers.Item(i);
                    if (switchProfile == TokenObject.GetDescription())
                    {
                        TokenObject.Category.Default = TokenObject.Id;
                        try
                        {
                            InProcRecognizer.Profile = TokenObject;
                            result = "New Profile installed: " + InProcRecognizer.Profile.GetDescription;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error in switching profiles \\n" + "Error message = " + ex.Message, "ERROR");
                        }
                        break;
                    }
                    else
                    {
                        result = "Profile not found. No new profile installed.";
                    }
                }
            }

            //Output result of profile switch
            Console.WriteLine(result);
            Console.WriteLine();
            Console.Write("Press any key to continue... ");
            Console.ReadLine();
        }
    }
}





