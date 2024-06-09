using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction movementInput;
    public static UnityEvent onPlayerMove = new UnityEvent();
    public static UnityEvent onPlayerIdle = new UnityEvent();
    [SerializeField] float moveSpeed = 2f;
    public float inputSpeed;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        movementInput= playerInput.actions.FindAction("Movement");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
    }
    void MovePlayer()
    {
        if (onPlayerMove != null && movementInput.ReadValue<Vector2>() != Vector2.zero)
        {
            inputSpeed = movementInput.ReadValue<Vector2>().normalized.magnitude;
            onPlayerMove.Invoke();
        }
        else if (onPlayerIdle != null) onPlayerIdle.Invoke();
        Vector2 direction = movementInput.ReadValue<Vector2>().normalized;
        transform.position += new Vector3(direction.x, 0, direction.y) * moveSpeed * Time.deltaTime;
    }
}
