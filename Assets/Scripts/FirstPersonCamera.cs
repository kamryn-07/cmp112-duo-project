using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{

    public GameObject player;
    Rigidbody playerRigidBody;
    InputAction Look;

    public Vector3 cameraOffset;
    public float sensX;
    public float sensY;
    public float smoothSpeed;
    public float maxInputDelta;

    Vector2 rawLook = Vector2.zero;
    Vector2 look = Vector2.zero;
    float yaw;      // horizontal
    float pitch;    // vertical

    void Start()
    {

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

        if (Time.deltaTime > 0.002f || Time.deltaTime < 0.001f)
        {
            UnityEngine.Debug.Log(Time.deltaTime);
        }

    }

    void LateUpdate()
    {

        if (player == null) return;

        // attach the camera's position to the player
        transform.position = player.transform.position + cameraOffset;
        transform.SetLocalPositionAndRotation(transform.position, transform.rotation);

        // rotation calculations
        yaw += look.x * sensX;
        pitch += -look.y * sensY;
        yaw = Mathf.Repeat(yaw, 360.0f);
        pitch = Mathf.Clamp(pitch, -85.0f, 85.0f);

        // rotation handling
        Quaternion targRotation = Quaternion.Euler(pitch, yaw, 0.0f);

        // interpolation
        float t = 1.0f - Mathf.Exp(-smoothSpeed * Time.deltaTime);
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
