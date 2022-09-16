using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState state;

    [SerializeField] public PlayerManager player1;
    [SerializeField] public PlayerManager player2;

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
            case GameState.PHASE_C:
                HandlePhaseC();
                break;
            case GameState.ENDGAME:
                HandleEndgame();
                break;
            default:
                Debug.Log("Error");
                break;
                
        }
        OnGameStateChanged?.Invoke(newState);
    }
    public void ModifyScore(PlayerEnum player, int score)
    {
        if (player == PlayerEnum.PLAYER_1)
        {
            player1.IncreaseScore(score);
        }
        else if (player == PlayerEnum.PLAYER_2)
        {
            player2.IncreaseScore(score);
        }
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

    private void HandlePhaseC()
    {

    }

    private void HandleEndgame()
    {

    }

    public void GoNextPhase()
    {
        UpdateGameState(state + 1);
    }

    public void OnVictory()
    {
        Application.Quit();
    }
}

public enum GameState
{
    PHASE_A_P1,
    PHASE_A_P2,
    PHASE_B_P1,
    PHASE_B_P2,
    PHASE_C,
    ENDGAME
}

public enum PlayerEnum
{
    PLAYER_1,
    PLAYER_2,
    DEFAULT
}