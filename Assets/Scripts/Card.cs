using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

    private Quaternion desiredRotation;

    [SerializeField] public GameObject textComponent;
    public ParticleSystem sparkles;

    public string value = "";

    public Card pair = null;

    public bool main = false; // Only true if it has the keyword
    public bool isSelected = false;
    public bool isFound = false;
    public bool wantsRotate = false;

    // Use this for initialization
    void Start()
    {
        isSelected = false;
        isFound = false;
        desiredRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    { 
        if (!isFound)
        {
            if (isSelected)
                Flip(true);
            else
                Flip(false);
        }

        if (wantsRotate)
            Rotate();
    }

    public void Flip(bool faceUp)
    {
        if (faceUp)
            desiredRotation = Quaternion.Euler(0, 0, 0); // Rotates on X axis
        else
            desiredRotation = Quaternion.Euler(180, 0, 0);
    }

    private void Rotate()
    {
        int scalingFactor = 7; // Higher = Faster
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * scalingFactor);
    }

    private void OnMouseUp()
    {
        GameObject.FindGameObjectWithTag("PairGame").GetComponent<SecondPhaseManager>().Check(this);
    }

    public void Setup(string value, bool main)
    {
        this.value = value;
        this.main = main;

        SetCardText(value);
    }

    public void SetPair(Card pair)
    {
        this.pair = pair;
    }

    public void SetFound(bool found)
    {
        if (found)
        {
            isFound = true;
            textComponent.GetComponent<TMPro.TextMeshPro>().color = new Color32(50, 210, 50, 255);
        }
        else
        {
            isFound = false;
        }
    }

    public void SetCardText(string value)
    {
        textComponent.GetComponent<TMPro.TextMeshPro>().text = value;
    }

    public void Hide()
    {
        transform.position = new Vector3(100f, 100f, 100f);
    }

    public void SparkleIt()
    {
        sparkles.Play();
    }

    public void Destroy()
    {
        DestroyImmediate(this, true);
    }
}
