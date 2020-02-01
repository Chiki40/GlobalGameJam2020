using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private string _sceneToLoad = "";
    public void OnClick()
    {
        GameController.GetInstance().StartLevel(_sceneToLoad);
    }
}
