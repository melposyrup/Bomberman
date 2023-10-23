using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Target;
    public float Speed = 30f;

    private void Awake()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;

    }
    private void Start()
    {
        // initialize
        transform.position = new Vector3(0, 0, -2f);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    private void Update()
    {
        if (Target)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.RotateAround(Target.position, Vector3.up, -Speed + Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.RotateAround(Target.position, Vector3.up, Speed + Time.deltaTime);
            }
        }
        else { Debug.Log("target not found"); }


    }

}
