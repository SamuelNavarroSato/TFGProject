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

    // Use this for initialization
    private void Start ()
    {
        Vector3 startPos = original.transform.position;

        ShuffleDeck(deck);

        for (int i = 0; i < cardColumns; i++)
        {
            for (int j = 0; j < cardRows; j++)
            {
                Card card = Instantiate(deck[i + (j * 4)]);

                float posX = (offsetX * i) + original.position.x;
                float posY = (offsetY * -j) + original.position.y;
                card.transform.position = new Vector3(posX, posY, original.position.z);

                deck[i + (j * 4)] = card;
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

    public void FlipAllCards()
    {
        for (int i = 0; i < deck.Length; i++)
        {
            deck[i].Flip();
        }
    }
}
