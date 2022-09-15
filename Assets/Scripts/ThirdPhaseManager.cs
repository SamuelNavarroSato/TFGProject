using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPhaseManager : MonoBehaviour
{
    private CardManager cardManager;

    public GameObject canvas;
    public Transform redPosition;
    public Transform bluePosition;

    public const int winnerPrize = 6;
    public const float threshold = 0.8f;

    void Awake()
    {
        GameManager.OnGameStateChanged += WhenGameStateChanges;
    }

    private void WhenGameStateChanges(GameState currentState)
    {
        if (currentState == GameState.PHASE_C)
        {
            canvas.SetActive(true);
            cardManager.SetupPhaseC(PlayerEnum.PLAYER_1, redPosition);
            cardManager.SetupPhaseC(PlayerEnum.PLAYER_2, bluePosition);
        }
        else
        {
            canvas.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cardManager = CardManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup()
    {
        
    }

    private void CleanUp()
    {
        Card[] cleanUp = cardManager.RetrieveDeck(PlayerEnum.PLAYER_1);
        Card[] cleanUp2 = cardManager.RetrieveDeck(PlayerEnum.PLAYER_2);
        for (int i = 0; i < cleanUp.Length; i++)
        {
            cleanUp[i].Hide();
            cleanUp2[i].Hide();
        }
    }

    IEnumerator FinishMemoryGame()
    {
        yield return new WaitForSeconds(2);

        if (GameManager.Instance.state == GameState.PHASE_C)
        {
            CleanUp();

            GameManager.Instance.GoNextPhase();
        }
    }

    public void ButtonSelectWinner(int winner)
    {
        if (winner == 1)
            SetWinner(PlayerEnum.PLAYER_1);
        else if (winner == 2)
            SetWinner(PlayerEnum.PLAYER_2);
    }
    public void SetWinner(PlayerEnum player)
    {
        GameManager.Instance.ModifyScore(player, winnerPrize);
        StartCoroutine(FinishMemoryGame());
    }
}
