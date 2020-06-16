using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    // Name of object.
    public string name;
    // Audio source.
    [HideInInspector] public AudioSource source;
    // Audio clip.
    public AudioClip audioClip;
    // Volume and pitch attributes.
    [Range(0f, 1f)] public float volume;
    // Loop
    public bool loop;
}
