using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FloorControl : MonoBehaviour
{
    public float RotateSpeed = 0.1f;
    private float final_angle_z = 0.0f;
    private float final_angle_x = 0.0f;


    public GameObject player;

    

    private Quaternion targetRotation;


    
    IEnumerator RotateLeft()
    {
        player.transform.rotation = Quaternion.AngleAxis(-90, new Vector3(0.0f, 0.0f, 1.0f)) * player.transform.rotation;
        //print(player.transform.localEulerAngles);
        while(transform.rotation.z < final_angle_z){
            transform.rotation = Quaternion.Slerp(transform.localRotation, targetRotation, RotateSpeed * Time.deltaTime);
            yield return null;
        }
        //print(player.transform.localEulerAngles);
    }

    IEnumerator RotateRight()
    {
        player.transform.rotation = Quaternion.AngleAxis(90, new Vector3(0.0f, 0.0f, 1.0f)) * player.transform.rotation;
        while(transform.rotation.z > final_angle_z){
            transform.rotation = Quaternion.Slerp(transform.localRotation, targetRotation, RotateSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Forward()
    {
        player.transform.rotation = Quaternion.AngleAxis(-90, new Vector3(1.0f, 0.0f, 0.0f)) * player.transform.rotation;
        while(transform.rotation.x < final_angle_x){
            transform.rotation = Quaternion.Slerp(transform.localRotation, targetRotation, RotateSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Backward()
    {
        player.transform.rotation = Quaternion.AngleAxis(90, new Vector3(1.0f, 0.0f, 0.0f)) * player.transform.rotation;
        while(transform.rotation.x > final_angle_x){
            transform.rotation = Quaternion.Slerp(transform.localRotation, targetRotation, RotateSpeed * Time.deltaTime);
            yield return null;
        }
    }



    void OnTurnLeft()
    {
        targetRotation = Quaternion.AngleAxis(90, new Vector3(0.0f, 0.0f, 1.0f)) * transform.rotation;
        final_angle_z = final_angle_z + 90;
        StartCoroutine("RotateLeft");
    }
    void OnTurnRight()
    {
        targetRotation = Quaternion.AngleAxis(-90, new Vector3(0.0f, 0.0f, 1.0f)) * transform.rotation;
        final_angle_z = final_angle_z - 90;
        StartCoroutine("RotateRight");
    }
    void OnForward()
    {
        targetRotation = Quaternion.AngleAxis(90, new Vector3(1.0f, 0.0f, 0.0f)) * transform.rotation;
        final_angle_x = final_angle_x + 90;
        StartCoroutine("Forward");
    }
    void OnBackward()
    {
        targetRotation = Quaternion.AngleAxis(-90, new Vector3(1.0f, 0.0f, 0.0f)) * transform.rotation;
        final_angle_x = final_angle_x - 90;
        StartCoroutine("Backward");
    }
}
