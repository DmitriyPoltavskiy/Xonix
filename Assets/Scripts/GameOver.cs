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

	public void GameIsOver() {
		if (_player.IsSelfCrosed() || _seaEnemy.EnemiesHitTrackOrXonix() || _landEnemy.IsHitXonix() || _timer.TimeIsOver()) {
			openPanel();
			_player.DecreaseLives();

			if (_player.GetCountLives() < 1)
				_panel.GetComponentInChildren<Text>().text = "Game Over";
			else
				_panel.GetComponentInChildren<Text>().text = "Tap to retry";
		}
	}

	private void openPanel() {
		Time.timeScale = 0;
		_panel.SetActive(true);
	}

	public void ClosePanel() {
		Time.timeScale = 1;
		_panel.SetActive(false);
	}
}
