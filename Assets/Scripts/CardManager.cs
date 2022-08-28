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

    public int pairs = 0;

    private string[] words;

    void Awake()
    {
        GameManager.OnGameStateChanged += WhenGameStateChanges;

        LoadWords();
    }

    private void WhenGameStateChanges(GameState currentState)
    {
        if (currentState == GameState.PHASE_A_P1)
        {
        }
        if (currentState == GameState.PHASE_B_P1)
        {
            SetupPairGame();
        }
    }

    // Use this for initialization
    private void Start ()
    {
        SetupDeck();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= WhenGameStateChanges;
    }

    public void SetupDeck()
    {
        for (int i = 0; i < deck.Length; i++)
        {
            deck[i] = Instantiate(deck[i]);            
            deck[i].transform.position = new Vector3(100f, 100f, 100f);
        }
        

        pairs = deck.Length / 2;


        for (int i = 0; i < pairs; i++)
        {
            int rnd = Random.Range(i, words.Length);

            deck[i].Setup(words[rnd], true); // Main card, having a word
            deck[i].SetPair(deck[i + pairs]);

            deck[i + pairs].Setup("", false); // Other card of the pair, being the one which has to be edited
            deck[i + pairs].SetPair(deck[i]);
        }
    }

    private void SetupPairGame()
    {
        Vector3 startPos = original.transform.position;

        for (int i = 0; i < cardColumns; i++)
        {
            for (int j = 0; j < cardRows; j++)
            {
                float posX = (offsetX * i) + original.position.x;
                float posY = (offsetY * -j) + original.position.y;
                deck[i + (j * 4)].transform.position = new Vector3(posX, posY, original.position.z);
            }
        }

        ShuffleDeck();
    }

    public void ShuffleDeck() // Shuffles the deck by interchanging the cards position randomly in increased number
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

    public string GetCardText(int position)
    {
        return deck[position].value;
    }
   
    private void LoadWords()
    {
        words = new string[50];
        words[0] = "VACACIONES"; // Temas de Conversación
        words[1] = "VIAJE";
        words[2] = "MÚSICA";
        words[3] = "LIBRO";
        words[4] = "POSTRE";
        words[5] = "HOBBY";
        words[6] = "DEPORTE";
        words[7] = "TRABAJO";
        words[8] = "AMIG@";
        words[9] = "ANIMAL";
        words[10] = "SERIE";
        words[11] = "DIVERTIDO"; // Sentimientos
        words[12] = "PÁNICO";
        words[13] = "SUSTO";
        words[14] = "TRISTE";
        words[15] = "BUENO"; // Adjetivos
        words[16] = "FRÍO";
        words[17] = "CALIENTE";
        words[18] = "RAPIDO";
        words[19] = "LENTO";
        words[20] = "FÁCIL";
        words[21] = "DIFERENTE";
        words[22] = "FUERTE";
        words[23] = "DULCE";
        words[24] = "SALADO";
        words[25] = "GRANDE";
        words[26] = "REFRESCANTE";
        words[27] = "ALEGRE";
        words[28] = "PEQUEÑO";
        words[29] = "EXTRAÑO";
        words[30] = "ESPECIAL";
        words[31] = "SALVAJE";
        words[32] = "REDONDO";
        words[33] = "ITALIANO";
        words[34] = "ROJO"; // COLORES
        words[35] = "NARANJA";
        words[36] = "AZUL";
        words[37] = "VERDE";
        words[38] = "VIOLETA";
        words[39] = "MITAD"; // MISCELANEA
        words[40] = "CIUDAD";
        words[41] = "GAFAS";
        words[42] = "NAVIDAD";
        words[43] = "CUMPLEAÑOS";
        words[44] = "FAMOS@";
        words[45] = "JUEGO";
        words[46] = "MEDIANOCHE";
        words[47] = "REGALO";
        words[48] = "PRIMERO";
        words[49] = "DESAYUNO";

    }
}