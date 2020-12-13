using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFootstepAudio : MonoBehaviour
{
    [SerializeField]
    AudioClip[] footstepSounds;
    
    [SerializeField]
    AudioSource footstepSource;
    
    [SerializeField]
    PlayerController player;

    [Tooltip("Variance with which to pitch footsteps up/down both ways from 1")]
    [SerializeField]
    float pitchVariance = 0.5f;

    public void PlayFootstep()
    {
        if (player.isWalking)
        {
            switch (player.groundType)
            {
                case GroundType.GRASS:
                    //Debug.Log("G");
                    footstepSource.clip = footstepSounds[0];
                    break;
            
                case GroundType.ROAD:
                    //Debug.Log("R");
                    footstepSource.clip = footstepSounds[1];
                    break;
            
                case GroundType.TALLGRASS:
                    //Debug.Log("T");
                    footstepSource.clip = footstepSounds[2];
                    break;

                default:
                    footstepSource.clip = footstepSounds[0];
                    break;
            }
        
            footstepSource.pitch = Random.Range(1.0f - pitchVariance, 1.0f + pitchVariance);
            footstepSource.Play();
        }
    }
}
