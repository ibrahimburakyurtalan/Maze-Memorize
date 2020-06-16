using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Non-visible private attributes.
    [SerializeField] private Sound[] sounds = null;
    // Public static attributes.
    public static AudioManager Instance;

    #region UNITY METHODS
    /*
     * Unity method :   Awake - Private
     * Description  :   1) 
     */
    private void Awake()
    {
        // See description 1 for information.
        DontDestroyOnLoad(gameObject);
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        // See description 2 for information.
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.audioClip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }
    }

    /*
     * Unity method :   Start - Private
     * Description  :   1) 
     */
    private void Start()
    {
        // See description 1 for information.
        Play("theme");
        if (DataManager.Instance.sound == 0)
        {
            Mute(true);
        }
        else
        {
            Mute(false);
        }
        
    }
    #endregion UNITY METHODS

    #region LOCAL METHODS
    /*
     * Local method :   Play - Public
     * Description  :   1) 
     */
    public void Play(string name)
    {
        // See description 1 for information.
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }    
    
    /*
     * Local method :   Mute - Public
     * Description  :   1) 
     */
    public void Mute(bool mute)
    {
        // See description 1 for information.
        foreach(Sound s in sounds)
        {
            s.source.mute = mute;
        }
        // See description 2 for information.
        DataManager.Instance.SetSound(mute);
    }


    #endregion LOCAL METHODS
}
