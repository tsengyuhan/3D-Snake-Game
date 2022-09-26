using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorControl : MonoBehaviour
{
    SnakeContorl snakecontroler; //empty container that for SnakeControl script
    //private float RotateSpeed = 0.0005f;
    public GameObject player;
    public bool TurnFloor = false;


    private Quaternion targetRotation;

    private Quaternion startPoint;
    private Quaternion targetPoint ;

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
        if(Input.GetKeyDown ("a"))
        {
           TurnLeft();
        }
        else if(Input.GetKeyDown ("d"))
        {
            TurnRight();
        }
        else if(Input.GetKeyDown ("w"))
        {
            Forward();
        }
        else if(Input.GetKeyDown ("s"))
        {
            Backward();
        }

    }

    IEnumerator Rotate(Vector3 axis, float playerRotate)
    {
        input_lock = true;

        startPoint = transform.rotation;
        targetPoint = Quaternion.AngleAxis(-playerRotate, axis) * startPoint;

        player.transform.rotation = Quaternion.AngleAxis(playerRotate, axis) * player.transform.rotation;
        float speed = 3.5f;
        float timeCount = 0.0f;
        while(timeCount < 1)
        {
            transform.rotation = Quaternion.Lerp(startPoint, targetPoint, timeCount);
            timeCount += Time.deltaTime * speed;
            if(timeCount >= 1)
            {
                transform.rotation = targetPoint;
                input_lock = false;
                //print("unlock");
                TurnFloor = false;
                yield return null;
            }
            yield return null;
        }
        
    }
    




    private void TurnLeft()
    {
        if(!input_lock)
        {
            TurnFloor = true;
            targetRotation = Quaternion.AngleAxis(90, new Vector3(0.0f, 0.0f, 1.0f)) * transform.rotation;
            StartCoroutine(Rotate(new Vector3(0.0f, 0.0f, 1.0f), -90.0f));
        }
        else
        {
            return;
        }
    }
    private void TurnRight()
    {
        if(!input_lock)
        {
            TurnFloor = true;
            targetRotation = Quaternion.AngleAxis(-90, new Vector3(0.0f, 0.0f, 1.0f)) * transform.rotation;
            StartCoroutine(Rotate(new Vector3(0.0f, 0.0f, 1.0f), 90.0f));
        }
        else
        {
            return;
        }
    }
    private void Forward()
    {
        if(!input_lock)
        {
            TurnFloor = true;
            targetRotation = Quaternion.AngleAxis(90, new Vector3(1.0f, 0.0f, 0.0f)) * transform.rotation;
            StartCoroutine(Rotate(new Vector3(1.0f, 0.0f, 0.0f), -90.0f));
        }
        else
        {
            return;
        }
    }
    private void Backward()
    {
        if(!input_lock)
        {
            TurnFloor = true;
            targetRotation = Quaternion.AngleAxis(-90, new Vector3(1.0f, 0.0f, 0.0f)) * transform.rotation;
            StartCoroutine(Rotate(new Vector3(1.0f, 0.0f, 0.0f), 90.0f));
        }
        else
        {
            return;
        }
    }
}
