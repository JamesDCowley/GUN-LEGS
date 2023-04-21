using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    
    public Sound[] sounds;
    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        //creates singleton (same object throughout scenes)
        if(instance == null) instance = this;
        else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        //goes through all sound objects and makes them AudioSources
        foreach(Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    //plays audio
    public void Play(string name){
        Sound s =  Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            Debug.LogWarning("Sound: " + name + " not found!!");
        }
        s.source.Play();
    }
    //stops audio
    public void Stop(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null) Debug.LogWarning("Sound: " + name + " not found!!");
        s.source.Stop();
    }

    public void StopAll(){
        foreach(Sound s in sounds){
            s.source.Stop();
        }
    }

}

