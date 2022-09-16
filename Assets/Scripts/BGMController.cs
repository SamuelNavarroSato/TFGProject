using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    public AudioClip buttonSound;

    public AudioSource buttonSource;
    public AudioSource source;

    public bool playAudio = true;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound()
    {
        buttonSource.PlayOneShot(buttonSound);
    }

    public void ToggleSound()
    {
        if (playAudio == true)
        {
            playAudio = false;
            source.Pause();
        }
        else
        {
            playAudio = true;
            source.Play();
        }
    }
}
