using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairGameManager : MonoBehaviour
{
    [SerializeField] public CardManager cardManager;

    public Card[] currentDeck;
    public Card[] CheckedPairs;

    public int foundPairs = 0;

    void Awake()
    {
        GameManager.OnGameStateChanged += WhenGameStateChanges;
    }

    private void WhenGameStateChanges(GameState currentState)
    {
        if (currentState == GameState.PHASE_B_P1)
        {
            currentDeck = cardManager.RetrieveDeck(PlayerEnum.PLAYER_2);
        }
        else if (currentState == GameState.PHASE_B_P2)
        {
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

            StartCoroutine(FinishPairGame());
        }
    }

    public void Check(Card card)
    {
        if (!card.isFound)
        {
            if (card != CheckedPairs[0] && card != CheckedPairs[1])
            {
                if (CheckedPairs[0] == null) // 1st Pick: Array is empty
                {
                    CheckedPairs[0] = card;
                    CheckedPairs[0].isSelected = true;
                    return;
                }
                else if (CheckedPairs[1] == null) // 2nd Pick and every 2 picks, then it should check below
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

    private bool IsItPairs()
    {
        if (CheckedPairs[0].pair == CheckedPairs[1])
        {
            return true;
        }
        return false;
    }

    IEnumerator FinishPairGame()
    {
        yield return new WaitForSeconds(2);

        for (int i = 0; i < currentDeck.Length; i++)
        {
            currentDeck[i].transform.position = new Vector3(100f, 100f, 100f);
            currentDeck[i].wantsRotate = false;
        }

        if (GameManager.Instance.state == GameState.PHASE_B_P1)
        {
            GameManager.Instance.UpdateGameState(GameState.PHASE_B_P2);
        }
        else if (GameManager.Instance.state == GameState.PHASE_B_P2)
        {
            GameManager.Instance.UpdateGameState(GameState.PHASE_C_P1);
        }
    }
}
