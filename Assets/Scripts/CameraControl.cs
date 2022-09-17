using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject snake;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - snake.transform.position;
        
    }

    // Update after every update in a frame
    void LateUpdate()
    {
        transform.position = snake.transform.position + offset;
           
    }
}
