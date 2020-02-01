using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMovementManager : MonoBehaviour
{
    public Camera _camera;
    public float marginLeft = 0.1f;
    public float marginRight = 0.1f;
    public float _velocity;
    public float maxMovement;
    private float actualMovement;

    // Start is called before the first frame update
    void Start()
    {
        actualMovement = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float deltaMovement = 0;
        if(Input.mousePosition.x <= marginLeft * Screen.width)
        {
            deltaMovement -= Time.deltaTime * _velocity;
        }

        if (Input.mousePosition.x >= Screen.width - (Screen.width*marginRight))
        {
            deltaMovement += Time.deltaTime * _velocity;
        }

        actualMovement += deltaMovement;

        if (Mathf.Abs(actualMovement) >= maxMovement)
        {
            deltaMovement = 0;
        }

        Vector3 position = _camera.transform.position;
        position.x += deltaMovement;
        _camera.transform.position = position;
    }
}
