# VA.Change-WSR-Profile
## What is it?
A general purpose VoiceAttack (VA) profile for changing the active Windows Speech Recognition (WSR) profile. 

## How does it work?
The VA profile "Change WSR Profile" (corresponding to "Change WSR Profile.vap") provides two VA commands: 
 - Change WSR profile by voice command
 - Change WSR profile based on variable

As you might guess the first command allows the user to change WSR profiles using a voice command, and the second command allows the user to change to a specific WSR profile based on the value stored in a text variable. Both commands rely on a C# inline function that leverages [Microsoft Speech API 5.4](https://msdn.microsoft.com/en-us/library/ee125663(v=vs.85).aspx) (Interop.SpeechLib.dll) to perform the actual profile switch. 

## How do I install it?
You will need a licensed copy of VoiceAttack to import the "Change WSR Profile.vap" file. This may be obtained at www.voiceattack.com or through [Steam](http://store.steampowered.com/app/583010/VoiceAttack/). 

## Wait, what is VoiceAttack?
More information about the VoiceAttack voice control and macro creation software may be found at www.voiceattack.com. It is an amazingly powerful application that will change the way you think about gaming and interacting with your PC in general.
