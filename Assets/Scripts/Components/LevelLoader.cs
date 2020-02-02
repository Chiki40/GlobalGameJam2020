using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public string sceneToLoad = "";
    public void OnClick()
    {
        GameController.GetInstance().StartLevel(sceneToLoad);
    }
}
