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
                    footstepSource.clip = footstepSounds[(int)GroundType.GRASS];
                    break;
            
                case GroundType.ROAD:
                    footstepSource.clip = footstepSounds[(int)GroundType.ROAD];
                    break;
            
                case GroundType.TALLGRASS:
                    footstepSource.clip = footstepSounds[(int)GroundType.TALLGRASS];
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
