using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneManager : MonoBehaviour
{
    [SerializeField]
    private Animator _animator = null;

    private void Start()
    {
        StartCoroutine(InitCoroutine());
    }

    private IEnumerator InitCoroutine()
    {
        _animator.enabled = true;
        yield return null;
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        UtilSound.instance.PlaySound("hit1");
        yield return new WaitForSeconds(0.06f);
        UtilSound.instance.PlaySound("hurt2");
        yield return new WaitForSeconds(0.3f);        
        UtilSound.instance.PlaySound("hurt4");
        yield return new WaitForSeconds(0.5f);
        UtilSound.instance.PlaySound("hurt5");
        yield return new WaitForSeconds(0.6f);
        UtilSound.instance.PlaySound("hurt1");
        yield return new WaitForSeconds(0.8f);        
        UtilSound.instance.PlaySound("hurt5");
        yield return new WaitForSeconds(0.5f);
        UtilSound.instance.PlaySound("hurt2");        
        yield return new WaitForSeconds(0.3f);
        UtilSound.instance.PlaySound("hurt4");
        yield return new WaitForSeconds(0.6f);    
        UtilSound.instance.PlaySound("hurt5");
        yield return new WaitForSeconds(0.5f);            
        UtilSound.instance.PlaySound("hurt3");
        yield return new WaitForSeconds(5.0f);

        SceneManager.LoadScene("Mirror");
    }
}
