using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public GameObject boomerang;
    public Transform SpawnPt;
    public void Shoot()
    {
        Instantiate(boomerang, SpawnPt.position, Quaternion.identity);
    }
}
