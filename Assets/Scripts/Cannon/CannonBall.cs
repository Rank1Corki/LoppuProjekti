using UnityEngine;

public class Cannonball : MonoBehaviour
{
    void Start()
    {
        // Destroy the cannonball after 5 seconds automatically
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);

        ModuleInfo partInfo = collision.gameObject.GetComponent<ModuleInfo>();

        partInfo.hP -= 10;
    }
}
