using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    [SerializeField] List<GameObject> ShipModules;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<ModuleInfo>() != null)
            {
                ShipModules.Add(child.gameObject);
            }
            else
            {
                Debug.Log("Useless");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
