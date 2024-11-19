using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleInfo : MonoBehaviour
{
    public int hP = 100;
    public Sprite destroyed;
    private Sprite intact;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if (hP <= 0)
        {
            spriteRenderer.sprite = destroyed;
        }
    }
}
