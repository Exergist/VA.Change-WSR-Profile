//References:
  //https://social.msdn.microsoft.com/Forums/en-US/f4feb3eb-0920-4923-83e8-6f2ef5bd4217/how-i-can-read-default-value-from-registry?forum=csharplanguage
  //https://stackoverflow.com/questions/8935161/how-to-add-a-case-insensitive-option-to-array-indexof
  //https://stackoverflow.com/questions/444798/case-insensitive-containsstring/444818#444818

using System;
using System.Linq;
using Microsoft.Win32;
using System.Windows.Forms;

namespace WSR_Profile_Change
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize important variables
            string WSRProfileList = "";
            string CurrentProfileName = "";
            string ActivatedProfileName = "profile1";
            string[] ProfileIdList, ProfileNameList;
            string WSRChangeResult = "";

            /// <summary> Get information about active and available WSR profiles using Windows registry queries</summary>
            #region Retrieve WSR Profile Information from Windows Registry
            RegistryKey WSRProfileRoot = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Speech\\RecoProfiles", true); //Capture registry information for the key at the specified registry path
            string CurrentProfileData = (string)WSRProfileRoot.GetValue("DefaultTokenId"); //Extract the value data associated with the "DefaultTokenId" value name, which corresponds to the ID for the current WSR profile
            string CurrentProfileId = CurrentProfileData.Substring(CurrentProfileData.IndexOf("{")); //Extract the WSR profile ID from the value data

            RegistryKey WSRProfiles = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Speech\\RecoProfiles\\Tokens"); //Capture registry information for the key at the specified registry path
            ProfileIdList = WSRProfiles.GetSubKeyNames(); //Extract list of WSR profile IDs corresponding to all WSR profiles
            ProfileNameList = new string[ProfileIdList.Count()]; //Initialize size of ProfileNameList string array

            for (int i = 0; i < ProfileIdList.Count(); i++) //Loop through all profile IDs contained within ProfileIdList
            {
                string WSRProfileName = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Speech\\RecoProfiles\\Tokens\\" + ProfileIdList[i]).GetValue(null).ToString(); //Get actual WSR profile name associated with profileId
                ProfileNameList[i] = WSRProfileName; //Store WSRProfileName in string array
                WSRProfileList += WSRProfileName + "; "; //Build a list of WSR profile names inside a single string variable

                if (ProfileIdList[i] == CurrentProfileId) //Check if value stored in ProfileIdList[i] matches the CurrentProfileId
                    CurrentProfileName = WSRProfileName; //Store the WSR CurrentProfileName
            }
            #endregion

            if (WSRProfileList.IndexOf(ActivatedProfileName, StringComparison.OrdinalIgnoreCase) >= 0) //Check if requested WSR profile is an available option (case insensitive)
            {
                int index = Array.FindIndex(ProfileNameList, t => t.IndexOf(ActivatedProfileName, StringComparison.InvariantCultureIgnoreCase) >= 0); //Store index of ActivatedProfileName within the ProfileNameList array (case insensitive comparison)
                ActivatedProfileName = ProfileNameList[index]; //Redefine ActivatedProfileName to ensure proper further processing and text formatting
                if (ActivatedProfileName.Contains(CurrentProfileName) == true) //Check if requested WSR profile matches the current WSR profile (case sensitive)
                    WSRChangeResult = "Requested profile (" + ActivatedProfileName + ") already activated"; //Store result information
                else
                {
                    string ActivatedProfileId = ProfileIdList[index]; //Extract the ActivatedProfileId from the ProfileIdList array using the index

                    try //Attempt the below "try" code and jump to "catch" if an exception (error) is encountered
                    {
                        WSRProfileRoot.SetValue("DefaultTokenId", ActivatedProfileId); //Change the value of the "DefaultTokenId" in the Windows registry to the ActivatedProfileId, which will change the WSR profile to ActivatedProfileName
                        WSRChangeResult = "New profile installed: " + ActivatedProfileName; //Store result information
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error in switching profiles." + "Error message = " + ex.Message, "ERROR");
                    }
                }
            }
            else
                WSRChangeResult = "Requested profile (" + ActivatedProfileName + ") not found. Profile was not changed."; //Store result information

            //Output information
            Console.WriteLine("Current WSR Profile = " + CurrentProfileName);
            Console.WriteLine("WSR Profile List = " + WSRProfileList);
            Console.WriteLine(WSRChangeResult);
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
    }
}
