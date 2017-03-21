using UnityEngine;
using UnityEngine.UI;

public class InfoCtrl : MonoBehaviour {
	private Field _field;
	private PlayerCtrl _player;
	private SeaEnemy _seaEnemy;
	private GameObject _score;
	private GameObject _lvl;
	private GameObject _xn;
	private GameObject _full;
	private GameObject _time;

	public InfoCtrl(GameObject score, 
					GameObject lvl, 
					GameObject xn,
					GameObject full, 
					GameObject time, 
					Field field, 
					SeaEnemy seaEnemy,
					PlayerCtrl player) {

		_field = field;
		_player = player;
		_seaEnemy = seaEnemy;
		_score = score;
		_lvl = lvl;
		_xn = xn;
		_full = full;
		_time = time;

		init();
	}

	public void init() {
		_score.GetComponent<Text>().text = "Score: " + 0;
		_lvl.GetComponent<Text>().text = "Lvl: " + _seaEnemy.getSeaEnemiesCount();
		_xn.GetComponent<Text>().text = "Xn: " + _player.getCountLives();
		_full.GetComponent<Text>().text = "Full: " + 0 + "%";
		_time.GetComponent<Text>().text = "Time: " + 0;
	}

	public void update() {
		if(_player.getCountLives() == -1)
			_xn.GetComponent<Text>().text = "Xn: " + 0;
		else
			_xn.GetComponent<Text>().text = "Xn: " + _player.getCountLives();

		_score.GetComponent<Text>().text = "Score: " + _field.getScore();
		_lvl.GetComponent<Text>().text = "Lvl: " + _seaEnemy.getSeaEnemiesCount();
		_full.GetComponent<Text>().text = "Full: " + (int)_field.getSeaPercent() + "%";
	}
}
