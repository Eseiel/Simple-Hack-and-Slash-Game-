using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicScript : MonoBehaviour
{
    public static MusicScript instance;

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField] GameObject Music, MusicSlider, MusicToggle;

    // Start is called before the first frame update
    void Start()
    {
        Music.SetActive(true);
    }

    void Update() 
    {
        if (MusicSlider != null && MusicToggle != null)
        {
            Music.SetActive(MusicToggle.GetComponent<Toggle>().isOn);
            Music.GetComponent<AudioSource>().volume = MusicSlider.GetComponent<Slider>().value;
        }
    }



}
