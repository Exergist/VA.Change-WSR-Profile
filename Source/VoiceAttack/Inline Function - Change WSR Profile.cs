//References:
  //https://stackoverflow.com/questions/13719579/equivalent-code-of-createobject-in-c-sharp
  //https://msdn.microsoft.com/en-us/library/ee125457(v=vs.85).aspx
  //http://microsoft.public.speech-tech.narkive.com/QryVJKhs/switch-sapi-profiles-in-c
  //https://www.codeproject.com/Questions/699504/Where-i-can-get-this-interop-speechlib-dll-file-in

//Change Windows Speech Recognition profile using C# inline function that leverages Interop.SpeechLib.dll
//Requires Interop.SpeechLib.dll be placed in the root VA folder (same location as VA's executable file)

using System;
using System.Windows.Forms;
using SpeechLib;

public class VAInline
{
	public static SpInprocRecognizer InProcRecognizer;
	public static ISpeechObjectTokens Recognizer;

	public void main()
	{
		//Initialize variables
		int i = 0;
		SpObjectToken TokenObject = default(SpObjectToken);
		SpObjectToken CurrentProfile = default(SpObjectToken);
		string result = "";
		string ProfileList = "";
		string[] myProfiles;
		string ActivatedProfile = VA.GetText("ActivatedProfile"); //Profile name that user wants to activate, passed from VA command

		//Get all speech recognition profile information
		Type RecogType = Type.GetTypeFromProgID("SAPI.SpInProcRecognizer");
		dynamic InProcRecognizer = Activator.CreateInstance(RecogType);
		CurrentProfile = InProcRecognizer.Profile;
		Recognizer = InProcRecognizer.GetProfiles;

		//Store current speech recognition profile name in VA text variable 'CurrentProfile'
		VA.SetText("CurrentProfile", CurrentProfile.GetDescription().ToString());

		//Store all speech recognition profile names in VA text variable 'ProfileList'
		myProfiles = new string[Recognizer.Count];
		for (i = 0; i <= Recognizer.Count - 1; i++)
		{
			TokenObject = Recognizer.Item(i);
			myProfiles[i] = TokenObject.GetDescription().ToString();
			ProfileList += myProfiles[i] + "  ";
		}
		VA.SetText("ProfileList", ProfileList);

		//Switch speech recognition profile to name stored in VA text variable 'ActivatedProfile'
		if (ActivatedProfile == CurrentProfile.GetDescription())
			result = "Requested profile (" + ActivatedProfile + ") already installed";
		else
		{
			for (i = 0; i <= Recognizer.Count - 1; i++)
			{
				TokenObject = Recognizer.Item(i);
				if (ActivatedProfile == TokenObject.GetDescription())
				{
					TokenObject.Category.Default = TokenObject.Id;
					try
					{
						InProcRecognizer.Profile = TokenObject;
						result = "New profile installed: " + InProcRecognizer.Profile.GetDescription;
					}
					catch (Exception ex)
					{
						MessageBox.Show("Error in switching profiles." + "Error message = " + ex.Message, "ERROR");
					}
					break;
				}
				else
					result = "Requested profile (" + ActivatedProfile + ") not found. Profile was not changed.";
			}
		}

		//Store result of profile switch in VA text variable 'ProfileChangeResult'
		VA.SetText("ProfileChangeResult", result);
	}
}