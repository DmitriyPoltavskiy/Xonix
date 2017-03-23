using UnityEngine;
using UnityEngine.UI;

public class InfoCtrl : MonoBehaviour {
	private Field _field;
	private PlayerCtrl _player;
	private SeaEnemy _seaEnemy;
	private Timer _timer;
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
					PlayerCtrl player,
					Timer timer) {

		_field = field;
		_player = player;
		_seaEnemy = seaEnemy;
		_score = score;
		_lvl = lvl;
		_xn = xn;
		_full = full;
		_time = time;
		_timer = timer;

		init();
	}

	public void init() {
		_score.GetComponent<Text>().text = "Score: " + 0;
		_lvl.GetComponent<Text>().text = "Lvl: " + _seaEnemy.getSeaEnemiesCount();
		_xn.GetComponent<Text>().text = "Xn: " + _player.getCountLives();
		_full.GetComponent<Text>().text = "Full: " + 0 + "%";
		_time.GetComponent<Text>().text = "Time: " + _timer.GetTime();
	}

	public void update() {
		int lives = _player.getCountLives() == -1 ? 0 : _player.getCountLives();

		_xn.GetComponent<Text>().text = "Xn: " + lives;
		_score.GetComponent<Text>().text = "Score: " + _field.getScore();
		_lvl.GetComponent<Text>().text = "Lvl: " + _seaEnemy.getSeaEnemiesCount();
		_full.GetComponent<Text>().text = "Full: " + (int)_field.getSeaPercent() + "%";
		_time.GetComponent<Text>().text = "Time: " + (int)_timer.GetTime();
	}
}
