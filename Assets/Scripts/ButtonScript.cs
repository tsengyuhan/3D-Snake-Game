using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject InforImg;
    public GameObject CloseBtn;

    void Start()
    {
        InforImg.SetActive(false);
    }
    public void OpenInfo()
    {
        InforImg.SetActive(true);

    }

    public void CloseInfo()
    {
        InforImg.SetActive(false);
    }
}
