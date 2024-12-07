using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public enum PlayerState
    {
        PlayerLeft,
        PlayerRight
    }

    [SerializeField] int commands;

    public PlayerState currentState;

    private CameraController camera;
    // Start is called before the first frame update
    void Start()
    {
        commands = 4;
        camera = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isMyTurn(string tag)
    {
        switch (currentState)
        {
            case PlayerState.PlayerLeft:
                if (tag == "Left")
                {
                    commands--;
                    if (commands == 0)
                    {
                        currentState = PlayerState.PlayerRight;
                        Debug.Log("Your turn is over");
                        commands = 5;
                        camera.MoveCamera(GameObject.Find("CameraTargetRight"));
                        return true;
                    }
                    Debug.Log($"You have {commands} commands left");
                    return true;

                }
                break;

            case PlayerState.PlayerRight:
                if (tag == "Right")
                {
                    commands--;
                    if (commands == 0)
                    {
                        currentState = PlayerState.PlayerLeft;
                        Debug.Log("Your turn is over");
                        commands = 4;
                        camera.MoveCamera(GameObject.Find("CameraTargetLeft"));
                        return true;
                    }
                    Debug.Log($"You have {commands} commands left");
                    return true;
                }
                break;

            default:
                Debug.Log("Unknown state.");
                break;
        }

        return false;
    }
}
