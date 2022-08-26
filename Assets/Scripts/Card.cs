using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

    private Quaternion desiredRotation;

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
}
