﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreKeeper : MonoBehaviour {

	public static int score;

	private Text myText;

	void Start() {
		Reset ();
		myText = GetComponent<Text> ();
	}

	public void Score (int points) {
		score += points;
		myText.text = score.ToString ();
	}

	public static void Reset () {
		score = 0;
	}
}
