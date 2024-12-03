using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Pan Settings")]
    public float panSpeed = 20f;
    public float panBorderThickness = 10f; // Thickness of the screen edge that triggers panning
    public Vector2 panLimit; // Limits for panning movement

    [Header("Zoom Settings")]
    public float scrollSpeed = 20f;
    public float minZoom = 10f; // Minimum camera height
    public float maxZoom = 50f; // Maximum camera height

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    void HandleMovement()
    {
        Vector3 pos = transform.position;

        // Calculate movement direction based on camera's orientation
        Vector3 up = transform.up;
        Vector3 right = transform.right;

        // Remove the y component so movement is only horizontal
        up.x = 0;
        right.y = 0;
        up.Normalize();
        right.Normalize();

        // Mouse and keyboard panning
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos += up * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            pos -= up * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos += right * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            pos -= right * panSpeed * Time.deltaTime;
        }

        // Clamp panning to defined limits
        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);

        transform.position = pos;
    }

    public void MoveCamera(GameObject target)
    {
        transform.position = target.transform.position;
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float newSize = cam.orthographicSize - scroll * scrollSpeed * Time.deltaTime;

        // Clamp zoom levels
        cam.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
    }
}
