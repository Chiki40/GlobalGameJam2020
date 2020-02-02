using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip musicClip; 
    // Start is called before the first frame update
    void Start()
    {
        UtilSound.instance.PlaySound(musicClip.name, 1, true);
    }
}
