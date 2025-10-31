using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    InputAction moveAction;
    InputAction jumpAction;
    GameObject player;
    Rigidbody playerRigidBody;

    const float SPEED = 4.5f;
    const float ACCELERATION = SPEED / 5;
    const float JUMP_FORCE = 100.0f;
    const float DOWNWARD_FORCE = -2.0f;
    float xSpeed = 0.0f;
    float ySpeed = 0.0f;

    void Start()
    {

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        player = GameObject.Find("Player");
        playerRigidBody = player.GetComponent<Rigidbody>();

    }

    void Update()
    {

        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        xSpeed = moveValue.x * SPEED;
        ySpeed = moveValue.y * SPEED;

        // player movement handling and increased gravity (downward force applied)
        playerRigidBody.AddForce(xSpeed, DOWNWARD_FORCE, ySpeed);

        if (jumpAction.IsPressed() && IsGrounded())
        {
            playerRigidBody.AddForce(0, JUMP_FORCE, 0);
        }

    }

    // raycast to find if player is grounded for jump so u can't inf jump
    public bool IsGrounded()
    {
        float distanceToGround = GetComponent<Collider>().bounds.extents.y;
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f);
    }

}
