using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSound : MonoBehaviour
{
    [SerializeField]
    private string _soundName = "";
    [SerializeField]
    private bool _random = false;

    public void PlaySound()
    {
        UtilSound.instance.PlaySound(_soundName, 1.0f, false, _random);
    }
}
