using System.Collections;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{

    public float ACCELERATION_FACTOR = 75.0f;
    public float DECELERATION_FACTOR = 50.0f;
    public float SPEED_THRESHOLD = 750.0f;
    public float JUMP_FORCE = 2500.0f;
    public float UTILITY_FORCE = 3500.0f;
    public float UTILITY_COOLDOWN = 1.5f;
    public float DOWNWARD_FORCE = -50.0f;

    InputAction moveAction;
    InputAction jumpAction;
    InputAction utilityAction;
    GameObject player;
    Rigidbody playerRigidBody;

    public float xSpeed = 0.0f;
    public float ySpeed = 0.0f;

    bool utilityDebounce = false;

    void Start()
    {

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        utilityAction = InputSystem.actions.FindAction("Utility");
        player = GameObject.Find("Player");
        playerRigidBody = player.GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {

        // player movement handling
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        if (moveValue.magnitude > 0)
        {
            xSpeed = (moveValue.x * ACCELERATION_FACTOR);
            ySpeed = (moveValue.y * ACCELERATION_FACTOR);
        }
        else
        {
            xSpeed = 0.0f;
            ySpeed = 0.0f; // come back to this to fix deceleration plz
        }
        playerRigidBody.AddRelativeForce(xSpeed, 0, ySpeed);

        // pressed actions & gravity application
        if (IsGrounded())
        {
            if (jumpAction.IsPressed())
            {
                playerRigidBody.AddForce(0, JUMP_FORCE, 0);
            }
        }
        else
        {
            playerRigidBody.AddForce(0, DOWNWARD_FORCE, 0);
        }
        if (utilityAction.IsPressed() && !utilityDebounce)
        {
            StartCoroutine(activateUtility(moveValue));
        }

    }

    // raycast to find if player is grounded for jump so u can't inf jump
    public bool IsGrounded()
    {

        float distanceToGround = GetComponent<Collider>().bounds.extents.y;
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f);

    }
    IEnumerator activateUtility(Vector2 v2MoveValue)
    {

        utilityDebounce = true;
        Vector2 direction = v2MoveValue;
        Vector3 force = new(UTILITY_FORCE * direction.x, 0, UTILITY_FORCE * direction.y);
        playerRigidBody.AddRelativeForce(force);
        yield return new WaitForSeconds(UTILITY_COOLDOWN);
        utilityDebounce = false;

    }

}
