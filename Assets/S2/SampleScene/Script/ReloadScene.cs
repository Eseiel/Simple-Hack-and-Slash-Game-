using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player") == null)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
