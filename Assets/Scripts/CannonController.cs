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
    public GameObject[] cannonballPrefabs;
    public float gravityScale = 1f;
    public float shootCooldown = 1f;
    public Inventory inventory;
    Quaternion spread;

    private int selectedCannonballType = -1;
    private bool canShoot = false;
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

            if (Input.GetMouseButtonUp(0))
            {
                ShootCannonball();
            }
        }
    }

    public void SetCannonballType(int type)
    {
        if (type >= 0 && type < cannonballPrefabs.Length)
        {
            selectedCannonballType = type;
            string cannonballType = cannonballPrefabs[selectedCannonballType].name;

            // Debug log to confirm the item name and quantity
            Debug.Log($"Checking for {cannonballType} in inventory.");

            inventory.ShowInventory();  // Print the current inventory contents

            foreach (var key in inventory.items.Keys)
            {
                Debug.Log($"Inventory Key: {key}");
            }


            if (inventory.HasItem(cannonballType, 1))
            {
                Debug.Log("Ammo available for the selected cannonball type.");
                StartCoroutine(ActivateShootingAfterDelay(1f));
            }
            else
            {
                Debug.LogWarning("No ammo available for the selected cannonball type.");
            }
        }
        else
        {
            Debug.LogWarning("Invalid cannonball type selected.");
        }
    }


    private IEnumerator ActivateShootingAfterDelay(float delay)
    {
        canShoot = false;
        yield return new WaitForSeconds(delay);
        canShoot = true;
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
        if (!canShoot) return;

        string cannonballType = cannonballPrefabs[selectedCannonballType].name;

        if (inventory.HasItem(cannonballType, 1))
        {
            canShoot = false;
            StartCoroutine(ShootingCooldown());
            spread = Quaternion.Euler(0, 0, Random.Range(4, -5));

            GameObject cannonball = Instantiate(cannonballPrefabs[selectedCannonballType], shootPoint.position, shootPoint.rotation * spread);
            Rigidbody2D rb = cannonball.GetComponent<Rigidbody2D>();
            inventory.RemoveItem(cannonballType, 1);
            if (rb != null)
            {
                rb.velocity = shootPoint.right * projectileSpeed;
                rb.gravityScale = gravityScale;
            }
        }
        else
        {
            objectSelector.Deselect();
            Debug.LogWarning("Not enough ammo in inventory!");
        }
    }

    private IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }
}
