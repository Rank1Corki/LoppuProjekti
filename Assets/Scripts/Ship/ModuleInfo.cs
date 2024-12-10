using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleInfo : MonoBehaviour
{
    public int hP = 10;
    public Sprite destroyed;
    private Sprite intact;
    private SpriteRenderer spriteRenderer;
    bool repairTool = true;
    public Inventory inventory;
    ShipScript shipScript;

    bool isDestroyed = false;

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

        shipScript = GetComponentInParent<ShipScript>();

        Debug.Log(this.tag);

    }

    // Update is called once per frame
    void Update()
    {
        if (hP <= 0 && !isDestroyed)
        {
            shipScript.health -= 10;
            spriteRenderer.sprite = destroyed;
            isDestroyed = true;
        }


    }

    private void OnMouseOver()
    {
        
        if (Input.GetMouseButtonDown(0) && cursorManager.repairToolIsActive && spriteRenderer.sprite == destroyed)
        {
            if (inventory.HasItem("Wood", 1) && turnManager.isMyTurn(this.tag))
            {
                hP = 10;
                shipScript.health += 10;
                spriteRenderer.sprite = intact;
                inventory.RemoveItem("Wood", 1);
                isDestroyed = false;
            }
            else
            {
                Debug.Log("No Wood");
            }
        }
    }
}
