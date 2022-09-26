using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    
    SnakeContorl snakeScript;

    private Animator CamearAnimator;
    //public bool flag = false;





    void Awake()
    {
        snakeScript = GameObject.Find("snake").GetComponent<SnakeContorl>();
        CamearAnimator = GetComponent<Animator>();
        CamearAnimator.SetTrigger("Start");

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update after every update in a frame
    void Update()
    {
        if(Input.GetKeyDown (KeyCode.Return))
        {
           MoveCamera();
        }
    }

    IEnumerator CameraMove()
    {
        CamearAnimator.SetTrigger("Play");
        return null;
    }

    void MoveCamera()
    {
        StartCoroutine("CameraMove");
        Invoke("StartGame",1);
    }

    void StartGame()
    {
        snakeScript.start = true;
    }
}
