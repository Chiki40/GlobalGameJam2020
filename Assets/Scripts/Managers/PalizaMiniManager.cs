using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PalizaMiniManager : MonoBehaviour
{
    [SerializeField]
    private Animator _animator = null;
    [SerializeField]
    private Image _blackScreen = null;
    [SerializeField]
    private GameObject _yearBook = null;
    [SerializeField]
    private Collider2D _yearBookCollider = null;    
    [SerializeField]
    private GameObject _levelSelection = null;
    [SerializeField]
    private ConversationManager _conversationManager = null;
    public UnityEvent onPalizaEnd;

    private IEnumerator Start()
    {
        // First time
        if (GameController.GetInstance().GetLevelsCompleted() == 0)
        {       
            yield return new WaitForSeconds(1.0f);
            UtilSound.instance.PlaySound("hit1");
            yield return new WaitForSeconds(0.06f);
            UtilSound.instance.PlaySound("hurt2");
            yield return new WaitForSeconds(0.8f);
            UtilSound.instance.PlaySound("movilDiego");
            yield return new WaitForSeconds(5.0f);
            _yearBook.SetActive(true);
            _animator.enabled = true;
            _conversationManager.SetConversation(new List<string>(){"holasdfdsfdsfdsfdsfdsffdsfdsfdsfdsfsdfsdfsdfsdfsd", "pusdfsdfdsfsdftada"});
            onPalizaEnd.Invoke();
            yield return null;
            yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
            _blackScreen.gameObject.SetActive(false);
        }
        else
        {
            // Disable completed levels
            for (int i = 0; i < _levelSelection.transform.childCount; ++i)
            {
                LevelLoader levelLoader = _levelSelection.transform.GetChild(i).GetComponent<LevelLoader>();
                if (levelLoader)
                {
                    if (GameController.GetInstance().HasCompletedLevel(levelLoader.sceneToLoad))
                    {
                        levelLoader.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        // Enable check
                        levelLoader.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    }
                }
            }
            _levelSelection.SetActive(true);
            _blackScreen.gameObject.SetActive(false);
        }
    }

    public void MonologueEnd()
    {
        _yearBookCollider.enabled = true;
    }

    public void YearBookPicked()
    {
        _yearBook.GetComponent<ChangeMouse>().OnMouseExit();
        _yearBook.SetActive(false);
        _levelSelection.SetActive(true);
    }
}
