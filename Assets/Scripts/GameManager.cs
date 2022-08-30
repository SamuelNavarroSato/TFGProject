using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public CardManager cardManager;

    public GameState state;

    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.PHASE_A_P1);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.PHASE_A_P1:
                HandlePhaseA_P1();
                break;
            case GameState.PHASE_A_P2:
                HandlePhaseA_P2();
                break;
            case GameState.PHASE_B_P1:
                HandlePhaseB_P1();
                break;
            case GameState.PHASE_B_P2:
                HandlePhaseB_P2();
                break;
            case GameState.PHASE_C_P1:
                HandlePhaseC_P1();
                break;
            case GameState.PHASE_C_P2:
                HandlePhaseC_P2();
                break;
            default:
                Debug.Log("Error");
                break;
                
        }
        OnGameStateChanged?.Invoke(newState);
    }

    private void HandlePhaseA_P1()
    {

    }

    private void HandlePhaseA_P2()
    {

    }

    private void HandlePhaseB_P1()
    {

    }

    private void HandlePhaseB_P2()
    {

    }

    private void HandlePhaseC_P1()
    {

    }

    private void HandlePhaseC_P2()
    {

    }
}

public enum GameState
{
    PHASE_A_P1,
    PHASE_A_P2,
    PHASE_B_P1,
    PHASE_B_P2,
    PHASE_C_P1,
    PHASE_C_P2
}

public enum PlayerEnum
{
    PLAYER_1,
    PLAYER_2
}