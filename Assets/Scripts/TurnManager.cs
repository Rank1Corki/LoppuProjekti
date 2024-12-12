using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public enum PlayerState
    {
        PlayerLeft,
        PlayerRight
    }

    [SerializeField] int commands;

    public PlayerState currentState;

    public TMP_Text turns;

    private CameraController camera;

    public GameObject leftCannon;
    public GameObject rightCannon;

    private ObjectSelector1 leftSel;
    private ObjectSelector1 rightSel;

    // Start is called before the first frame update
    void Start()
    {
        commands = 5;
        camera = GameObject.Find("Main Camera").GetComponent<CameraController>();
        turns.text = $"Turns left: {commands}";

        leftSel = leftCannon.GetComponent<ObjectSelector1>();
        rightSel = rightCannon.GetComponent<ObjectSelector1>();
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
                    turns.text = $"Turns left: {commands}";
                    if (commands == 0)
                    {
                        currentState = PlayerState.PlayerRight;
                        Debug.Log("Your turn is over");
                        commands = 5;
                        camera.MoveCamera(GameObject.Find("CameraTargetRight"));
                        turns.text = $"Turns left: {commands}";
                        leftSel.Deselect();
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
                    turns.text = $"Turns left: {commands}";
                    if (commands == 0)
                    {
                        currentState = PlayerState.PlayerLeft;
                        Debug.Log("Your turn is over");
                        commands = 5;
                        camera.MoveCamera(GameObject.Find("CameraTargetLeft"));
                        turns.text = $"Turns left: {commands}";
                        rightSel.Deselect();
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
