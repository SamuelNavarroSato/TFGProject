using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject blue;
    public GameObject red;

    void Awake()
    {
        GameManager.OnGameStateChanged += WhenGameStateChanges;
    }

    private void WhenGameStateChanges(GameState currentState)
    {
        if (currentState == GameState.PHASE_A_P1)
        {
            red.SetActive(true);
            blue.SetActive(false);
        }
        else if (currentState == GameState.PHASE_A_P2)
        {
            red.SetActive(false);
            blue.SetActive(true);
        }
        else if (currentState == GameState.PHASE_B_P1)
        {
            red.SetActive(true);
            blue.SetActive(false);
        }
        else if (currentState == GameState.PHASE_B_P2)
        {
            red.SetActive(false);
            blue.SetActive(true);
        }
        else if (currentState == GameState.PHASE_C_P1)
        {
            red.SetActive(true);
            blue.SetActive(false);
        }
        else if (currentState == GameState.PHASE_C_P2)
        {
            red.SetActive(false);
            blue.SetActive(true);
        }
    }

}
