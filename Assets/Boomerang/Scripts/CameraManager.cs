using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public void offCam()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
    public void SwitchView()
    {
        bool curStat = transform.GetChild(0).gameObject.activeSelf;
        transform.GetChild(0).gameObject.SetActive(!curStat);
    }
}
