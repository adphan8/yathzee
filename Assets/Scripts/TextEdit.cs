using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEdit : MonoBehaviour {

    public Text t;

	// Use this for initialization
	void Start () {
        t.text = "Players: ";
        foreach (Player player in Controller.instance.game.GetPlayers())
            t.text = t.text + player.GetName() + " ";
	}
}
