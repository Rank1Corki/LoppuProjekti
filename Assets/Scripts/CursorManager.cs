using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public bool repairToolIsActive = false;

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            repairToolIsActive = true;
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            repairToolIsActive = false;
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }


}
