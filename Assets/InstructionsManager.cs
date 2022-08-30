using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsManager : MonoBehaviour
{
    public GameObject redPlayer;
    public GameObject bluePlayer;

    public GameObject redPair;
    public GameObject bluePair;

    public GameObject redWord;
    public GameObject blueWord;

    void Awake()
    {
        GameManager.OnGameStateChanged += WhenGameStateChanges;
    }

    private void WhenGameStateChanges(GameState currentState)
    {
        if (currentState == GameState.PHASE_A_P1)
        {
            gameObject.SetActive(true);

            redWord.SetActive(true);
            redPair.SetActive(false);
            blueWord.SetActive(false);
            bluePair.SetActive(false);
        }
        else if (currentState == GameState.PHASE_A_P2)
        {
            gameObject.SetActive(true);

            bluePlayer.SetActive(true);
            redPlayer.SetActive(false);

            blueWord.SetActive(true);
            redWord.SetActive(false);

        }
        else if (currentState == GameState.PHASE_B_P1)
        {
            gameObject.SetActive(true);

            bluePlayer.SetActive(false);
            redPlayer.SetActive(true);

            redWord.SetActive(false);
            redPair.SetActive(true);
        }
        else if (currentState == GameState.PHASE_B_P2)
        {
            gameObject.SetActive(true);

            redPlayer.SetActive(false);
            bluePlayer.SetActive(true);

            blueWord.SetActive(false);
            bluePair.SetActive(true);
        }
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
