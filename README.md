## What is it?
A VoiceAttack profile for changing the active Windows Speech Recognition profile. Supporting documentation and source code are also provided.

## A VoiceAttack and Windows Speech Recognition Primer
### Wait, what is VoiceAttack (VA)?
*Voice-activated and macro controls for your PC games and apps.*

More information about the VoiceAttack voice control and macro creation software may be found at www.voiceattack.com. It is an amazingly powerful application that will change the way you think about gaming and interacting with your PC in general.

### Why should I care about Windows Speech Recognition (WSR) and corresponding profile switching?
WSR allows you to use your voice to control your (Windows) computer, and VA dramatically expands upon this capability. To effectively use WSR you need to have a microphone (of course) and "train" your computer to better understand you. A WSR profile contains the information from the voice training. Besides needing different profiles for different users, the effectiveness of voice recognition is impacted by your microphone and your ambient surroundings (as well as other stuff). So WSR profile switching is very important if you have multiple users who want to employ voice control and, to a lesser extent, if you want to use voice control with different microphones or within different ambient noise environments. 

<details>
	<summary>
		Click here to see a snapshot of the Windows 7 Speech Properties window (which pretty much looks identical to the same in Windows 10). You can see the option to select from multiple WSR profiles.
	</summary>
	<img src="https://github.com/Exergist/VA.Change-WSR-Profile/blob/master/Images/Windows%207%20Speech%20Properties.png" title="Windows Speech Properties" width="40%">
</details>
<br>

Here's the catch: **changing WSR profiles through the Windows interface is cumbersome. That is where VA.Change-WSR-Profile comes in.** 

## How does it work?
The VA profile "Change WSR Profile" provides two primary VA commands: 
 - Change WSR profile by voice command
 - Change WSR profile based on variable

As you might guess the first command allows the user to change WSR profiles using a voice command, and the second command allows the user to change to a specific WSR profile based on the value stored in a text variable. Both commands rely on a C# inline function that interacts with the Windows registry (with a base key path starting at HKEY_CURRENT_USER\Software\Microsoft\Speech\RecoProfiles) to gather information about the user's WSR profiles, perform the actual profile change (via registry edit), and report the results. Feel free to check out the source code to better understand what's happening "under the hood."

## How do I install it?
This is going to be wordy, but I'm trying to cater the content to all users and be thorough. 

1. According to www.voiceattack.com "VoiceAttack works with Windows 10 all the way back to Vista." So you've got to have one of those versions of Windows to even use VA. Note though that I've only tested VA.Change-WSR-Profile in Windows 7 and Windows 10. 
2. VA.Change-WSR-Profile will work with VoiceAttack v1.6.9 and later. There are currently two versions of the VoiceAttack software available: a purchasable full version and a free limited trial version (the trial version of VoiceAttack "gives you one profile with up to twenty commands"). You will need the licensed version of VoiceAttack to import the "Change WSR Profile.vap" file. The VoiceAttack software may be obtained at www.voiceattack.com (free trial and fully licensed versions) or through [Steam](http://store.steampowered.com/app/583010/VoiceAttack/) (licensed version only). I believe it would be possible to manually recreate the commands contained within the VA.Change-WSR-Profile for use with the trial version, however I will not be covering that. In my opinion the low cost for the VoiceAttack license was totally worth it. 
3. If you're unfamiliar with VoiceAttack this is a great time to check out the [VoiceAttack manual](http://voiceattack.com/VoiceAttackHelp.pdf) to acquaint yourself with the application. 
4. I'm going to assume you've already handled other VoiceAttack-related setup steps like training the voice profile, configuring your settings, etc. If you have not already done so then go read the manual so you can learn how to properly set up VoiceAttack. 
5. Navigate to the [VA.Change-WSR-Profile Releases](https://github.com/Exergist/VA.Change-WSR-Profile/releases) and download the most recent version. All you'll need to get up and running is the .vap file, but feel free to check out the source if you wish. 
6. Launch VoiceAttack (again, I'm assuming you have the licensed version), open the "More Profile Actions" tool (one of the "Profile management buttons"...read the manual if you don't know what this is), and select "Import Profile." Navigate to the folder where you downloaded the release file, select the release file (again, should be a .vap file), and confirm the selection. You should receive a popup message telling you that the profile contains actions that execute applications and/or kill processes. This is okay (explained more later), so confirm the profile import. Now you should have the profile "Change WSR Profile" available to you within VoiceAttack. The downloaded release file is no longer needed and you may delete it since the profile is now loaded in VoiceAttack's internal files. 

## How do I use it?
If you plan on using the "Change WSR profile based on variable" command you must edit the text variable "WSRActivatedProfile" to match the exact name of the WSR profile you want to switch to.   
      ![alt text](https://github.com/Exergist/VA.Change-WSR-Profile/blob/master/Images/Modify%20this%20value%20for%20variable-based%20WSR%20profile%20changing.png "Modify this value for variable-based WSR profile changing")
   - If you want to use the "Change WSR profile by voice command" then you need to edit the text contained within the brackets in the "When I say" field of the command.   
      ![alt text](https://github.com/Exergist/VA.Change-WSR-Profile/blob/master/Images/Modify%20these%20values%20for%20voice-commanded%20WSR%20profile%20changing.png "Modify these values for voice-commanded WSR profile changing")
      
   The manual calls the bracketed portion a "dynamic command section," which allows any one of the options contained within the brackets to be recognized by VoiceAttack. You just need to change the "Profile1," "Profile2," and "Profile3" text entries to exactly match the names of the profiles you want to possibly switch to using voice commands. A minimum of one profile name is needed to make the command function (and remove the brackets for this case), and I don't know if there's a maximum limit to the number of profiles options you can provide VA. So one entry would look like `Change Recognition Profile to Profile1` and 2 entries would look like `Change Recognition Profile to [Profile1; Profile2]`. Note that the last entry should NOT be followed by a semicolon when multiple options are entered or else you may have unexpected results (read the manual). Again, the second example would allow for the voiced phrases "Change Recognition Profile to Profile1" as well as "Change Recognition Profile to Profile2" to be recognizable by VoiceAttack for executing the same command. The command is coded so that VA will check what was spoken and act on the appropriate WSR profile name.

The voice or variable-based commands are used like any other VA command. However there are two important details that you need to remember:
   - **The Windows Speech Properties window (see image in the "Why should I care" section for reference) must be closed for any WSR profile changes performed by VoiceAttack to take effect.** The profile has a function that checks all running processes to determine if the Speech Properties window is open and subsequently terminates the command if the window is found (with no profile change taking place). Basically the registry changes don't get saved properly if the Speech Properties window is open when changes are attempted. 
   - **For the WSR profile change to be recognized by VoiceAttack you need to perform the WSR profile change and then reset the active VoiceAttack profile (which will be done automatically for you in v2.2.0), switch VoiceAttack profiles, or restart VoiceAttack.** For the purposes of discussion these three actions effectively result in the active profile resetting. Every time the profile is reset VA instantiates an instance of a speech engine object and holds on to that object. That instance is based on settings at the time of instantiation. The speech object is only destroyed when the profile resets, so any of the above three actions are needed for the new WSR profile to be recognized by VA.
   Prior to VA.Change-WSR-Profile v2.2.0, VoiceAttack would restart itself in order to recognize the WSR profile change. VoiceAttack v1.7 introduced an action for resetting the active profile, so the reset action replaced the restart action in v2.2.0 of VA.Change-WSR-Profile. 
## Can I have more details about how the commands and functions work?
Here is the general order of operations for what happens when you attempt to switch profiles:
   1. Activate the voice or variable-based command to initiate a WSR profile switch.
   2. VoiceAttack obtains the desired profile name via the spoken command or from within the text variable (depending on which command type you used).
   3. Function for processing the profile switch initiates:
      1. Check for presence of Speech Properties Window and terminate command if the window is found.
      2. Perform actual WSR profile data retrieval and attempt to switch WSR profiles. As previously mentioned this is done through Windows registry queries and edits. 
      3. If an error is encountered then output the detail to the VA event log and terminate the command.
      4. If no error is encountered then output WSR profile data and change status, and (if the WSR profile switch was successful) initiate function to restart VA. 
   4. The VoiceAttack restart function then performs the following:
      1. Create a folder within the VoiceAttack's "Apps" folder for housing the batch file for restarting VA. This process will only occur if the folder doesn't already exist.
      2. Create the batch file for restarting VoiceAttack. Again, this will only occur if the batch file doesn't already exist.
      3. Batch file runs.
      4. VoiceAttack closes itself.
      5. Batch file process detects closure of VA and subsequently restarts VA.
   5. VoiceAttack automatically runs the "Profile Start" command and outputs final WSR profile change detail to the VoiceAttack event log.

## How do I know the profile and associated commands are working?
You can confirm that the WSR profile change is working by performing the following:
   1. Open the Speech Properties window and note which WSR profile is activated. Then close the window. 
   2. Perform the WSR profile change with VA, receive initial confirmation from VA that the switch was successful, VA should restart, then VA should output final confirmation that the switch is complete.
   3. Reopen the Speech Properties window and confirm (from Windows' point of view) that the switch was successful. In other words, the profile you activated should now have a checkmark next to its name. 

## Help I have issues!
First and foremost, **[read the VoiceAttack manual](http://voiceattack.com/VoiceAttackHelp.pdf)**. Yes it's long, but it covers most of what is needed for you to understand and use VoiceAttack. Plus it covers more advanced stuff which is great to know so you can do other super cool things. 

If you are having problems with the VA software itself there is a dedicated [VoiceAttack User Forum](http://voiceattack.com/SMF/index.php) where you may seek help. The community is active and full of dedicated users who will help you with your general VA issues. Plus it's a great place to learn more about VA so you can do other super cool things as well as check out profiles and commands that other users have shared. 

If you are having problems specifically with the VA.Change-WSR-Profile commands or code then head over to the VoiceAttack User Forum, check out the "Issues" section to get an idea for how to provide enough information to request assistance, and then post to the [Change Speech Recognition Profile](https://voiceattack.com/SMF/index.php?topic=1660.0) thread within the "Inline Functions" section of the forum.

## Full Disclosure
I am one of the moderators on the VoiceAttack User Forums, and I receive no benefits from the use or promotion of the VoiceAttack software. 

## Thanks
Many thanks go out to Gary (VoiceAttack developer) for giving the world this amazing piece of software, as well as for providing initial feedback about VA.Change-WSR-Profile and some info about VoiceAttack's inner workings. You sir, are the man. 

Pfeil for fielding many of my VA-related questions when I first got started on the VoiceAttack User Forums as well as for sharing his method for restarting VoiceAttack.

Antaniserse and Gangrel for providing feedback during development of the beta versions. 

Additional appreciation goes out to the VoiceAttack User Forum community for helping to build a great place to learn and develop cool stuff with VoiceAttack.
