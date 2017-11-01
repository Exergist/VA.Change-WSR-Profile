## What is it?
A VoiceAttack profile for changing the active Windows Speech Recognition profile. 

## A VoiceAttack and Windows Speech Recognition Primer
### Wait, what is VoiceAttack (VA)?
More information about the VoiceAttack voice control and macro creation software may be found at www.voiceattack.com. It is an amazingly powerful application that will change the way you think about gaming and interacting with your PC in general.

### Why should I care about Windows Speech Recognition (WSR) and corresponding profile switching?
WSR allows you to use your voice to control your (Windows) computer, and VA dramatically expands upon this capability. To effectively use WSR you need to have a microphone (of course) and "train" your computer to better understand you. A WSR profile contains the information from the voice training. Besides needing different profiles for different users, the effectiveness of voice recognition is impacted by your microphone and your ambient surroundings (as well as other stuff). So WSR profile switching is very important if you have multiple users who want to employ voice control and, to a lesser extent, if you want to use voice control with different microphones or within different ambient noise environments. 

<img src="https://github.com/Exergist/VA.Change-WSR-Profile/blob/master/Images/Windows%207%20Speech%20Properties.png" title="Windows 7 Speech Properties" width="40%">

The catch is that switching profiles through the Windows interface is cumbersome. That is where VA.Change-WSR-Profile comes in. 

## How does it work?
The VA profile "Change WSR Profile" (corresponding to "Change WSR Profile.vap") provides two VA commands: 
 - Change WSR profile by voice command
 - Change WSR profile based on variable

As you might guess the first command allows the user to change WSR profiles using a voice command, and the second command allows the user to change to a specific WSR profile based on the value stored in a text variable. Both commands rely on a C# inline function that leverages the [Microsoft Speech API 5.4](https://msdn.microsoft.com/en-us/library/ee125663(v=vs.85).aspx) (Interop.SpeechLib.dll) to perform the actual profile switch. Feel free to check out the source code to better understand what's happening "under the hood."

## How do I install it?
1. According to www.voiceattack.com "VoiceAttack works with Windows 10 all the way back to Vista." So you've got to have one of those versions of Windows to even use VA. Note though that I've only tested VA.Change-WSR-Profile in Windows 7 and Windows 10. 
2. There are currently two versions of the VoiceAttack software available: a purchasable full version and a free limited trial version (the trial version of VoiceAttack "gives you one profile with up to twenty commands"). You will need the licensed version of VoiceAttack to import the "Change WSR Profile.vap" file. This may be obtained at www.voiceattack.com or through [Steam](http://store.steampowered.com/app/583010/VoiceAttack/). I believe it would be possible to manually recreate the commands contained within the VA.Change-WSR-Profile for use with the trial version, however I will not be covering that. In my opinion the low cost for the VoiceAttack license was totally worth it. 
3. Navigate to the [releases section](https://github.com/Exergist/VA.Change-WSR-Profile/releases) and download the most recent version.

## Help I have issues!
First and foremost, **[read the VA manual](http://voiceattack.com/VoiceAttackHelp.pdf)**. Yes it's long but it covers most of what is needed for you to understand and use VoiceAttack. Plus it covers more advanced stuff which is great to know so you can do other super cool things. 

If you are having problems with the VA software itself there is a dedicated [VoiceAttack User Forum](http://voiceattack.com/SMF/index.php) where you may seek help. The community is active and full of dedicated users who will help you with your general VA issues. Plus it's a great place to learn more about VA so you can do other super cool things as well as check out profiles and commands that other users have shared. 

If you are having problems specifically with the VA.Change-WSR-Profile then head over to the VoiceAttack User Forum, check out the "Issues" section to get an idea for how to provide enough information to request assistance, and then post to the "VA.Change-WSR-Profile" thread within the "Profiles, Commands and Plugins" section. 

## Full Disclosure
I am one of the moderators on the VoiceAttack User Forums, and I receive no benefits from the use or promotion of the VoiceAttack software. 
