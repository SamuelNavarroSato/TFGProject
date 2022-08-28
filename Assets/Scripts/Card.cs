using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

    private Quaternion desiredRotation;

    [SerializeField] public GameObject textComponent;

    public string value = "";

    public Card pair = null;

    public bool main = false; // Only true if it has the keyword

	// Use this for initialization
	void Start ()
    {
        desiredRotation = transform.rotation;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Rotate();
    }

    public void Flip()
    {
        desiredRotation = desiredRotation * Quaternion.Euler(180, 0, 0); // Rotates on X axis
    }

    private void Rotate()
    {
        int scalingFactor = 7; // Higher = Faster
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * scalingFactor);
    }

    private void OnMouseUp()
    {
        Flip(); 
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

    public void SetCardText(string value)
    {
        textComponent.GetComponent<TMPro.TextMeshPro>().text = value;
    }

    public void Destroy()
    {
        DestroyImmediate(this, true);
    }
}
