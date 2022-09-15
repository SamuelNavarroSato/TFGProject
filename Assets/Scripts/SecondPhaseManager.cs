using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPhaseManager : MonoBehaviour
{
    private CardManager cardManager;

    public Card[] currentDeck;
    public Card[] CheckedPairs;

    public int foundPairs = 0;
    public const int maximumScore = 30;
    public const int penalizationScore = 1;
    public int currentScore;

    void Awake()
    {
        GameManager.OnGameStateChanged += WhenGameStateChanges;
    }

    private void WhenGameStateChanges(GameState currentState)
    {
        if (currentState == GameState.PHASE_B_P1)
        {
            currentScore = maximumScore;
            currentDeck = cardManager.RetrieveDeck(PlayerEnum.PLAYER_2);
        }
        else if (currentState == GameState.PHASE_B_P2)
        {
            currentScore = maximumScore;
            currentDeck = cardManager.RetrieveDeck(PlayerEnum.PLAYER_1);
        }
        else
        {
            currentDeck = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cardManager = CardManager.Instance;
        CheckedPairs = new Card[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (foundPairs == cardManager.pairs)
        {
            foundPairs = 0; // Reset the Game
            CheckedPairs[0] = null;
            CheckedPairs[1] = null;

            // Awards points
            if (GameManager.Instance.state == GameState.PHASE_B_P1)
                GameManager.Instance.ModifyScore(PlayerEnum.PLAYER_1, currentScore);
            else if (GameManager.Instance.state == GameState.PHASE_B_P2)
                GameManager.Instance.ModifyScore(PlayerEnum.PLAYER_2, currentScore);

            StartCoroutine(FinishPhase());
        }
    }

    public void Check(Card card)
    {
        if (!card.isFound)
        {
            if (card != CheckedPairs[0] && card != CheckedPairs[1])
            {
                if (CheckedPairs[0] == null) // 1st Card picked: Array is empty
                {
                    CheckedPairs[0] = card;
                    CheckedPairs[0].isSelected = true;
                    return;
                }
                else if (CheckedPairs[1] == null) // 2nd Card picked and every 2 picks, then it should check below
                {
                    CheckedPairs[1] = card;
                    CheckedPairs[1].isSelected = true;
                }
                else // Every 3rd pick, new pair
                {
                    CheckedPairs[0].isSelected = false;
                    CheckedPairs[1].isSelected = false;
                    CheckedPairs[1] = null;

                    CheckedPairs[0] = card;
                    CheckedPairs[0].isSelected = true;
                    return;
                }

                if (IsItPairs())
                {
                    CheckedPairs[1].SetFound(true);
                    CheckedPairs[0].SetFound(true);

                    CheckedPairs[1].Flip(true);
                    CheckedPairs[0].Flip(true);

                    foundPairs++;
                }
                else
                {
                    SubstractScore();
                }
            }
            else if (card == CheckedPairs[0] && CheckedPairs[1] != null)
            {
                CheckedPairs[1].isSelected = false;
                CheckedPairs[1] = null;
            }
            else if (card == CheckedPairs[1])
            {
                CheckedPairs[1] = null;
                CheckedPairs[0].isSelected = false;
                CheckedPairs[0] = null;
                CheckedPairs[0] = card;
            }
        }
    }

    private void SubstractScore()
    {
        if (currentScore > 0)
            currentScore -= penalizationScore;
    }

    private bool IsItPairs()
    {
        if (CheckedPairs[0].pair == CheckedPairs[1])
        {
            return true;
        }
        return false;
    }

    IEnumerator FinishPhase()
    {
        yield return new WaitForSeconds(2);

        for (int i = 0; i < currentDeck.Length; i++)
        {
            currentDeck[i].transform.position = new Vector3(100f, 100f, 100f);
            currentDeck[i].wantsRotate = false;
        }

        GameManager.Instance.GoNextPhase();
    }
}
