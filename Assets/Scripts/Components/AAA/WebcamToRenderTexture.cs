using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamToRenderTexture : MonoBehaviour
{
    [SerializeField]
    private Material _newMaterial = null;    
    [SerializeField]
    private Renderer _renderer = null;
    [SerializeField]
    private Vector3 _rotationAfterInit = Vector3.zero;

    private void Awake()
    {
        if (_newMaterial == null)
        {
             Debug.LogError("[WebcamToRenderTexture.Awake] ERROR: Serializable _newMaterial not set");
            return;
        }
        if (_renderer == null)
        {
             Debug.LogError("[WebcamToRenderTexture.Awake] ERROR: Serializable _renderer not set");
            return;
        }        
        Application.RequestUserAuthorization(UserAuthorization.WebCam);
    }

    private void Start()
    {
    }

    public void InitCam()
    {
        WebCamTexture webcamTexture = new WebCamTexture(1280,720,30);
        _renderer.material = _newMaterial;
        _renderer.material.mainTexture = webcamTexture;
        _renderer.gameObject.transform.localEulerAngles = _rotationAfterInit;
        webcamTexture.Play();
    }
}
