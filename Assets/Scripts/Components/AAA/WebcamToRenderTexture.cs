using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamToRenderTexture : MonoBehaviour
{
    [SerializeField]
    private Renderer _renderer = null;

    private void Awake()
    {
        if (_renderer == null)
        {
             Debug.LogError("[WebcamToRenderTexture.Awake] ERROR: Serializable _renderer not set");
            return;
        }
        Application.RequestUserAuthorization(UserAuthorization.WebCam);
    }

    void Start()
    {
        WebCamTexture webcamTexture = new WebCamTexture(1280,720,30);
        _renderer.material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }
}
