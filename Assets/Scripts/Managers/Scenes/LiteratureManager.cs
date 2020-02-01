using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LiteratureManager : MonoBehaviour
{
    [SerializeField]
    private ConversationManager _conversationManager = null;

    public GameObject _personaje;
    public GameObject _personajeBig;
    public List<GameObject> _pistas;
    public List<GameObject> _textInput;
    private List<bool> _pistaConocida;
    private int _numGlich = 0;
    public int _maxGlich = 5;
    private int _actualPalabrasCorrectas = 0;
    public int _maxPalabrasCorrectas = 5;
    private bool _allPistas = false;
    private int currentFrase = 0;


    private void Awake()
    {
        if (_conversationManager == null)
        {
             Debug.LogError("[LiteratureManager.Awake] ERROR: Serializable _conversationManager not set");
            return;
        }
    }

    private void Start()
    {
        _numGlich = 0;
        _pistaConocida = new List<bool>();
        for(int i = 0;i < _pistas.Count; ++i)
        {
            _pistaConocida.Add(false);
        }
        PrimerMensaje();
    }

    public void PrimerMensaje()
    {
        FindObjectOfType<ConversationManager>().SetConversation(new List<string>() {"Primer mensaje", "Bloste, una polla como un poste" });
        MostrarPersonajeBig();
    }

    private void MostrarPersonajeBig()
    {
        ChangeMouse ms = _personaje.GetComponent<ChangeMouse>();
        ms.OnMouseExit();
        _personaje.SetActive(false);
        _personajeBig.SetActive(true);
    }

    private void OcultarPersonajeBig()
    {
        _personaje.SetActive(true);
        _personajeBig.SetActive(false);
    }

    public void EndConversation()
    {
        OcultarPersonajeBig();
    }
    public void EndPartConversation()
    {
        //deactivate all
        for( int i = 0; i < _textInput.Count; ++i)
        {
            _textInput[i].SetActive(false);
        }

        if (_allPistas)
        {
            Debug.Log("loool" + currentFrase);
            //dependin the clue, we activate a different one
            if (currentFrase == 1)
            {
                _textInput[0].SetActive(true);
                _conversationManager.Block(true);
            }

            if (currentFrase == 2)
            {
                _textInput[1].SetActive(true);
                _conversationManager.Block(true);
            }
            ++currentFrase;
        }
    }

    public void PistaConocida(int index)
    {
        _pistaConocida[index] = true;
    }

    public void PalabraCorrecta(int index)
    {
        if (_allPistas)
        {
            ++_actualPalabrasCorrectas;
            Debug.Log(_actualPalabrasCorrectas);
            if (_actualPalabrasCorrectas >= _maxPalabrasCorrectas)
            {
                _conversationManager.SetConversation(new List<string>() { "Moraleja final", "eres un desgraciado", "ojala tengas que hacer un build de luces de unity" });
                Debug.Log("has acabado bien el nivel");
            }
            else
            {
                Debug.Log("pasamos conver");
                _conversationManager.NextMessage(force:true);
            }
        }
        else
        {
            Debug.Log("aunque la palabra es correcta, no tienes todas las pistas");
            PalabraIncorrecta();
        }
    }

    public void PalabraIncorrecta()
    {
        CrearGlich();
    }

    public void PulsadoElPersonaje()
    {
        MostrarPersonajeBig();

        _allPistas  = _pistaConocida.TrueForAll(b => b == true);

        if (!_allPistas)
        {
            FindObjectOfType<ConversationManager>().SetConversation(new List<string>() {"**Glich**", "No tienes todas las pistas" });
            CrearGlich();
        }
        else
        {
            currentFrase = 0;
            FindObjectOfType<ConversationManager>().SetConversation(new List<string>() { "Texto final bien, mas te vale poner siempre bloste", "Bloste", "Bloste"});
        }
    }
    
    private void CrearGlich()
    {
        Debug.Log("creamos un glich");
        ++_numGlich;
        if (_numGlich > _maxGlich)
        {
            Debug.Log("game over");
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

    }
}
