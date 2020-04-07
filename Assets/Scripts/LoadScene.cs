using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour {

    public void SceneLoader(int SceneIndex)
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex == 4 && SceneIndex != 0)
        {
            int g = PlayerPrefs.GetInt("PreviousPageIndex");
            SceneManager.LoadScene(g);
        }
        else if(currentIndex == 4 && SceneIndex == 0) {
            PlayerPrefs.SetInt("PreviousPageIndex", currentIndex);
            SceneManager.LoadScene(SceneIndex);
        }
        else
        {
            PlayerPrefs.SetInt("PreviousPageIndex", currentIndex);
            SceneManager.LoadScene(SceneIndex);
        }
    }
}
