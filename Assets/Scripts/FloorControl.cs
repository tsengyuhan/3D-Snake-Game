using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FloorControl : MonoBehaviour
{
    SnakeContorl snakecontroler; //empty container that for SnakeControl script
    private float RotateSpeed = 0.0005f;
    public GameObject player;
    public bool TurnFloor = false;


    private Quaternion targetRotation;

    private bool input_lock = false;




    // before  Start()
    void Awake()
    {
        snakecontroler = GameObject.Find("snake").GetComponent<SnakeContorl>();

    }

    void Update()
    {
        if(snakecontroler.gameOver){
            input_lock = true;
            //print("game over");
        }
    }

    IEnumerator Rotate(Vector3 axis, float playerRotate, int rotateStep)
    {
        input_lock = true;
        //print("lock");
        player.transform.rotation = Quaternion.AngleAxis(playerRotate, axis) * player.transform.rotation;
        for(int i=1; i<=90; i++)
        {
            transform.rotation = Quaternion.AngleAxis(rotateStep, axis) * transform.rotation;
            if(i==90)
            {
                input_lock = false;
                //print("unlock");
                TurnFloor = false;
                yield return null;
            }
            yield return new WaitForSeconds(RotateSpeed);
        }   
        
    }
    




    void OnTurnLeft()
    {
        if(!input_lock)
        {
            TurnFloor = true;
            targetRotation = Quaternion.AngleAxis(90, new Vector3(0.0f, 0.0f, 1.0f)) * transform.rotation;
            StartCoroutine(Rotate(new Vector3(0.0f, 0.0f, 1.0f), -90.0f, 1));
        }
        else
        {
            return;
        }
    }
    void OnTurnRight()
    {
        if(!input_lock)
        {
            TurnFloor = true;
            targetRotation = Quaternion.AngleAxis(-90, new Vector3(0.0f, 0.0f, 1.0f)) * transform.rotation;
            StartCoroutine(Rotate(new Vector3(0.0f, 0.0f, 1.0f), 90.0f, -1));
        }
        else
        {
            return;
        }
    }
    void OnForward()
    {
        if(!input_lock)
        {
            TurnFloor = true;
            targetRotation = Quaternion.AngleAxis(90, new Vector3(1.0f, 0.0f, 0.0f)) * transform.rotation;
            StartCoroutine(Rotate(new Vector3(1.0f, 0.0f, 0.0f), -90.0f, 1));
        }
        else
        {
            return;
        }
    }
    void OnBackward()
    {
        if(!input_lock)
        {
            TurnFloor = true;
            targetRotation = Quaternion.AngleAxis(-90, new Vector3(1.0f, 0.0f, 0.0f)) * transform.rotation;
            StartCoroutine(Rotate(new Vector3(1.0f, 0.0f, 0.0f), 90.0f, -1));
        }
        else
        {
            return;
        }
    }
}
