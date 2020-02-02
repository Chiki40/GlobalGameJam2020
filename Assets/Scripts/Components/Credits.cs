using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
            return;
        }

        Vector3 currentPos = transform.localPosition;
        currentPos.x -= _speed * Time.deltaTime;
        transform.localPosition = currentPos;
    }    
}
