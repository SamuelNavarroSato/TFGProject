using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsManager : MonoBehaviour
{
    public GameObject bluePlayer;
    public GameObject redPlayer;
    void Awake()
    {
        GameManager.OnGameStateChanged += WhenGameStateChanges;
    }

    private void WhenGameStateChanges(GameState currentState)
    {
        if (currentState == GameState.PHASE_A_P1)
        {
            gameObject.SetActive(true);
        }
        else if (currentState == GameState.PHASE_A_P2)
        {
            gameObject.SetActive(true);
            bluePlayer.SetActive(true);
            redPlayer.SetActive(false);
        }
        else
        {

        }
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
