using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager Instance;

    public GameObject instructionsGO;

    public GameObject redCurtain;
    public GameObject blueCurtain;
    public GameObject bothCurtain;

    public GameObject redPhaseA;
    public GameObject bluePhaseA;

    public GameObject redPhaseB;
    public GameObject bluePhaseB;

    public GameObject bothPhaseC;

    public GameObject resultsScreen;
    public GameObject redResult;
    public GameObject blueResult;
    public GameObject playerText;

    public GameObject blueBackground;
    public GameObject redBackground;
    public GameObject bothBackground;

    void Awake()
    {
        GameManager.OnGameStateChanged += WhenGameStateChanges;

        Instance = this;
    }

    private void WhenGameStateChanges(GameState currentState)
    {
        if (currentState != GameState.ENDGAME)
            instructionsGO.SetActive(true);

        if (currentState == GameState.PHASE_A_P1)
        {
            resultsScreen.SetActive(false);

            redPhaseA.SetActive(true);
            redPhaseB.SetActive(false);
            bluePhaseA.SetActive(false);
            bluePhaseB.SetActive(false);
            bothPhaseC.SetActive(false);

            SetBackground(PlayerEnum.PLAYER_1);
        }
        else if (currentState == GameState.PHASE_A_P2)
        {
            redPhaseA.SetActive(false);
            bluePhaseA.SetActive(true);

            SetBackground(PlayerEnum.PLAYER_2);
        }
        else if (currentState == GameState.PHASE_B_P1)
        {
            bluePhaseA.SetActive(false);
            redPhaseB.SetActive(true);

            SetBackground(PlayerEnum.PLAYER_1);
        }
        else if (currentState == GameState.PHASE_B_P2)
        {
            redPhaseB.SetActive(false);
            bluePhaseB.SetActive(true);

            SetBackground(PlayerEnum.PLAYER_2);
        }
        else if (currentState == GameState.PHASE_C)
        {
            bluePhaseB.SetActive(false);
            bothPhaseC.SetActive(true);

            SetBackground(PlayerEnum.DEFAULT);
        }

        else if (currentState == GameState.ENDGAME)
        {
            resultsScreen.SetActive(true);

            SetResults();
        }
    }
    
    private void SetBackground (PlayerEnum player)
    {
        switch (player)
        {
            case PlayerEnum.PLAYER_1:
                redBackground.SetActive(true);
                redCurtain.SetActive(true);

                blueBackground.SetActive(false);
                blueCurtain.SetActive(false);

                bothBackground.SetActive(false);
                bothCurtain.SetActive(false);
                break;

            case PlayerEnum.PLAYER_2:
                redBackground.SetActive(false);
                redCurtain.SetActive(false);

                blueBackground.SetActive(true);
                blueCurtain.SetActive(true);

                bothBackground.SetActive(false);
                bothCurtain.SetActive(false);
                break;

            case PlayerEnum.DEFAULT:
                redBackground.SetActive(false);
                redCurtain.SetActive(false);

                blueBackground.SetActive(false);
                blueCurtain.SetActive(false);

                bothBackground.SetActive(true);
                bothCurtain.SetActive(true);
                break;
        }
    }

    // Functionality for Buttons
    public void ButtonDeactivateCanvas(GameObject canvas)
    {
        canvas.SetActive(false);
    }

    private void SetResults()
    {
        int redScore = GameManager.Instance.player1.score;
        int blueScore = GameManager.Instance.player2.score;

        redResult.GetComponent<TMPro.TextMeshProUGUI>().text = redScore.ToString();
        blueResult.GetComponent<TMPro.TextMeshProUGUI>().text = blueScore.ToString();

        if (redScore > blueScore)
        {
            playerText.GetComponent<TMPro.TextMeshProUGUI>().text = "JUGADOR ROJO";
        }
        else if (blueScore > redScore)
        {
            playerText.GetComponent<TMPro.TextMeshProUGUI>().text = "JUGADOR AZUL";
        }
        else if (redScore == blueScore)
        {
            playerText.GetComponent<TMPro.TextMeshProUGUI>().text = "NADIE";
        }
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
