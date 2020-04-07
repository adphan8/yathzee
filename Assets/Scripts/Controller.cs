using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

    public static Controller instance = null;
    public float key = 0;
    public Game game;

    void Awake()
    {
        if (instance == null)
        { 
            DontDestroyOnLoad(gameObject);
            key = Random.Range(0f, 1f);
            instance = this;
        } else if (instance != this)
            Destroy(gameObject);        
    }
    public Game getGame() {
        return this.game;
    }

    public void StartGame()
    {
        Debug.Log("Start new game");
        List<string> names = new List<string>();
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("player name"))
        {
            try
            { 
                string name = go.GetComponent<InputField>().text;                            
                if (name != "")
                {
                    Debug.Log("Testing w/name: " + name);
                    names.Add(name);
                }
            }
            catch
            {
                Debug.Log("Failed to add");
            }
        }
        game = new Game(names);
        Debug.Log("Create game object: " + game);
    }
}
