using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour {

    public Slider volumeControl;
    AudioSource ourMusic;// = obj.GetComponent<AudioSource>();
	Toggle mute;
	Toggle gameMute;
	Toggle backgroundMute;

    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Game Music");
        ourMusic = obj.GetComponent<AudioSource>();
		mute = GameObject.FindGameObjectWithTag("Mute").GetComponent<Toggle>();
		gameMute = GameObject.FindGameObjectWithTag("GameMute").GetComponent<Toggle>();
		backgroundMute = GameObject.FindGameObjectWithTag("BackgoundMute").GetComponent<Toggle>();
		PlayerPrefs.SetInt("mute", 0);
		PlayerPrefs.SetInt("gamemute", 0);
		PlayerPrefs.SetInt("backgroundmute", 0);
	}
    
    void Update () {
		if(mute.isOn == true)
		{
			PlayerPrefs.SetInt("mute", 1);
			ourMusic.volume = 0;
		}
		else
		{
			PlayerPrefs.SetInt("mute", 0);
			changeVolume();
		}
		if(gameMute.isOn == true)
			PlayerPrefs.SetInt("gamemute", 1);
		else
			PlayerPrefs.SetInt("gamemute", 0);
		if(backgroundMute.isOn == true)
		{
			PlayerPrefs.SetInt("backgroundmute", 1);
			ourMusic.volume = 0;
		}
		else
		{
			PlayerPrefs.SetInt("backgroundmute", 0);
			changeVolume();
		}
	}
	
	public void changeVolume()
	{
		ourMusic.volume = volumeControl.value;
	}
	
	/*
	mute =GameObject.FindGameObjectWithTag("Mute");
		gameMute = GameObject.FindGameObjectWithTag("GameMute");
		backgroundMute = GameObject.FindGameObjectWithTag("BackgroundMute");
	mute.GetComponent<Toggle>().isOn = false;
		gameMute.GetComponent<Toggle>().isOn = false;
		backgroundMute.GetComponent<Toggle>().isOn = false;
	*/
}
