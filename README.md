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
1. You will need a licensed copy of VoiceAttack to import the "Change WSR Profile.vap" file. This may be obtained at www.voiceattack.com or through [Steam](http://store.steampowered.com/app/583010/VoiceAttack/). 
2. Navigate to the [releases section](https://github.com/Exergist/VA.Change-WSR-Profile/releases) and download the most recent version. 
