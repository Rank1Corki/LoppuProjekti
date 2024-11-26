using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleInfo : MonoBehaviour
{
    public int hP = 100;
    public Sprite destroyed;
    private Sprite intact;
    private SpriteRenderer spriteRenderer;
    bool repairTool = true;
    public Inventory inventory;

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

    private void OnMouseOver()
    {
        // Check for left mouse button click to select the cannon
        if (Input.GetMouseButtonDown(0) && repairTool)
        {
            if (inventory.HasItem("wood", 1))
            {
                spriteRenderer.sprite = destroyed;
            }
        }
    }
}
