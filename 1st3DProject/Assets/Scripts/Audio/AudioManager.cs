using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public List<AudioClip> audioList = new List<AudioClip>();
    
    [HideInInspector] public Dictionary<string, AudioClip> m_audioDictionary = new Dictionary<string, AudioClip> ();


}
