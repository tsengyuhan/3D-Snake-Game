using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBody : MonoBehaviour
{
    // Start is called before the first frame update
    SnakeContorl snakecontroler; //empty container that for SnakeControl script
    //public GameObject snake;


    // before  Start()
    void Awake()
    {
        snakecontroler = GameObject.Find("snake").GetComponent<SnakeContorl>();
        GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("he");
        if(other.gameObject.CompareTag("Body"))
        {
            //print("HE");
            int index = snakecontroler.BodyParts.IndexOf(other.gameObject);
            if(index != 0)
            {
                //print(index);
                int total = snakecontroler.BodyParts.Count;
                //print(total);
                for(int i = index; i< total-1; i++)
                {
                    if(snakecontroler.BodyParts[snakecontroler.BodyParts.Count - 1] != null){
                        Destroy(snakecontroler.BodyParts[snakecontroler.BodyParts.Count - 1]);
                        snakecontroler.BodyParts.RemoveAt(snakecontroler.BodyParts.Count - 1);
                        //print(snakecontroler.BodyParts.Count);
                    }
                    
                }
            }
            
        }
    }
}
