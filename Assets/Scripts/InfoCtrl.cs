using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoCtrl : MonoBehaviour {
	public GameObject Score;
	public GameObject Xn;
	public GameObject Full;
	public GameObject Time;

	void Start () {
		Score.GetComponent<Text>().text = "Score: " + 0;
		Xn.GetComponent<Text>().text = "Xn: " + PlayerCtrl.Instance.getCountLives();
		Full.GetComponent<Text>().text = "Full: " + 0 + "%";
		Time.GetComponent<Text>().text = "Time: " + 0;
	}

	void Update () {
		Score.GetComponent<Text>().text = "Score: " + Field.Instance.Score;
		Xn.GetComponent<Text>().text = "Xn: " + PlayerCtrl.Instance.getCountLives();
		Full.GetComponent<Text>().text = "Full: " + (int)Field.Instance.getSeaPercent() + "%";
	}

	//public void setScore(int score) {
	//	Score.GetComponent<Text>().text = "Score: " + score;
	//}

	//public void setXn(int xn) {
	//	Score.GetComponent<Text>().text = "Score: " + xn;
	//}

	//public void setFull(int full) {
	//	Score.GetComponent<Text>().text = "Score: " + full;
	//}

	//public void setTime(int time) {
	//	Score.GetComponent<Text>().text = "Score: " + score;
	//}
}
