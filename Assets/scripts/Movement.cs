using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    public GameObject player;
    public CooldownController cooldownController;
    public SfxController sfxController;

    Rigidbody playerRigidBody;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction utilityAction;

    public float accelerationFactor;
    public float decelerationFactor;
    public float speedThreshold;
    public float jumpForce;
    public float utilityForce;
    public float utilityCooldown;
    public float downwardForce;
    public float walljumpThreshold;
    public float walljumpVerticalDivision;

    public float xSpeed = 0.0f;
    public float ySpeed = 0.0f;
    public bool inAir = false;
    public bool inWalljumpPosition = false;

    private RaycastHit[] hits;

    private bool utilityDebounce = false;

    void Start()
    {

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        utilityAction = InputSystem.actions.FindAction("Utility");
        playerRigidBody = player.GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {

        // player movement handling
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        if (moveValue.magnitude > 0)
        {
            xSpeed = (moveValue.x * accelerationFactor);
            ySpeed = (moveValue.y * accelerationFactor);
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
            else
            {
                inWalljumpPosition = false;
            }

        }

        if (!IsGrounded())
        {
            inAir = true;
            playerRigidBody.AddForce(0, downwardForce, 0);
        }
        else
        {
            if (inAir)
            {
                sfxController.OnLandSfx();
            }
            inAir = false;
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

        Physics.Raycast(transform.position, Vector3.left, out var hitL, walljumpThreshold);
        Physics.Raycast(transform.position, Vector3.right, out var hitR, walljumpThreshold);
        Physics.Raycast(transform.position, Vector3.forward, out var hitF, walljumpThreshold);
        Physics.Raycast(transform.position, Vector3.back, out var hitB, walljumpThreshold);

        return new RaycastHit[4]{ hitL, hitR, hitF, hitB };

    }

    IEnumerator ActivateUtility(Vector2 v2MoveValue)
    {

        utilityDebounce = true;
        sfxController.OnUtilitySfx();
        Vector2 direction = v2MoveValue;
        Vector3 force = new(utilityForce * direction.x, 0, utilityForce * direction.y);
        playerRigidBody.AddRelativeForce(force);
        cooldownController.StartCoroutine("InitiateCooldown", utilityCooldown);
        yield return new WaitForSecondsRealtime(utilityCooldown);
        utilityDebounce = false;

    }
    private void OnJump()
    {

        sfxController.OnJumpSfx();
        playerRigidBody.AddForce(0, jumpForce, 0);

    }
    private void OnWallJump(Vector3 normal)
    {

        if (!inWalljumpPosition)
        {
            sfxController.OnJumpSfx();
        }
        inWalljumpPosition = true;
        playerRigidBody.AddForce(jumpForce * normal.x, jumpForce / walljumpVerticalDivision, jumpForce * normal.z);

    }

}
