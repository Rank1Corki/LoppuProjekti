using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ShipScript : MonoBehaviour
{
    [SerializeField] List<GameObject> ShipModules;
    public int health;
    ShipSceneManager sceneManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject tM = GameObject.Find("SceneManager");

        sceneManager = tM.GetComponent<ShipSceneManager>();
        foreach (Transform child in transform)
        {
            if (child.GetComponent<ModuleInfo>() != null)
            {
                ShipModules.Add(child.gameObject);
                health += 10;
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
        if (health <= 0)
        {
            if (this.tag == "Left")
            {
                sceneManager.GoToGameOver("Right player won!");
                SceneManager.LoadScene("GameOver");
            }
            else if (this.tag == "Right")
            {
                sceneManager.GoToGameOver("Left player won!");
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}
