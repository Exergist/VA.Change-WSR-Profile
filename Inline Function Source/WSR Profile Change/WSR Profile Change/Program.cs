using System;
using System.Linq;
using Microsoft.Win32;

namespace Change_WSR_Profile
{
    class Program
    {
        static void Main(string[] args)
        {
            #region OBTAIN NAME OF REQUESTED WSR PROFILE
            string ActivatedProfileName = "profile1"; // Define profile name that user wants to activate
            ///string ActivatedProfileName = VA.GetText("WSRActivatedProfile"); // Store profile name that user wants to activate, passed from VoiceAttack 
            #endregion

            #region RETRIEVE WSR PROFILE DATA FROM WINDOWS REGISTRY
            #region Perform registry queries for current WSR profile
            string RecoProfilesRegPath = @"Software\Microsoft\Speech\RecoProfiles"; // Variable for storing portion of the registry path where the Windows Speech Recognition profile data is stored
            string CurrentProfileData = ""; // Variable that will store profile data from the registry
            string CurrentProfileId = ""; // Variable that will store profile ID info from the registry
            string CurrentProfileName = ""; // Variable that will store the name of the currently activated profile
            string ProfileNameString = ""; // Variable that will store the names of all available profiles
            string ProfileChangeErrorDetail = ""; // Variable that will store error information (if applicable)
            string ChangeResult = ""; // String that will hold the result information from the (attempted) WSR profile change

            try // Attempt the following code...
            {
                using (RegistryKey RecoProfiles = Registry.CurrentUser.OpenSubKey(RecoProfilesRegPath)) // Capture registry information for the key at the specified registry path (read-only). "using" also properly disposes RegistryKey
                {
                    if (RecoProfiles != null) // Check if key is not null
                    {
                        CurrentProfileData = (string)RecoProfiles.GetValue("DefaultTokenId"); // Extract the value data associated with the "DefaultTokenId" value name, which corresponds to the ID for the current WSR profile
                        CurrentProfileId = CurrentProfileData.Substring(CurrentProfileData.IndexOf("{")).Replace("{", "").Replace("}", ""); // Extract the current WSR profile ID from the profile data and remove the bracket characters
                    }
                    else // key is null
                    {
                        ProfileChangeErrorDetail = "Error during WSR profile data retrieval: RecoProfiles key is null."; // Store error detail
                        goto OutputSection; // Jump to code line containing the corresponding label
                    }
                }
            }
            catch // Perform if "try" encounter an exception (error)
            {
                ProfileChangeErrorDetail = "General error during current WSR profile data retrieval."; // Store error detail
                goto OutputSection; // Jump to code line containing the corresponding label
            }
            #endregion

            #region Perform registry queries for all available WSR profiles
            string TokensRegPath = RecoProfilesRegPath + @"\Tokens"; // Store registry path of the Tokens folder inside of RecoProfiles
            string[] ProfileIdList; // Initialize string array for containing WSR profile IDs
            try // Attempt the following code...
            {
                using (RegistryKey Tokens = Registry.CurrentUser.OpenSubKey(TokensRegPath)) // Capture registry information for the key at the specified registry path (read-only). "using" also properly disposes RegistryKey
                {
                    if (Tokens != null) // Check if key is not null
                        ProfileIdList = Tokens.GetSubKeyNames(); // Extract list of WSR profile IDs corresponding to all WSR profiles
                    else
                    {
                        ProfileChangeErrorDetail = "Error during WSR profile data retrieval: Tokens key is null."; // Store error detail
                        goto OutputSection; // Jump to code line containing the corresponding label
                    }
                }
            }
            catch // Perform if "try" encounter an exception (error)
            {
                ProfileChangeErrorDetail = "General error during WSR profile data retrieval for all available profiles."; // Store error detail
                goto OutputSection; // Jump to code line containing the corresponding label
            }
            #endregion

            #region Identify names of current, requested (activated), and all available WSR profiles
            string ProfileIdString = ""; // Variable that will store the registry IDs of all available profiles
            string WSRProfileName = ""; // Variable for storing profile name data

            for (int i = 0; i < ProfileIdList.Count(); i++) // Loop through all profile IDs contained within ProfileIdList
            {
                ProfileIdList[i] = ProfileIdList[i].Replace("{", "").Replace("}", ""); // Remove bracket characters from ProfileIdList entries. Brackets stored in text variables appear to give VoiceAttack issues. 
                ProfileIdString += ProfileIdList[i] + "; "; // Build a string of available WSR profile names inside a single variable
                try // Attempt the following code...
                {
                    string ProfileKeyPath = TokensRegPath + @"\" + "{" + ProfileIdList[i] + "}"; // Define registry path to individual profile key
                    WSRProfileName = Registry.CurrentUser.OpenSubKey(ProfileKeyPath).GetValue(null).ToString(); // Get actual WSR profile name associated with ProfileIdList[i] from registry (read-only)
                }
                catch // Perform if "try" encounter an exception (error)
                {
                    ProfileChangeErrorDetail = "Error during WSR profile data retrieval: Issue obtaining WSRProfileName."; // Store error detail
                    goto OutputSection; // Jump to code line containing the corresponding label
                }

                if (i < ProfileIdList.Count() - 1) // Check if counter is less than the total number of profile IDs
                    ProfileNameString += WSRProfileName + "; "; // Build a list of WSR profile names inside a single string variable delimited by ";"
                else // Counter is equal to the total number of profile IDs
                    ProfileNameString += WSRProfileName; // Complete the list of WSR profile names inside a single string variable

                if (ProfileIdList[i] == CurrentProfileId) // Check if value stored in ProfileIdList[i] matches the CurrentProfileId
                {
                    CurrentProfileName = WSRProfileName; // Store the WSR CurrentProfileName
                    ///VA.SetText("WSRCurrentProfile", CurrentProfileName); // Pass the CurrentProfileName back to VoiceAttack as a text variable
                }
            }
            ///VA.SetText("WSRProfileNames", ProfileNameString); // Pass the list of available profile names back to VoiceAttack as a text variable
            #endregion
            #endregion

            #region PERFORM THE REQUESTED WSR PROFILE CHANGE
            try // Attempt the following code...
            {
                // Redefine ActivatedProfileName to ensure proper further processing and text formatting
                // Also provides means to check for ActivatedProfileName within the list of available WSR profiles
                ActivatedProfileName = ProfileNameString.Substring(ProfileNameString.IndexOf(ActivatedProfileName, StringComparison.OrdinalIgnoreCase), ActivatedProfileName.Length);
                ///VA.SetText("WSRActivatedProfile", ActivatedProfileName); // Store (correctly formatted) activated profile name in VoiceAttack text variable
            }
            catch // Perform if "try" encounter an exception (error)
            {
                // Empty catch statement
            }

            int ActivatedProfileCharIndex = ProfileNameString.IndexOf(ActivatedProfileName, StringComparison.OrdinalIgnoreCase); // Get character index of requested WSR profile from within the list (string) of available profiles (case insensitive)

            if (ActivatedProfileCharIndex >= 0) // Check if requested WSR profile is an available option
            {
                int ActivatedProfileNameIndex = ProfileNameString.Substring(0, ActivatedProfileCharIndex).Split(';').Length; // Store index of ActivatedProfileName within the ProfileNameString

                if (ActivatedProfileName == CurrentProfileName) // Check if requested WSR profile matches the current WSR profile
                    ChangeResult = "Requested profile (" + ActivatedProfileName + ") already activated"; // Store result information for case where requested profile is the current profile
                else // Requested profile is not the same as the current profile (so a profile change may be possible)
                {
                    int ProfileIdCharIndex; // Integer that will store the starting character index of the desired profile ID inside of the ProfileIdString
                    if (ActivatedProfileNameIndex == 1) // Check if ActivatedProfileNameIndex = 1
                        ProfileIdCharIndex = 0; // Set the ProfileIdCharIndex to 0
                    else // ActivatedProfileNameIndex is not 1
                    {
                        ProfileIdCharIndex = ProfileIdString.IndexOf(";"); // Get character index of first ";" in ProfileIdString
                        for (int i = 2; i < ActivatedProfileNameIndex; i++) // Loop based on the ActivatedProfileNameIndex
                        {
                            ProfileIdCharIndex = ProfileIdString.IndexOf(";", ProfileIdCharIndex + 1); // Redefine ProfileIdCharIndex to identify character index of desired profile ID within ProfileIdString
                        }
                        ProfileIdCharIndex += 2; // Add two to the index to account for the "space" and ";" characters
                    }
                    string ActivatedProfileId = ProfileIdString.Substring(ProfileIdCharIndex, ProfileIdString.IndexOf(';')); // Extract desired profile ID from ProfileIdString using the found ProfileIdCharIndex and store it

                    try // Attempt the following code...
                    {
                        using (RegistryKey WSRProfileRoot = Registry.CurrentUser.OpenSubKey(RecoProfilesRegPath, true)) // Capture registry information for the key at the specified registry path. "using" also properly disposes RegistryKey
                        {
                            string ActivatedProfileDataValue = @"HKEY_CURRENT_USER\" + RecoProfilesRegPath + @"\Tokens\" + "{" + ActivatedProfileId + "}"; // Define the registry data string corresponding to the ActivatedProfileName
                            WSRProfileRoot.SetValue("DefaultTokenId", ActivatedProfileDataValue); // Change the data value of the "DefaultTokenId" entry (inside the RecoProfiles key) in the Windows registry to ActivatedProfileDataValue, which will change the WSR profile to ActivatedProfileName
                        }
                    }
                    catch // Perform if "try" encounter an exception (error)
                    {
                        ProfileChangeErrorDetail = "General error during WSR profile change."; // Store error detail
                        goto OutputSection; // Jump to code line containing the corresponding label
                    }

                    ChangeResult = "Activated Profile = " + ActivatedProfileName; // Store result information for case where requested profile is not the same as the current profile
                }
            }
            else // Requested WSR profile is not an available option
                ChangeResult = "Requested profile (" + ActivatedProfileName + ") not found. Profile was not changed."; // Store result information for case where requested profile is not an available option
            #endregion

            #region OUTPUT INFORMATION
            OutputSection: // goto marker (destination)
            if (ProfileChangeErrorDetail != "") // Check if an error was encountered during processing (ProfileChangeErrorDetail would be non-blank)
            {
                ChangeResult = "An error occurred. " + ProfileChangeErrorDetail; // Store result information for case where an error occurred during processing
                Console.WriteLine(ChangeResult); // Output error detail to user
                ///VA.SetText("WSRActivatedProfile", null); // Set the VoiceAttack text variable to null (not set)
            }
            else
            {
                // Output data to user via the console
                Console.WriteLine("Current WSR Profile = " + CurrentProfileName);
                Console.WriteLine("WSR Profile List = " + ProfileNameString);
                Console.WriteLine(ChangeResult);
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }
            ///VA.SetText("WSRChangeResult", ChangeResult); // Pass the ChangeResult back to VoiceAttack as a text variable
            #endregion
        }
    }
}

// References:
// https://social.msdn.microsoft.com/Forums/en-US/f4feb3eb-0920-4923-83e8-6f2ef5bd4217/how-i-can-read-default-value-from-registry?forum=csharplanguage
// https://stackoverflow.com/questions/8935161/how-to-add-a-case-insensitive-option-to-array-indexof
// https://stackoverflow.com/questions/444798/case-insensitive-containsstring/444818#444818
// https://stackoverflow.com/questions/541954/how-would-you-count-occurrences-of-a-string-within-a-string