using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LiteratureManager : MonoBehaviour
{
    public GameObject _personaje;
    public GameObject _personajeBig;
    public List<GameObject> _pistas;
    private List<bool> _pistaConocida;
    private int _numGlich = 0;
    public int _maxGlich = 5;


    // Start is called before the first frame update
    void Start()
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
        FindObjectOfType<ConversationManager>().SetConversation(new List<string>() {"Primer mensaje", "BlOsTe", "UnA", "PoLlA", "CoMo", "Un", "PoStE" });
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

    public void PistaConocida(int index)
    {
        _pistaConocida[index] = true;
    }

    public void PulsadoElPersonaje()
    {
        MostrarPersonajeBig();

        bool todasLasPistas = _pistaConocida.TrueForAll(b => b == true);

        if (!todasLasPistas)
        {
            FindObjectOfType<ConversationManager>().SetConversation(new List<string>() {"Glich", "BlOsTe", "UnA", "PoLlA", "CoMo", "Un", "PoStE" });
            CrearGlich();
            ++_numGlich;

            if(_numGlich > _maxGlich)
            {
                Debug.Log("game over");
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }
        else
        {
            FindObjectOfType<ConversationManager>().SetConversation(new List<string>() { "TODO bien", "BLOSTE", "UNA", "POLLA", "COMO", "UN", "POSTE" });
            Debug.Log("has acaado bien el nivel");
        }
    }
    
    private void CrearGlich()
    {
        Debug.Log("creamos un glich");
        
    }
}
