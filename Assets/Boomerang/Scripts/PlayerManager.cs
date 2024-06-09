using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance { get; set; }
    public PlayerMovement movement;
    public PlayerAnimation anim;
    public GameObject boomerang;
    public Transform SpawnPt;

    private void Awake()
    {
        if (instance != null && instance != this) { Destroy(this); }
        else { instance = this; }
        movement = GetComponent<PlayerMovement>();
        anim= GetComponent<PlayerAnimation>();
    }
    public void Shoot()
    {
        Instantiate(boomerang, SpawnPt.position, Quaternion.identity);
    }
}
