using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance { get; set; }
    public PlayerMovement movement;
    public PlayerAnimation anim;
    public GameObject boomerang;
    public GameObject player;
    public Slider force;

    private void Awake()
    {
        if (instance != null && instance != this) { Destroy(this); }
        else { instance = this; }

        movement = GetComponent<PlayerMovement>();
        anim= GetComponent<PlayerAnimation>();
        player = GameObject.FindGameObjectWithTag("Player");
        force = GameObject.FindGameObjectWithTag("Force").GetComponent<Slider>();
    }
}
