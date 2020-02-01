using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMovementManager : MonoBehaviour
{
    public Camera _camera;
    public float marginLeft;
    public float marginRight;
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
        if(Input.mousePosition.x <= marginLeft)
        {

        }
    }
}
