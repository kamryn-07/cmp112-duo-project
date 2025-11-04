using System.Collections;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    InputAction moveAction;
    InputAction jumpAction;
    InputAction utilityAction;
    GameObject player;
    Rigidbody playerRigidBody;

    const float SPEED = 4.5f;
    const float JUMP_FORCE = 100.0f;
    const float UTILITY_FORCE = 2000.0f;
    const float UTILITY_COOLDOWN = 1.5f;
    const float DOWNWARD_FORCE = -2.0f;
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

        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        xSpeed = moveValue.x * SPEED;
        ySpeed = moveValue.y * SPEED;

        // player movement handling and increased gravity (downward force applied)
        playerRigidBody.AddForce(xSpeed, DOWNWARD_FORCE, ySpeed);

        // pressed actions
        if (jumpAction.IsPressed() && IsGrounded())
        {
            playerRigidBody.AddForce(0, JUMP_FORCE, 0);
        }
        if (utilityAction.IsPressed() && !utilityDebounce)
        {
            UnityEngine.Debug.Log("activated");
            StartCoroutine(activateUtility(moveValue));
            UnityEngine.Debug.Log("off cooldown");
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
        playerRigidBody.AddForce(force);
        yield return new WaitForSeconds(UTILITY_COOLDOWN);
        utilityDebounce = false;

    }

}
