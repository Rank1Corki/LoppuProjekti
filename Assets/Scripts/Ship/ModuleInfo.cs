using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleInfo : MonoBehaviour
{
    public int hP = 100;
    public GameObject destroyed;
    private GameObject intact;

    // Start is called before the first frame update
    void Start()
    {
        intact = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (hP <= 0)
        {

        }
    }
}
