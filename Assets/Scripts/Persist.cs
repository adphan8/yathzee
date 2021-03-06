﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persist : MonoBehaviour {


	// Use this for initialization
	void Start () {

    }

    private static Persist instance = null;
    public static Persist Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

}
