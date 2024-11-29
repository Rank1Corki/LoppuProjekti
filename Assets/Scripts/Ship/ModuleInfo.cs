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

    TurnManager turnManager;
    CursorManager cursorManager;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        intact = spriteRenderer.sprite;

        GameObject tM = GameObject.Find("TurnManager");

       turnManager = tM.GetComponent<TurnManager>();
        cursorManager = tM.GetComponent<CursorManager>();

        Debug.Log(this.tag);

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
        
        if (Input.GetMouseButtonDown(0) && cursorManager.repairToolIsActive && spriteRenderer.sprite == destroyed)
        {
            if (inventory.HasItem("Wood", 1) && turnManager.isMyTurn(this.tag))
            {
                hP = 100;
                spriteRenderer.sprite = intact;
                inventory.RemoveItem("Wood", 1);
            }
            else
            {
                Debug.Log("No Wood");
            }
        }
    }
}
