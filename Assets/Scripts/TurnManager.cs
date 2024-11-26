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

    public PlayerState currentState;    
    // Start is called before the first frame update
    void Start()
    {
        
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
                    return true;
                }
                break;

            case PlayerState.PlayerRight:
                if (tag == "Right")
                {
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
