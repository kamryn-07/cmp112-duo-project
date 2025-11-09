using System;
using System.Collections;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{

    public CooldownController cooldownBarScript;

    public float ACCELERATION_FACTOR = 75.0f;
    public float DECELERATION_FACTOR = 50.0f;
    public float SPEED_THRESHOLD = 750.0f;
    public float JUMP_FORCE = 2500.0f;
    public float UTILITY_FORCE = 3500.0f;
    public float UTILITY_COOLDOWN = 1.5f;
    public float DOWNWARD_FORCE = -50.0f;
    public float WALLJUMP_THRESHOLD = 50.0f;
    public float WALLJUMP_VERTICAL_DIVISION = 2.5f;

    InputAction moveAction;
    InputAction jumpAction;
    InputAction utilityAction;
    GameObject player;
    Rigidbody playerRigidBody;

    public float xSpeed = 0.0f;
    public float ySpeed = 0.0f;

    private RaycastHit[] hits;

    bool utilityDebounce = false;

    private void Start()
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
        if (IsGrounded() && jumpAction.IsPressed())
        {
            OnJump();
        }
        else if (!IsGrounded() && jumpAction.IsPressed())
        {

            hits = new RaycastHit[4];
            hits = GetWallHits();

            if (hits[0].normal != Vector3.zero)
            {
                OnWallJump(hits[0].normal);
            }
            else if (hits[1].normal != Vector3.zero)
            {
                OnWallJump(hits[1].normal);
            }
            else if (hits[2].normal != Vector3.zero)
            {
                OnWallJump(hits[2].normal);
            }
            else if (hits[3].normal != Vector3.zero)
            {
                OnWallJump(hits[3].normal);
            }

        }
        if (!IsGrounded())
        {
            playerRigidBody.AddForce(0, DOWNWARD_FORCE, 0);
        }
        if (utilityAction.IsPressed() && !utilityDebounce)
        {
            StartCoroutine(ActivateUtility(moveValue));
        }

    }

    // raycast to find if player is grounded for jump so u can't inf jump
    private bool IsGrounded()
    {

        float distanceToGround = GetComponent<Collider>().bounds.extents.y;
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f);

    }

    // specifically for wall jumps, of course
    private RaycastHit[] GetWallHits()
    {

        Physics.Raycast(transform.position, Vector3.left, out var hitL, WALLJUMP_THRESHOLD);
        Physics.Raycast(transform.position, Vector3.right, out var hitR, WALLJUMP_THRESHOLD);
        Physics.Raycast(transform.position, Vector3.forward, out var hitF, WALLJUMP_THRESHOLD);
        Physics.Raycast(transform.position, Vector3.back, out var hitB, WALLJUMP_THRESHOLD);

        return new RaycastHit[4]{ hitL, hitR, hitF, hitB };

    }

    IEnumerator ActivateUtility(Vector2 v2MoveValue)
    {

        utilityDebounce = true;
        Vector2 direction = v2MoveValue;
        Vector3 force = new(UTILITY_FORCE * direction.x, 0, UTILITY_FORCE * direction.y);
        playerRigidBody.AddRelativeForce(force);
        cooldownBarScript.StartCoroutine("InitiateCooldown", UTILITY_COOLDOWN);
        yield return new WaitForSecondsRealtime(UTILITY_COOLDOWN);
        utilityDebounce = false;

    }
    private void OnJump()
    {

        playerRigidBody.AddForce(0, JUMP_FORCE, 0);

    }
    private void OnWallJump(Vector3 normal)
    {

        UnityEngine.Debug.Log(normal);
        playerRigidBody.AddForce(JUMP_FORCE * normal.x, JUMP_FORCE / WALLJUMP_VERTICAL_DIVISION, JUMP_FORCE * normal.z);

    }

}
