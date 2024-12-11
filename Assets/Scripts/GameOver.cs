using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TMP_Text text;
    ShipSceneManager shipSceneManager;
    // Start is called before the first frame update
    void Start()
    {
        GameObject tM = GameObject.Find("SceneManager");

        shipSceneManager = tM.GetComponent<ShipSceneManager>();

        text.text = shipSceneManager.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
