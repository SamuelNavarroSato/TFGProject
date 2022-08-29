using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairGameManager : MonoBehaviour
{
    [SerializeField] public CardManager cardManager;

    public Card[] currentDeck;
    public Card[] CheckedPairs;

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
        if (GameManager.Instance.state == GameState.PHASE_B_P1 || GameManager.Instance.state == GameState.PHASE_B_P2)
        {
            Check();
        }
    }

    private void Check()
    {
        if (currentDeck.Length != 0 && currentDeck[0] != null && CheckedPairs[1] == null)
        {
            for (int i = 0; i < currentDeck.Length; i++)
            {
                if (currentDeck[i].isSelected && currentDeck[i] != CheckedPairs[0] && currentDeck[i] != CheckedPairs[1])
                {
                    if (CheckedPairs[0] == null)
                    {
                        CheckedPairs[0] = currentDeck[i];
                    }
                    else
                    {
                        CheckedPairs[1] = currentDeck[i];
                    }

                    if (IsItPairs())
                    {
                        CheckedPairs[1].isFound = true;
                        CheckedPairs[0].isFound = true;

                        StartCoroutine(ResetPosition(true));
                    }
                    else if (!IsItPairs())
                    {
                        Debug.Log("Cards are not pair");


                        StartCoroutine(ResetPosition(false));
                    }
                }
            }
        }
        else
        {
            if (GameManager.Instance.state == GameState.PHASE_A_P1)
                GameManager.Instance.UpdateGameState(GameState.PHASE_A_P2);
            else if (GameManager.Instance.state == GameState.PHASE_A_P2)
                GameManager.Instance.UpdateGameState(GameState.PHASE_B_P1);
        }
    }

    IEnumerator ResetPosition(bool found)
    {
        if (found)
        {
            yield return new WaitForSeconds(3f);
            CheckedPairs[0].isSelected = false;
            CheckedPairs[1].isSelected = false;
        }
        else
        {
            yield return new WaitForSeconds(4f);

            CheckedPairs[0].isSelected = false;
            CheckedPairs[1].isSelected = false;

            CheckedPairs[0].isFound = false;
            CheckedPairs[1].isFound = false;
        }

        CheckedPairs[0] = null;
        CheckedPairs[1] = null;

        Debug.Log("End of coroutine");
        yield return null;
    }

    private bool IsItPairs()
    {
        if (CheckedPairs[0].pair == CheckedPairs[1])
        {
            Debug.Log("Cards are pair");
            return true;
        }
        return false;
    }
}
