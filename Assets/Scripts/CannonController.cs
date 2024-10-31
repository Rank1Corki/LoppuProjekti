using UnityEngine;
using System.Collections;

public class CannonController : MonoBehaviour
{
    public Transform barrel;
    public Transform shootPoint;
    public LineRenderer lineRenderer;
    public float projectileSpeed = 10f;
    public float maxAngle = 45f;
    public int trajectorySteps = 30;
    public GameObject[] cannonballPrefabs; // Array to store different cannonball prefabs
    public float gravityScale = 1f;
    public int ammoCount = 5;
    public float shootCooldown = 1f; // Cooldown duration between each shot
    Quaternion spread;

    private int selectedCannonballType = -1; // Start with an invalid type
    private bool canShoot = false; // Flag to indicate if the cannon is ready to fire
    private ObjectSelector1 objectSelector;

    private void Start()
    {
        objectSelector = GetComponent<ObjectSelector1>();
    }

    private void Update()
    {
        if (objectSelector != null && objectSelector.IsSelected() && selectedCannonballType != -1 && canShoot)
        {
            RotateBarrelTowardsMouse();

            if (Input.GetMouseButton(0))
            {
                DrawTrajectory();
                lineRenderer.enabled = true;
            }
            else
            {
                lineRenderer.enabled = false;
            }

            if (Input.GetMouseButtonUp(0) && ammoCount > 0)
            {
                ShootCannonball();
                ammoCount--;

                if (ammoCount == 0)
                {
                    objectSelector.Deselect();
                }
            }
        }
    }

    public void SetCannonballType(int type)
    {
        if (type >= 0 && type < cannonballPrefabs.Length)
        {
            selectedCannonballType = type; // Update the selected type
            Debug.Log("Cannonball Type Set: " + type);
            ammoCount += 5;
            StartCoroutine(ActivateShootingAfterDelay(1f)); // Start the coroutine with a 1-second delay
        }
        else
        {
            Debug.LogWarning("Invalid cannonball type selected.");
        }
    }

    private IEnumerator ActivateShootingAfterDelay(float delay)
    {
        canShoot = false; // Set canShoot to false initially
        Debug.Log("Cannon is in use and cannot shoot yet.");
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        canShoot = true; // Allow shooting after the delay
        Debug.Log("Cannon is now ready to shoot.");
    }

    private void RotateBarrelTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector2 direction = (mousePosition - barrel.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, 0, maxAngle);

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        barrel.rotation = Quaternion.Slerp(barrel.rotation, targetRotation, Time.deltaTime * 5f);
    }

    private void DrawTrajectory()
    {
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        Vector2 startPoint = shootPoint.position;
        Vector2 initialVelocity = shootPoint.right * projectileSpeed;

        lineRenderer.positionCount = trajectorySteps;
        for (int i = 0; i < trajectorySteps; i++)
        {
            float time = i * 0.1f;
            Vector2 position = startPoint + initialVelocity * time + 0.5f * Physics2D.gravity * gravityScale * time * time;
            lineRenderer.SetPosition(i, position);
        }

        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
    }

    private void ShootCannonball()
    {
        if (!canShoot) return; // Prevent shooting if canShoot is false

        // Set canShoot to false immediately to prevent shooting again until cooldown is over
        canShoot = false;
        StartCoroutine(ShootingCooldown());
        spread = Quaternion.Euler(0, 0, Random.Range(1, -5));

        // Use the selected cannonball prefab
        GameObject cannonballPrefab = cannonballPrefabs[selectedCannonballType];

        GameObject cannonball = Instantiate(cannonballPrefab, shootPoint.position, shootPoint.rotation * spread);
        Rigidbody2D rb = cannonball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = shootPoint.right * projectileSpeed;
            rb.gravityScale = gravityScale;
        }
    }

    private IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(shootCooldown); // Wait for cooldown duration
        canShoot = true; // Allow shooting again
    }
}
