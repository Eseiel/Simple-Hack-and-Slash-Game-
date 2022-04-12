using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] GameObject MusicToggle, back, slider, quit, start, option;

    private void Start()
    {
        if (MusicToggle != null &&
            slider != null)
        {
            MusicToggle.SetActive(false);
            slider.SetActive(false);
        }
    }

    public void StartClick()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void OptionClick()
    {
        if (MusicToggle != null &&
            back != null &&
            slider != null &&
            quit != null &&
            start != null &&
            option != null)
        {
            MusicToggle.SetActive(true);
            slider.SetActive(true);
            back.SetActive(true);
            start.SetActive(false);
            option.SetActive(false);
            quit.SetActive(false);
        }
    }
    
    public void QuitClick()
    {
        Application.Quit();
    }

    public void BackClick()
    {
        MusicToggle.SetActive(false);
        slider.SetActive(false);
        back.SetActive(false);
        start.SetActive(true);
        option.SetActive(true);
        quit.SetActive(true);
    }

    public void ReturnClick()
    {
        SceneManager.LoadScene("TitleScene");
    }

    
}
