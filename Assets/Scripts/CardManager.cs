using UnityEngine;
using System.Collections;

public class CardManager : MonoBehaviour
{
    public const int cardColumns = 4;
    public const int cardRows = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2f;

    public Transform original;
    public Card[] deck;

    // Use this for initialization
    private void Start ()
    {
        Vector3 startPos = original.transform.position;

        ShuffleDeck(deck);

        for (int i = 0; i < cardColumns; i++)
        {
            for (int j = 0; j < cardRows; j++)
            {
                Card card = Instantiate(deck[i + j]);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
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
}
