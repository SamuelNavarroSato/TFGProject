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

    public int wordsLeft = 6;

    public int[] positions;
    public string[] words;
    

    void Awake()
    {
        GameManager.OnGameStateChanged += WhenGameStateChanges;
    }

    private void WhenGameStateChanges(GameState currentState)
    {
        if (currentState == GameState.PHASE_A_P1 || currentState == GameState.PHASE_A_P2)
        {
            wordGameCanvas.SetActive(true);

        }
        else
        {
            wordGameCanvas.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Setup();

    }

    // Update is called once per frame
    void Update()
    {
        if (wordGameCanvas)
        {

        }
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= WhenGameStateChanges;
    }

    private void Setup()
    {
        displayWord.GetComponent<TMPro.TextMeshProUGUI>().text = cardManager.GetCardText(0);
    }
}
