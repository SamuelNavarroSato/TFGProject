using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGameManager : MonoBehaviour
{
    [SerializeField] public GameObject wordGameCanvas;

    [SerializeField] public CardManager cardManager;

    public GameObject counter;
    [SerializeField] public GameObject displayWord;
    public GameObject inputWord;

    public int[] positions;
    public string[] words;

    private int current = 1;
    private bool updateDisplay = false;
    

    void Awake()
    {
        GameManager.OnGameStateChanged += WhenGameStateChanges;
    }

    private void WhenGameStateChanges(GameState currentState)
    {
        if (currentState == GameState.PHASE_A_P1)
        {
            wordGameCanvas.SetActive(true);

            Setup(PlayerEnum.PLAYER_1);
        }
        else if (currentState == GameState.PHASE_A_P2)
        {
            wordGameCanvas.SetActive(true);

            Setup(PlayerEnum.PLAYER_2);
        }
        else
        {
            wordGameCanvas.SetActive(false);
        }
    }

    void Start()
    {

    }

    void Update()
    {
        if (wordGameCanvas)
        {
            if (current > cardManager.pairs)
            {
                if (GameManager.Instance.state == GameState.PHASE_A_P1)
                {
                    current = 1;
                    GameManager.Instance.UpdateGameState(GameState.PHASE_A_P2);
                }
                else if (GameManager.Instance.state == GameState.PHASE_A_P2)
                {
                    current = 1;
                    GameManager.Instance.UpdateGameState(GameState.PHASE_B_P1);
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (updateDisplay) // Sets the text component for the new word
        {
            counter.GetComponent<TMPro.TextMeshProUGUI>().text = (current).ToString();

            if (GameManager.Instance.state == GameState.PHASE_A_P1)
                displayWord.GetComponent<TMPro.TextMeshProUGUI>().text = cardManager.GetCardText(PlayerEnum.PLAYER_1, current - 1);
            else if (GameManager.Instance.state == GameState.PHASE_A_P2)
                displayWord.GetComponent<TMPro.TextMeshProUGUI>().text = cardManager.GetCardText(PlayerEnum.PLAYER_2, current - 1);

            updateDisplay = false;
        }   
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= WhenGameStateChanges;
    }

    private void Setup(PlayerEnum player)
    {
        displayWord.GetComponent<TMPro.TextMeshProUGUI>().text = cardManager.GetCardText(player, current - 1);
    }

    public void WordSubmission(GameObject submittedTextGO) // Called every time the player inputs a word
    {
        if (submittedTextGO.GetComponent<TMPro.TMP_InputField>().text.Length <= 1) // Minimum size set to avoid missclicks.
        {
            return;
        }
        else
        {
            if (GameManager.Instance.state == GameState.PHASE_A_P1)
                cardManager.SetCardText(PlayerEnum.PLAYER_1, current + cardManager.pairs - 1, submittedTextGO.GetComponent<TMPro.TMP_InputField>().text);
            else if (GameManager.Instance.state == GameState.PHASE_A_P2)
                cardManager.SetCardText(PlayerEnum.PLAYER_2, current + cardManager.pairs - 1, submittedTextGO.GetComponent<TMPro.TMP_InputField>().text);

            submittedTextGO.GetComponent<TMPro.TMP_InputField>().text = "";

            ++current;

            updateDisplay = true;
        }
    }
}
