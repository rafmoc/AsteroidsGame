using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    void Start()
    {
        GameObject score = GameObject.Find("Score");
        

        if (PlayerPrefs.GetInt("score") != 0)
        {
            score.GetComponent<Text>().text = "Last Score: " + PlayerPrefs.GetInt("score");
        }
        else
        {
            score.GetComponent<Text>().text = "Last Score: 0";
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
