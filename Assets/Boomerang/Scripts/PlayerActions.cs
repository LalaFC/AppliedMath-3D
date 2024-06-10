using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour
{
    public GameObject boomerang;
    public Transform SpawnPt;
    public static UnityEvent onPlayerShoot = new UnityEvent();
    private bool camEnabled=true;
    public Button shootBtn;
    private void Start()
    {
        boomerang = PlayerManager.instance.boomerang;
        SpawnPt = transform.GetChild(0);
    }
    public void EnableShoot()
    {
        shootBtn.interactable= true;
    }
    public void DisableShoot()
    {
        shootBtn.interactable = false;
    }

    public void Shoot()
    {
        StartCoroutine(ShootDelay());
    }
    IEnumerator ShootDelay()
    {
        ChangeCam();
        yield return new WaitForSeconds(1.5f);
        if (onPlayerShoot != null)
        {
            onPlayerShoot.Invoke();
        }
        Instantiate(boomerang, SpawnPt.position, Quaternion.identity);
    }
    public void ChangeCam()
    {
        camEnabled = !camEnabled;
        PlayerManager.instance.playerCam.gameObject.SetActive(camEnabled);
        transform.GetChild(1).gameObject.SetActive(camEnabled);
    }
}
