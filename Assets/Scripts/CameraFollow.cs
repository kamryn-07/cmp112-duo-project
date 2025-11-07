using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{

    public float sensX = 12.5f;
    public float sensY = 12.5f;

    InputAction Look;

    GameObject player;

    void Start()
    {

        player = GameObject.Find("Player");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Look = InputSystem.actions.FindAction("Look");

    }

    void Update()
    {
        
        float dt = Time.deltaTime;

        float mouseX = Look.ReadValue<Vector2>().x;
        float mouseY = Look.ReadValue<Vector2>().y;

        // attach the camera's position to the player
        transform.position = player.transform.position;

        // rotation handling
        transform.RotateAround(player.transform.position, player.transform.up, (mouseX) * dt * sensX); // sensX is not working, please fix this fucking immediately (and same for y)

    }

}
