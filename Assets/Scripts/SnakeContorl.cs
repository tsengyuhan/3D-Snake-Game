using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeContorl : MonoBehaviour
{
    public int MoveSpeed = 5;
    public float SteerSpeed = 180;
    public float BodySpeed = 5;
    public int Gap = 5;// the sapce between two body parts

    public GameObject SnakeBody;
    
    private List<GameObject> BodyParts = new List<GameObject>();

    //Save the postion of each body part
    private List<Vector3> HistoryPosition = new List<Vector3>();



    // Start is called before the first frame update
    void Start()
    {
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
    }

    // Update is called once per frame
    void Update()
    {
        //move forward
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        //change direction
        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime);

        //save the past position
        HistoryPosition.Insert(0,transform.position);

        //move each body part
        int index = 0;
        foreach(var part in BodyParts)
        {
            Vector3 pos = HistoryPosition[(Mathf.Min(index * Gap, HistoryPosition.Count-1))];
            Vector3 moveDirection = pos - part.transform.position;
            part.transform.position += moveDirection * BodySpeed * Time.deltaTime;
            part.transform.LookAt(pos);
            index++;
        }
    }

    private void GrowSnake()
    {
        GameObject body = Instantiate(SnakeBody);
        BodyParts.Add(body);
    }
}
