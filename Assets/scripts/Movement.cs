using System.Collections;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    const int FRAME_SPEED_CAP = 60;
    const float ACCELERATION_FACTOR = 600.0f;
    const float DECELERATION_FACTOR = 450.0f;
    const float SPEED_THRESHOLD = 2000.0f;
    const float JUMP_FORCE = 100.0f;
    const float UTILITY_FORCE = 3500.0f;
    const float UTILITY_COOLDOWN = 1.5f;
    const float DOWNWARD_FORCE = -1750.0f;

    InputAction moveAction;
    InputAction jumpAction;
    InputAction utilityAction;
    GameObject player;
    Rigidbody playerRigidBody;

    float xSpeed = 0.0f;
    float ySpeed = 0.0f;

    bool utilityDebounce = false;

    void Start()
    {

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        utilityAction = InputSystem.actions.FindAction("Utility");
        player = GameObject.Find("Player");
        playerRigidBody = player.GetComponent<Rigidbody>();

    }

    void Update()
    {

        float frameRateFactor = Time.deltaTime;

        // player movement handling
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        if (moveValue.magnitude > 0)
        {
            xSpeed = (moveValue.x * ACCELERATION_FACTOR) * frameRateFactor;
            ySpeed = (moveValue.y * ACCELERATION_FACTOR) * frameRateFactor;
        }
        else
        {
            xSpeed = 0.0f;
            ySpeed = 0.0f; // come back to this to fix deceleration plz
        }
        playerRigidBody.AddForce(xSpeed, 0, ySpeed);

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
            playerRigidBody.AddForce(0, DOWNWARD_FORCE * frameRateFactor, 0);
        }
        if (utilityAction.IsPressed() && !utilityDebounce)
        {
            UnityEngine.Debug.Log("activated");
            StartCoroutine(activateUtility(moveValue, frameRateFactor));
            UnityEngine.Debug.Log("off cooldown");
        }

    }

    // raycast to find if player is grounded for jump so u can't inf jump
    public bool IsGrounded()
    {

        float distanceToGround = GetComponent<Collider>().bounds.extents.y;
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f);

    }
    IEnumerator activateUtility(Vector2 v2MoveValue, float frameFactor)
    {

        utilityDebounce = true;
        Vector2 direction = v2MoveValue;
        Vector3 force = new(UTILITY_FORCE * direction.x, 0, UTILITY_FORCE * direction.y);
        playerRigidBody.AddForce(force);
        yield return new WaitForSeconds(UTILITY_COOLDOWN);
        utilityDebounce = false;

    }

}
