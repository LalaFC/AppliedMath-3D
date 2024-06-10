using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        PlayerMovement.onPlayerMove.AddListener(Move);
        PlayerMovement.onPlayerIdle.AddListener(Idle);
        PlayerActions.onPlayerShoot.AddListener(Shoot);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Move()
    {
        anim.SetFloat("velocity", PlayerManager.instance.movement.inputSpeed);
    }
    void Idle()
    {
        anim.SetFloat("velocity", 0);
    }
    void Shoot()
    {
        anim.SetTrigger("attack");
    }
}
