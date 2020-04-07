using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSave : MonoBehaviour {

    public InputField p1;
    public InputField p2;
    public InputField p3;
    public InputField p4;
    public InputField p5;

    public void SaveText()
    {
        PlayerPrefs.SetString("P1", p1.text);
        PlayerPrefs.SetString("P2", p2.text);
        PlayerPrefs.SetString("P3", p3.text);
        PlayerPrefs.SetString("P4", p4.text);
        PlayerPrefs.SetString("P5", p5.text);
    }
}
