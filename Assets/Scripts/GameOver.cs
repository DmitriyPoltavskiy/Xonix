using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
	private GameObject _panel;
	private PlayerCtrl _player;
	private SeaEnemy _seaEnemy;
	private LandEnemy _landEnemy;
	private List<SeaEnemy> _seaEnemies;
	private Timer _timer;

	public GameOver(GameObject panel, PlayerCtrl player, SeaEnemy seaEnemy, List<SeaEnemy> seaEnemies, LandEnemy landEnemy, Timer timer) {
		_panel = panel;
		_player = player;
		_seaEnemy = seaEnemy;
		_seaEnemies = seaEnemies;
		_landEnemy = landEnemy;
		_timer = timer;
	}

	public void gameOver() {
		if (_player.isSelfCrosed() || _seaEnemy.EnemiesHitTrackOrXonix() || _landEnemy.isHitXonix() || _timer.TimeIsOver()) {
			openPanel();
			_player.decreaseLives();

			if (_player.getCountLives() < 0)
				_panel.GetComponentInChildren<Text>().text = "Game Over";
			else
				_panel.GetComponentInChildren<Text>().text = "Tap to retry";
		}
	}

	private void openPanel() {
		Time.timeScale = 0;
		_panel.SetActive(true);
	}

	public void closePanel() {
		Time.timeScale = 1;
		_panel.SetActive(false);
	}
}
