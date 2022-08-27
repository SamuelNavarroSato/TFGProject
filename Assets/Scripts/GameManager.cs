using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public CardManager cardManager;
    public Camera cameraManager;

    public GameState state;

    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.PHASE_2);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.PHASE_1:
                HandlePhase1();
                break;
            case GameState.PHASE_2:
                break;
            case GameState.PHASE_3:
                break;
            default:
                Debug.Log("Error");
                break;
                
        }
        OnGameStateChanged?.Invoke(newState);
    }

    private void HandlePhase1()
    {

    }
}

public enum GameState
{
    PHASE_1,
    PHASE_2,
    PHASE_3
}