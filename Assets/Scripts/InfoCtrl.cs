using UnityEngine;
using UnityEngine.UI;

public class InfoCtrl : MonoBehaviour {
	private Field _field;
	private PlayerCtrl _player;
	private GameObject _score;
	private GameObject _xn;
	private GameObject _full;
	private GameObject _time;

	public InfoCtrl(GameObject score, 
					GameObject xn, 
					GameObject full, 
					GameObject time, 
					Field field, 
					PlayerCtrl player) {

		_field = field;
		_player = player;
		_score = score;
		_xn = xn;
		_full = full;
		_time = time;

		init();
	}

	public void init() {
		_score.GetComponent<Text>().text = "Score: " + 0;
		_xn.GetComponent<Text>().text = "Xn: " + _player.getCountLives();
		_full.GetComponent<Text>().text = "Full: " + 0 + "%";
		_time.GetComponent<Text>().text = "Time: " + 0;
	}

	public void update() {
		_score.GetComponent<Text>().text = "Score: " + _field.getScore();
		_xn.GetComponent<Text>().text = "Xn: " + _player.getCountLives();
		_full.GetComponent<Text>().text = "Full: " + (int)_field.getSeaPercent() + "%";
	}
}
