using System.Collections;
using System;
using System.Timers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class VictorySequence : MonoBehaviour
{
	Timer t = new Timer();

	private void Start() {
		StartCoroutine(DelayLoadLevel(10));
	}

	IEnumerator DelayLoadLevel(float seconds) {
		yield return new WaitForSeconds(4);
		SceneManager.LoadScene("MenuScene");
	}
}
