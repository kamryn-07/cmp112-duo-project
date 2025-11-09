using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{

    public float sensX = 75.0f;
    public float sensY = 75.0f;
    public float smoothSpeed = 12.5f;
    public float maxInputDelta = 1000.0f;
    public bool lookIsPointerDelta = true;

    Vector2 rawLook = Vector2.zero;
    Vector2 look = Vector2.zero;
    float yaw;      // horizontal
    float pitch;    // vertical

    InputAction Look;
    GameObject player;
    Rigidbody playerRigidBody;

    void Start()
    {

        player = GameObject.Find("Player");
        playerRigidBody = player.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Look = InputSystem.actions.FindAction("Look");
        transform.rotation = Quaternion.identity;

    }

    private void Update()
    {

        if (Look == null) return;
        rawLook = Look.ReadValue<Vector2>();

        // clamp extremely large spikes
        if (rawLook.sqrMagnitude > maxInputDelta * maxInputDelta)
            rawLook = rawLook.normalized * maxInputDelta;

        // exponential smoothing of the raw input to reduce spikes
        float factor = 1f - Mathf.Exp(-smoothSpeed * Time.deltaTime);
        look = Vector2.Lerp(look, rawLook, factor);

    }

    void LateUpdate()
    {

        if (player == null) return;
        float dt = Time.deltaTime;

        // attach the camera's position to the player
        transform.position = player.transform.position;

        // rotation calculations
        float scale = lookIsPointerDelta ? 1.0f : dt;
        yaw += look.x * sensX * scale;
        pitch += -look.y * sensY * scale;
        yaw = Mathf.Repeat(yaw, 360.0f);
        pitch = Mathf.Clamp(pitch, -89.0f, 89.0f);

        // rotation handling
        Quaternion targRotation = Quaternion.Euler(pitch, yaw, 0.0f);

        // interpolation
        float t = 1.0f - Mathf.Exp(-smoothSpeed * dt);
        transform.rotation = Quaternion.Slerp(transform.rotation, targRotation, t);

    }
    private void FixedUpdate()
    {

        // player rotation handling
        float tFixed = 1.0f - Mathf.Exp(-smoothSpeed * Time.fixedDeltaTime);
        Quaternion targPlrRotation = Quaternion.Euler(0.0f, yaw, 0.0f);
        playerRigidBody.MoveRotation(Quaternion.Slerp(player.transform.rotation, targPlrRotation, tFixed));    // camera's rotation still jumps around in odd ways                                                                                              // please do fix this, it actually looks so fucking bad

    }

}
