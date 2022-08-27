using UnityEngine;
using System.Collections;

public class CardManager : MonoBehaviour
{
    public const int cardColumns = 4;
    public const int cardRows = 3;
    public const float offsetX = 5f;
    public const float offsetY = 3f;

    public Transform original;
    public Card[] deck;

    void Awake()
    {
        GameManager.OnGameStateChanged += WhenGameStateChanges;
    }

    private void WhenGameStateChanges(GameState currentState)
    {
        if (currentState == GameState.PHASE_A_P2)
        {
            SetupPairGame();
        }
    }

    // Use this for initialization
    private void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= WhenGameStateChanges;
    }

    private void SetupPairGame()
    {
        Vector3 startPos = original.transform.position;

        ShuffleDeck(deck);

        for (int i = 0; i < cardColumns; i++)
        {
            for (int j = 0; j < cardRows; j++)
            {
                Card card = Instantiate(deck[i + (j * 4)]);

                card.Setup("Name", true);

                float posX = (offsetX * i) + original.position.x;
                float posY = (offsetY * -j) + original.position.y;
                card.transform.position = new Vector3(posX, posY, original.position.z);

                deck[i + (j * 4)] = card;
            }
        }
    }

    private void ShuffleDeck(Card[] deck) // Shuffles the deck by interchanging the cards position randomly in increased number
    {
        for (int i = 0; i < deck.Length - 1; i++)
        {
            int rnd = Random.Range(i, deck.Length);
            Card card = deck[rnd];
            deck[rnd] = deck[i];
            deck[i] = card;
        }
    }

    public void FlipAllCards()
    {
        for (int i = 0; i < deck.Length; i++)
        {
            deck[i].Flip();
        }
    }
}