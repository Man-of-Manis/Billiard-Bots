using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    /*
     * Anything in the environment such as a bumper just needs to call the method on the AudioManager script. More info on that at Line17
     * 
     * Anything that spawns and causes a sound (i.e an explosion), just attach an audio source to and have "play on awake" active. IF you run into issues, it would be the explosion deleting itself/going inactive.
     * 
     * Anything which would be derived from a sound needing to be played and not immediately in the environment i.e. music, a player death, item pickup, wall hits, etc... use the audio manager script.
     * 
     * 
     * 
     * 
     * to call a sound to be played, just put FindObjectOfType<AudioManager>().Play("SoundNameHere"); wherever you'd like it in the code. Make sure the object making the sound has an audio source element.
     * 
     * anything with an array of sounds all have a consistent naming convention with a number after the name. That way it can have a random number added to the end of the string that goes into the "Play" method.
     * 
     * 
     * 
     * You can add sounds to the audio manager, if you try to call a sound that is not in the array, it will throw an error. I have added all the currently existing sounds.
     * 
     * You can adjust the different attributes of sounds in the AudioManager script.
     * 
     * I Made a snapshot for the pause screen that can be called in the audio mixer if that's wanted.
     * 
     * There are groups for anything that utilizes an audio source so the volumes of environmental sounds can be changed based on a subset as well
     * 
     *
     * 
     * Used Brackeys for reference, if you have any questions let me know
     Link to video https://www.youtube.com/watch?v=6OT43pvUyfY
     */
}
