using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameManager : MonoBehaviour
{
    [SerializeField] public CardManager cardManager;

    public GameObject canvas;
    public Transform originalPos;
    private Card[] guessDeck;

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
            guessDeck = cardManager.SetupMemoryGame(PlayerEnum.PLAYER_2, originalPos);
        }
        else
        {
            canvas.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup()
    {
        
    }

    /*public void MemorySubmission(GameObject submittedTextGO)
    {
        string word = submittedTextGO.GetComponent<TMPro.TMP_InputField>().text;

        if (word.Length <= 1) // Minimum size set to avoid missclicks.
        {
            return;
        }
        else
        {
            for (int i = 0; i < guessDeck.Length; i++)
            {
                if (GetSimilarity(word, guessDeck[i].value) >= threshold)
                {
                    submittedTextGO.GetComponent<TMPro.TMP_InputField>().text = "";
                    StartCoroutine(FinishMemoryGame());
                    break;
                }
            }

            submittedTextGO.GetComponent<TMPro.TMP_InputField>().text = "";
        }
    }*/

    private void CleanUp(PlayerEnum player)
    {
        Card[] cleanUp = cardManager.RetrieveDeck(player);
        for(int i = 0; i < cleanUp.Length; i++)
        {
            cleanUp[i].Hide();
        }
    }

    IEnumerator FinishMemoryGame()
    {
        yield return new WaitForSeconds(2);

        if (GameManager.Instance.state == GameState.PHASE_C)
        {
            CleanUp(PlayerEnum.PLAYER_2);

            GameManager.Instance.UpdateGameState(GameState.ENDGAME);
        }
    }

    private float GetSimilarity(string source, string target)
    {
        if ((source == null) || (target == null)) return 0.0f;
        if ((source.Length == 0) || (target.Length == 0)) return 0.0f;
        if (source == target) return 1.0f;

        int stepsToSame = LevenshteinDistance(source, target);
        return (1.0f - (stepsToSame / Mathf.Max(source.Length, target.Length)));
    }

    private static int LevenshteinDistance(string source, string target)
    {

        if (source == target) return 0;
        if (source.Length == 0) return target.Length;
        if (target.Length == 0) return source.Length;

        int[] v0 = new int[target.Length + 1];
        int[] v1 = new int[target.Length + 1];

        for (int i = 0; i < v0.Length; i++)
            v0[i] = i;

        for (int i = 0; i < source.Length; i++)
        {
            v1[0] = i + 1;

            for (int j = 0; j < target.Length; j++)
            {
                var cost = (source[i] == target[j]) ? 0 : 1;
                v1[j + 1] = Mathf.Min(v1[j] + 1, Mathf.Min(v0[j + 1] + 1, v0[j] + cost));
            }

            for (int j = 0; j < v0.Length; j++)
                v0[j] = v1[j];
        }

        return v1[target.Length];
    }
}
