using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
	private GameObject _panel;
	private PlayerCtrl _player;
	private SeaEnemy _seaEnemy;
	private LandEnemy _landEnemy;
	private List<SeaEnemy> _seaEnemies;
	private bool _gameIsOver = false;

	public GameOver(GameObject panel, PlayerCtrl player, SeaEnemy seaEnemy, List<SeaEnemy> seaEnemies, LandEnemy landEnemy) {
		_panel = panel;
		_player = player;
		_seaEnemy = seaEnemy;
		_seaEnemies = seaEnemies;
		_landEnemy = landEnemy;
	}

	public void gameOver() {
		if ((_player.isSelfCrosed() || _seaEnemy.EnemiesHitTrackOrXonix() || _landEnemy.isHitXonix()) && !_gameIsOver) {
			print(_player.isSelfCrosed());
			openPanel();
			_player.decreaseLives();

			if (_player.getCountLives() < 0)
				_panel.GetComponentInChildren<Text>().text = "Game Over";
			else
				_panel.GetComponentInChildren<Text>().text = "Tap to retry";
		}
	}

	public bool getGameIsOver() {
		return _gameIsOver;
	}

	public void SetGameIsOver(bool gameIsOver) {
		_gameIsOver = gameIsOver;
	}

	private void openPanel() {
		_gameIsOver = true;
		_panel.SetActive(true);
	}

	public void closePanel() {
		_gameIsOver = false;
		_panel.SetActive(false);
		_player.updateSelfCrosed();
	}
}
