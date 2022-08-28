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
        if (currentState == GameState.PHASE_A_P1 || currentState == GameState.PHASE_A_P2)
        {
            wordGameCanvas.SetActive(true);

            Setup();
        }
        else
        {
            wordGameCanvas.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
        if (updateDisplay)
        {
            counter.GetComponent<TMPro.TextMeshProUGUI>().text = (current).ToString();

            displayWord.GetComponent<TMPro.TextMeshProUGUI>().text = cardManager.GetCardText(current - 1);

            updateDisplay = false;
        }   
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= WhenGameStateChanges;
    }

    private void Setup()
    {
        displayWord.GetComponent<TMPro.TextMeshProUGUI>().text = cardManager.GetCardText(current - 1);
    }

    public void WordSubmission(GameObject submittedTextGO)
    {
        cardManager.deck[current + cardManager.pairs - 1].SetCardText(submittedTextGO.GetComponent<TMPro.TMP_InputField>().text);

        submittedTextGO.GetComponent<TMPro.TMP_InputField>().text = "";

        ++current;

        updateDisplay = true;
    }
}
