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

		Init();
	}

	public void Init() {
		_score.GetComponent<Text>().text = "Score: " + 0;
		_lvl.GetComponent<Text>().text = "Lvl: " + _seaEnemy.GetSeaEnemiesCount();
		_xn.GetComponent<Text>().text = "Xn: " + _player.GetCountLives();
		_full.GetComponent<Text>().text = "Full: " + 0 + "%";
		_time.GetComponent<Text>().text = "Time: " + _timer.GetTime();
	}

	public void UpdateInfo() {
		int lives = _player.GetCountLives() == -1 ? 0 : _player.GetCountLives();

		_xn.GetComponent<Text>().text = "Xn: " + lives;
		_score.GetComponent<Text>().text = "Score: " + _field.GetScore();
		_lvl.GetComponent<Text>().text = "Lvl: " + _seaEnemy.GetSeaEnemiesCount();
		_full.GetComponent<Text>().text = "Full: " + (int)_field.GetSeaPercent() + "%";
		_time.GetComponent<Text>().text = "Time: " + (int)_timer.GetTime();
	}
}
