using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ShipSceneManager : MonoBehaviour
{
    public string text;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToGameOver(string varText)
    {
        text = varText;
    }
}
