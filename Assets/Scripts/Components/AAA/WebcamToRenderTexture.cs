using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamToRenderTexture : MonoBehaviour
{
    [SerializeField]
    private Animator _fadeOutAnimator = null;
    [SerializeField]
    private Collider2D _mirrorCollider = null;
    [SerializeField]
    private Material _newMaterial = null;    
    [SerializeField]
    private Renderer _renderer = null;
    [SerializeField]
    private Vector3 _rotationAfterInit = Vector3.zero;

    private void Awake()
    {
        if (_mirrorCollider == null)
        {
             Debug.LogError("[WebcamToRenderTexture.Awake] ERROR: Serializable _mirrorCollider not set");
            return;
        }          
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
        StartCoroutine(InitialCoroutine());
    }

    private IEnumerator InitialCoroutine()
    {
        _fadeOutAnimator.enabled = true;
        yield return null;
        yield return new WaitForSeconds(_fadeOutAnimator.GetCurrentAnimatorStateInfo(0).length);
        _mirrorCollider.enabled = true;
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
