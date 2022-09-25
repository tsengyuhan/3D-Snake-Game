using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;



public class SnakeContorl : MonoBehaviour
{
    FloorControl floorscript;
    //CameraControl camScript;
    Animator cameraAni;
  
    public float MoveSpeed = 5.0f;
    public int SteerSpeed = 180;
    public float BodySpeed = 5.0f;
    public int Gap = 5;// the sapce between two body parts

    public float CameraSpeed = 5.0f;

    //about sound
    public AudioSource GetFood;
    public AudioSource FallDown;
    public AudioSource BackgroundMusic;
    public AudioSource GameOverBGM;
    

    public GameObject SnakeBody; 
    public GameObject SnakeHead;

    private Vector3 offset; // the offset between snake and the camera
    private Vector3 CameraEndPos;

    
    public List<GameObject> BodyParts = new List<GameObject>();

    //Save the postion of each body part
    private List<Vector3> HistoryPosition = new List<Vector3>();

    private int score = 0;
    private int toatalScore = 48;
    public TextMeshProUGUI ScoreText;

    private Rigidbody rb;
    public bool gameOver;
    private bool flag = false;
    private bool win = false;
    private int winTurnDir = 1;
    public bool start = false;

    public GameObject AllTexts;
    public TextMeshProUGUI StatusText;



    void Awake()
    {
        floorscript = GameObject.Find("Floor").GetComponent<FloorControl>();
        cameraAni = Camera.main.GetComponent<Animator>();
    }


    void Start()
    {    
        BackgroundMusic.Play();
        StatusText.text = "CATerpillar";
        //GameOverText.SetActive(false);
        rb = GetComponent<Rigidbody>();
        offset = new Vector3(0,5,-5);
        ScoreText.text = "Body:" + score + " / " + toatalScore + "m";
        ScoreText.enabled = false;
        
    }


    
    // Update is called once per frame
    void FixedUpdate()
    {

        if(rb.worldCenterOfMass.y < 4 && !flag )
        {
            gameOver = true;
            flag = true;
            StatusText.text = "GAME OVER";
            AllTexts.SetActive(true);
            FallDown.Play();
            BackgroundMusic.Stop();
            GameOverBGM.Play();
            //offset = Camera.transform.position - transform.position;
        }
        
        

        if (!gameOver  && start)
        {
            //move forward
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            //change direction
            float steerDirection = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime);
        }

        //when the game ends
        else if(gameOver)
        {
            start = false;   
            cameraAni.enabled = false;
            //var x =  Mathf.Clamp(rb.velocity.x,0f,10f);
            //limit the veliciity of the snake's head so the body can follow up
            var y =  Mathf.Clamp(rb.velocity.y,-MoveSpeed,MoveSpeed);
            //var z =  Mathf.Clamp(rb.velocity.z,0f,10f); 

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            //change direction
            float steerDirection =Random.Range(-5.0f,5.0f);
            transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime);

            //Camera.main.transform.position = transform.position + offset;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, transform.position + offset, Time.deltaTime * CameraSpeed);
            SnakeHead.transform.LookAt(Camera.main.transform);
            SnakeHead.transform.Rotate(0, 0, 180);
            foreach(var part in BodyParts)
            {
                part.transform.Rotate(0, Random.Range(-20.0f,20.0f), 180, Space.Self);
            }
            
            rb.velocity = new Vector3(rb.velocity.x, y, rb.velocity.z);
        }

        if(score == toatalScore)
        {
            win = true;
        }

        if(win)
        {
            //print("win");
            start = false;
            StatusText.text = "You're the longest CATerpillar!";
            AllTexts.SetActive(true);


            /*while(moveStep.x > 9.0f || moveStep.x < 0.0f)
            {
                moveStep = new Vector3(Random.Range(-1.0f,1.0f),0.0f,0.0f);
            }*/


            /*if(transform.position.z > 8.0f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 8.0f);
            }
            if(transform.position.z < 2.0f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 2.0f);
            }
            if(transform.position.x > 8.0f)
            {
                transform.position = new Vector3(8.0f, transform.position.y, transform.position.z);
            }
            if(transform.position.x < 2.0f)
            {
                transform.position = new Vector3(2.0f, transform.position.y, transform.position.z);
            }*/

            //change direction
            float steerDirection = Random.Range(-0.5f,-1.0f);
            int steerAng = Random.Range(90,180);
            transform.Rotate(Vector3.up * steerDirection * steerAng * Time.deltaTime);
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
        
        //save the past position
        HistoryPosition.Insert(0,transform.position);

        //move each body part
        int index = 0;
        foreach(var part in BodyParts)
        {
            Vector3 pos = HistoryPosition[(Mathf.Min(index * Gap, HistoryPosition.Count-1))];
            Vector3 moveDirection = pos - part.transform.position;
            part.transform.position += moveDirection * BodySpeed * Time.deltaTime;
            if(!gameOver)
            {
                part.transform.LookAt(pos);   
            }
            
            index++;
        }
        ScoreText.text = "Body:" + score + " / " + toatalScore + "m";
    }

    

    /*void LateUpdate()
    {
        if(gameOver)
        {
            //print("FALLLLLL DOWN NOW");
            Camera.transform.position = transform.position + offset;
        }

    }*/

    private void GrowSnake()
    {
        GameObject body = Instantiate(SnakeBody);
        BodyParts.Add(body);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            GetFood.Play();
            //Debug.Log("yes");   
            other.gameObject.SetActive(false);
            GrowSnake();
            score ++;
            if(score % 3 == 0)
            {
                MoveSpeed += 1.8f;
                BodySpeed += 1.8f;
                Gap = Mathf.RoundToInt(40/BodySpeed);
            }
            //print(score);
            
        }
    }

    void OnStart()
    {
        AllTexts.SetActive(false);
        ScoreText.enabled = true;
        if(gameOver || win)
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    
}
