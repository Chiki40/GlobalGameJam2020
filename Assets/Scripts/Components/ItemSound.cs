using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSound : MonoBehaviour
{
    [SerializeField]
    private string _soundName = "";

    public void PlaySound()
    {
        UtilSound.instance.PlaySound(_soundName);
    }
}
