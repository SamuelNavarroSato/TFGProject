using UnityEngine;
using System.Collections;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;

    public const int cardColumns = 4;
    public const int cardRows = 3;
    public const float offsetX = 5f;
    public const float offsetY = 3f;

    public Transform original;

    [SerializeField] public PlayerManager player1;
    [SerializeField] public PlayerManager player2;

    public int pairs = 0;

    private string[] words;

    void Awake()
    {
        Instance = this;

        GameManager.OnGameStateChanged += WhenGameStateChanges;

        LoadWords();
    }

    private void WhenGameStateChanges(GameState currentState)
    {
        if (currentState == GameState.PHASE_A_P1)
        {
            SetupDeck(RetrieveDeck(PlayerEnum.PLAYER_1));
        }
        else if (currentState == GameState.PHASE_A_P2)
        {
            SetupDeck(RetrieveDeck(PlayerEnum.PLAYER_2));
        }
        else if (currentState == GameState.PHASE_B_P1)
        {
            SetupPhaseB(RetrieveDeck(PlayerEnum.PLAYER_2)); // Player 1 has Player 2's deck on display
        }
        else if (currentState == GameState.PHASE_B_P2)
        {
            SetupPhaseB(RetrieveDeck(PlayerEnum.PLAYER_1));
        }
    }
    
    private void Start ()
    {

    }
	
	void Update ()
    {
	
	}

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= WhenGameStateChanges;
    }

    public void SetupDeck(Card[] deck)
    {
        for (int i = 0; i < deck.Length; i++)
        {
            deck[i] = Instantiate(deck[i]);
            deck[i].Hide();
        }


        pairs = deck.Length / 2;

        int[] rnd = new int[pairs];

        for (int i = 0; i < pairs; i++)
        {
            rnd[i] = RecursiveRandom(rnd, i); 
        }


        for (int i = 0; i < pairs; i++)
        {
            deck[i].Setup(words[rnd[i]], true); // Main card, having a word
            deck[i].SetPair(deck[i + pairs]);

            deck[i + pairs].Setup("", false); // Other card of the pair, being the one which has to be edited
            deck[i + pairs].SetPair(deck[i]);
        }
    }

    private int RecursiveRandom(int[] array, int position)
    {
        int rand = Random.Range(0, words.Length);

        if (position >= 1)
        {
            for (int j = 0; j < position - 1; j++)
            {
                if (array[j] == rand)
                {
                    rand = RecursiveRandom(array, position);
                }
            }
        }

        return rand;
    }

    public Card[] RetrieveDeck(PlayerEnum player)
    {
        Card[] ret;

        if (player == PlayerEnum.PLAYER_1)
        {
            ret = player1.deck;
        }
        else if (player == PlayerEnum.PLAYER_2)
        {
            ret = player2.deck;
        }
        else // default
        {
            ret = new Card[0];
        }

        return ret;
    }

    private void SetupPhaseB(Card[] deck)
    {
        Vector3 startPos = original.transform.position;
        
        ShuffleDeck(deck);

        for (int i = 0; i < cardColumns; i++)
        {
            for (int j = 0; j < cardRows; j++)
            {
                float posX = (offsetX * i) + original.position.x;
                float posY = (offsetY * -j) + original.position.y;
                deck[i + (j * 4)].transform.position = new Vector3(posX, posY, original.position.z);
                deck[i + (j * 4)].wantsRotate = true;
            }
        }
    }

    public void SetupPhaseC(PlayerEnum player, Transform trans)
    {
        Card[] deck = RetrieveDeck(player);
        Vector3 startPos = trans.transform.position;

        Color32 color = Color.black;

        if (player == PlayerEnum.PLAYER_1) // RED
            color = new Color32(191, 30, 19, 255);
        else if (player == PlayerEnum.PLAYER_2) // BLUE
            color = new Color32(0, 94, 190, 255);

        ShuffleDeck(deck);

        for (int i = 0; i < deck.Length; i++)
        {
            if (deck[i].main == true) // Find the first Main Card to show on top
            {
                // Set Main Card
                deck[i].transform.position = new Vector3(trans.position.x, trans.position.y, trans.position.z);
                deck[i].textComponent.GetComponent<TMPro.TextMeshPro>().color = color;

                // Set Main Card's Pair position
                deck[i].pair.transform.position = new Vector3(trans.position.x * 0.7f, trans.position.y - (offsetY * 0.6f), trans.position.z - 1);
                deck[i].pair.textComponent.GetComponent<TMPro.TextMeshPro>().color = color;

                break;
            }
        }
    }

    public void ShuffleDeck(Card[] deck) // Shuffles the deck by interchanging the cards position randomly in increased number
    {
        for (int i = 0; i < deck.Length - 1; i++)
        {
            int rnd = Random.Range(i, deck.Length);
            Card card = deck[rnd];
            deck[rnd] = deck[i];
            deck[i] = card;
        }
    }

    public void FlipAllCards(Card[] deck)
    {
        for (int i = 0; i < deck.Length; i++)
        {
            deck[i].Flip(true);
        }
    }

    public string GetCardText(PlayerEnum player, int position)
    {
        Card[] ret = RetrieveDeck(player);

        return ret[position].value;
    }

    public void SetCardText(PlayerEnum player, int position, string value)
    {
        RetrieveDeck(player)[position].value = value;
        RetrieveDeck(player)[position].SetCardText(value);
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
        words[13] = "HAMBRE";
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
        words[26] = "SUAVE";
        words[27] = "ALEGRE";
        words[28] = "PEQUEÑO";
        words[29] = "RUGOSO";
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
        words[44] = "SUDOR";
        words[45] = "JUEGO";
        words[46] = "MEDIANOCHE";
        words[47] = "REGALO";
        words[48] = "PRIMERO";
        words[49] = "DESAYUNO";

    }
}