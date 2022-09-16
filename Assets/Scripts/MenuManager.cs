using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject volumeOn;
    public GameObject volumeOff;

    public void ButtonStartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ButtonExit()
    {
        Application.Quit();
    }

    public void ButtonToggleVolume()
    {
        if (volumeOn.activeInHierarchy)
        {
            volumeOff.SetActive(true);
            volumeOn.SetActive(false);
        }
        else if (volumeOff.activeInHierarchy)
        {
            volumeOn.SetActive(true);
            volumeOff.SetActive(false);
        }
    }
}
