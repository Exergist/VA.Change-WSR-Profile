## What is it?
A VoiceAttack profile for changing the active Windows Speech Recognition profile. 

## A VoiceAttack and Windows Speech Recognition Primer
### Wait, what is VoiceAttack (VA)?
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
The VA profile "Change WSR Profile" (corresponding to "Change WSR Profile.vap") provides two primary VA commands: 
 - Change WSR profile by voice command
 - Change WSR profile based on variable

As you might guess the first command allows the user to change WSR profiles using a voice command, and the second command allows the user to change to a specific WSR profile based on the value stored in a text variable. Both commands rely on a C# inline function that interacts with the Windows registry (with a base key path starting at HKEY_CURRENT_USER\Software\Microsoft\Speech\RecoProfiles) to gather information about the user's WSR profiles, perform the actual profile change (via registry edit), and report the results. Feel free to check out the source code to better understand what's happening "under the hood."

## How do I install and use it?
This is going to be wordy, but I'm trying to cater the content to all users and be thorough. 

1. According to www.voiceattack.com "VoiceAttack works with Windows 10 all the way back to Vista." So you've got to have one of those versions of Windows to even use VA. Note though that I've only tested VA.Change-WSR-Profile in Windows 7 and Windows 10. 
2. There are currently two versions of the VoiceAttack software available: a purchasable full version and a free limited trial version (the trial version of VoiceAttack "gives you one profile with up to twenty commands"). You will need the licensed version of VoiceAttack to import the "Change WSR Profile.vap" file. This may be obtained at www.voiceattack.com or through [Steam](http://store.steampowered.com/app/583010/VoiceAttack/). I believe it would be possible to manually recreate the commands contained within the VA.Change-WSR-Profile for use with the trial version, however I will not be covering that. In my opinion the low cost for the VoiceAttack license was totally worth it. 
3. If you're unfamiliar with VoiceAttack this is a great time to check out the [VoiceAttack manual](http://voiceattack.com/VoiceAttackHelp.pdf) to acquaint yourself with the application. 
4. I'm going to assume you've already handled other VoiceAttack-related setup steps like training the voice profile, configuring your settings, etc. If you have not already done so then go read the manual so you can learn how to properly set up VoiceAttack. 
5. Navigate to the [VA.Change-WSR-Profile Releases](https://github.com/Exergist/VA.Change-WSR-Profile/releases) and download the most recent version. All you'll need to get up and running is the .vap file, but feel free to check out the source if you wish. 
6. Launch VoiceAttack (again, I'm assuming you have the licensed version), open the "More Profile Actions" tool (one of the "Profile management buttons"...read the manual if you don't know what this is), and select "Import Profile." Navigate to the folder where you downloaded the release file, select the release file (should be a .vap file), and confirm the selection. You should receive a popup message telling you that the profile contains actions that execute applications and/or kill processes. This is okay (explained more later), so confirm the profile import. Now you should have the profile "Change WSR Profile" available to you within VoiceAttack. The downloaded release file is no longer needed and you may delete it since the profile is now loaded in VoiceAttack's internal files. 
7. At this point all you need to do is edit the WSR profile name information inside the VA commands within the imported profile to match what is on your PC.
   - If you plan on using the "Change WSR profile based on variable" command you just have to edit the text variable "WSRActivatedProfile" to match the exact name of the WSR profile you want to switch to.   
      ![alt text](https://github.com/Exergist/VA.Change-WSR-Profile/blob/master/Images/Modify%20this%20value%20for%20variable-based%20WSR%20profile%20changing.png "Modify this value for variable-based WSR profile changing")
   - If you want to use the "Change WSR profile by voice command" then you need to edit the text contained within the brackets in the "When I say" field of the command.   
      ![alt text](https://github.com/Exergist/VA.Change-WSR-Profile/blob/master/Images/Modify%20these%20values%20for%20voice-commanded%20WSR%20profile%20changing.png "Modify these values for voice-commanded WSR profile changing")
   
   The manual calls the bracketed portion a "dynamic command section," which allows any one of the options contained within the brackets to be recognized by VoiceAttack. You just need to change the "Profile1," "Profile2," and "Profile3" text entries to exactly match the names of the profiles you want to possibly switch to using voice commands. A minimum of one profile name is needed to make the command function (and remove the brackets for this case), and I don't know if there's a maximum limit to the number of profiles options you can provide VA. So one entry would look like `Change Recognition Profile to Profile1` and 2 entries would like like `Change Recognition Profile to [Profile1; Profile2]`. Note that the last entry should NOT be followed by a semicolon when multiple options are entered (read the manual). Again, the second example would allow for the voiced phrases "Change Recognition Profile to Profile1" as well as "Change Recognition Profile to Profile2" to be recognizable by VoiceAttack for executing the same command. The command is coded so that VA will check what was actually spoken and act on the appropriate profile.
8. Either command is used like any other VA command. However there are two important details that you need to remember:
   - **The Windows Speech Properties window (see image in the "Why should I care" section for reference) must be closed for any WSR profile changes performed by VoiceAttack to take effect.** The profile has a function that checks all running processes to determine if the Speech Properties window is open and subsequently terminates the command if the window is found (with no profile change taking place). Basically the registry changes don't get saved properly when the Speech Properties window is open when changes are attempted. 
   - **For the WSR profile change to be recognized by VoiceAttack you need to perform the WSR profile change and then switch VoiceAttack profiles (restarting VoiceAttack achieves the same effect).** Every time you switch VoiceAttack profiles VA instantiates an instance of a speech engine object and holds on to that object. That instance is based on settings at the time of instantiation. The speech object is only destroyed when a profile changes, so a VA profile switch is needed in order for the new WSR profile to be recognized by VA. Restarting VoiceAttack after performing the WSR profile change also achieves the same effect, and it is easier to implement so that the WSR profile switching works "out of the box" for everyone. I chose to include code in the VA command that will create and run a batch file ("RestartVoiceAttack.bat") and then close down VoiceAttack. The batch file waits for the calling instance of VoiceAttack to close before launching a new VoiceAttack instance.
   
   You can confirm that the WSR profile change is working by:
   1. Making sure the Windows Speech Properties window is closed.
   2. Perform the WSR profile change with VA, receive initial confirmation from VA that the switch was successful, VA should restart, then VA should output final confirmation that the switch was successful.
   3. Open the Windows Speech Properties window and confirm (from Windows' point of view) that the switch was successful.
9. Profit!

## Help I have issues!
First and foremost, **[read the VoiceAttack manual](http://voiceattack.com/VoiceAttackHelp.pdf)**. Yes it's long but it covers most of what is needed for you to understand and use VoiceAttack. Plus it covers more advanced stuff which is great to know so you can do other super cool things. 

If you are having problems with the VA software itself there is a dedicated [VoiceAttack User Forum](http://voiceattack.com/SMF/index.php) where you may seek help. The community is active and full of dedicated users who will help you with your general VA issues. Plus it's a great place to learn more about VA so you can do other super cool things as well as check out profiles and commands that other users have shared. 

~If you are having problems specifically with the VA.Change-WSR-Profile commands or code then head over to the VoiceAttack User Forum, check out the "Issues" section to get an idea for how to provide enough information to request assistance, and then post to the "VA.Change-WSR-Profile" thread within the "Profiles, Commands and Plugins" section.~ Waiting on official release of VA.Change-WSR-Profile on the VoiceAttack Forum before starting to provide help specifically for VA.Change-WSR-Profile.

## Full Disclosure
I am one of the moderators on the VoiceAttack User Forums, and I receive no benefits from the use or promotion of the VoiceAttack software. 

## Thanks
Many thanks go out to Gary (VoiceAttack developer) for giving the world this amazing piece of software, as well as for providing initial feedback about VA.Change-WSR-Profile and some info about VoiceAttack's inner workings. You sir, are the man. 

Antaniserse and Gangrel for providing feedback during development of the beta versions. 

Pfeil for fielding many of my VA-related questions when I first got started on the VoiceAttack User Forums as well as for sharing his method for restarting VoiceAttack.

Additional appreciation goes out to the VoiceAttack User Forum community for helping to build a great place to learn and develop cool stuff with VoiceAttack.
