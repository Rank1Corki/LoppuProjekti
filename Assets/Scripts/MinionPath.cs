using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionPath : MonoBehaviour
{
    [SerializeField] GameObject pointToGo;


    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hit;
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            hit = Physics2D.Raycast(gameObject.transform.position, pointToGo.transform.position);
        }
        
    }
}
