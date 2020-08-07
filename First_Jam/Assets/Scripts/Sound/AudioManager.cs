using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] GameObject ThemeMusic, ShieldErrorSound, CollisionSound, ClockTickingSound;
    GameObject ShieldSoundInstance, CollisonInstsance, ClockTickingInstance;
    public static AudioManager instance;
    public float volume;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
   
    public void PlayThemeMusic(float volume)
    {
         ThemeMusic.GetComponent<AudioSource>().Play();
        
    }

    public void PlayShieldErrorSound(Vector3 pos)
    {
        if(ShieldSoundInstance == null)
        {
            ShieldSoundInstance = Instantiate(ShieldErrorSound, pos, Quaternion.identity);
        }
    
       
    }
    public void StopShieldErrorSound()
    {
        if(ShieldSoundInstance != null)
        {
            Destroy(ShieldSoundInstance);
        }
    
       
    }

    public void PlayCollisionSound(Vector3 pos)
    {
        if(CollisonInstsance == null)
        {
            CollisonInstsance = Instantiate(CollisionSound, pos, Quaternion.identity);

        }
       
    }
    
    public void PlayClockTickingSound(Vector3 pos)
    {
        if(ClockTickingInstance == null)                  
        {
            ClockTickingInstance = Instantiate(ClockTickingSound, pos, Quaternion.identity);
        }
     
       
    }
    public void StopClockTickingSound()
    {
        if(ClockTickingInstance != null)                  
        {
            Destroy(ClockTickingInstance);
        }
     
       
    }
}
