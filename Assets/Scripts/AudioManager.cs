using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip UIButtonSound;
    public AudioClip UIInputSound;
    public AudioClip cardFlipSound;
    public AudioClip cardCorrectSound;
    public AudioClip phaseEndSound;
    public AudioClip instructionsSound;
    public AudioClip victorySound;

    public AudioSource source;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(int type)
    {
        switch(type)
        {
            case 0:
                source.PlayOneShot(UIButtonSound);
                break;
            case 1:
                source.PlayOneShot(UIInputSound);
                break;
            case 2:
                source.PlayOneShot(cardFlipSound);
                break;
            case 3:
                source.PlayOneShot(cardCorrectSound);
                break;
            case 4:
                source.PlayOneShot(phaseEndSound);
                break;
            case 5:
                source.PlayOneShot(instructionsSound);
                break;
            case 6:
                source.PlayOneShot(victorySound);
                break;
        }
    }
}

