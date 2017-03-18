using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	[SerializeField]
	private GameObject _player;
	[SerializeField]
	private GameObject _track;
	private PlayerCtrl _playerObj;

	[SerializeField]
	private GameObject _seaEnemy;
	private SeaEnemy _seaEnemyObj;
	private List<SeaEnemy> _seaEnemies = new List<SeaEnemy>();

	[SerializeField]
	private GameObject _landEnemy;
	private LandEnemy _landEnemyObj;

	[SerializeField]
	private GameObject _sea;
	[SerializeField]
	private GameObject _land;
	private GameObject _field;
	private Field _fieldObj;

	[SerializeField]
	private GameObject _score;
	[SerializeField]
	private GameObject _xn;
	[SerializeField]
	private GameObject _full;
	[SerializeField]
	private GameObject _time;
	private InfoCtrl _info;

	[SerializeField]
	private GameObject _startGame;
	[SerializeField]
	private GameObject _paused;
	[SerializeField]
	private GameObject _gameOver;
	[SerializeField]
	private GameObject _nextLevel;
	private NextLevel _nextLevelObj;

	private const int WIN_PERCENT = 60;
	private bool _appIsPaused = false,
				_appIsStarted = false,
				_gameIsOver = false,
				_tapToPlay = false;

	void Start() {
		_startGame.SetActive(true);

		_fieldObj = new Field(_land, _sea);
		_fieldObj.init();

		_playerObj = new PlayerCtrl(_player, _fieldObj, _track);
		_playerObj.init();

		_seaEnemyObj = new SeaEnemy(_seaEnemy, _fieldObj, _playerObj);
		_seaEnemies.Add(_seaEnemyObj);
		foreach(SeaEnemy seaEnemy in _seaEnemies)
			_seaEnemyObj.init();

		_landEnemyObj = new LandEnemy(_landEnemy, _fieldObj, _playerObj);
		_landEnemyObj.init();

		_info = new InfoCtrl(_score, _xn, _full, _time, _fieldObj, _playerObj);

		_nextLevelObj = new NextLevel(_nextLevel, _fieldObj);
	}

	void Update() {
		if (!_gameIsOver && !_nextLevelObj.nextLevel() && !_appIsPaused && _appIsStarted) {
			_playerObj.move();
		}
	}

	void FixedUpdate () {
		GameOver();

		_info.update();

		updateStates();

		if (!_gameIsOver && !_nextLevelObj.nextLevel() && !_appIsPaused && _appIsStarted) {
			foreach (SeaEnemy seaEnemy in _seaEnemies)
				seaEnemy.move();
			_landEnemyObj.move();
		}

		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}

	void updateStates() {
		if (_appIsPaused) {
			_paused.SetActive(true);
		}
		else if(!_appIsPaused) {
			_paused.SetActive(false);
		}
		if (_gameIsOver) {
			_gameOver.SetActive(true);
		}
	}

	public void StartGame() {
		_startGame.SetActive(false);

		_appIsStarted = true;
	}

	public void OnApplicationPaused() {
		if (!_appIsPaused) {
			_appIsPaused = true;
		}
		else {
			_appIsPaused = false;
		}
	}

	public void closePanel() {
		_fieldObj.destroy();
		_fieldObj.init();
		_fieldObj.fillTrackArea();

		_playerObj.destroy();
		_playerObj.init();

		_landEnemyObj.destroy();
		_landEnemyObj.init();

		SeaEnemy seaEnemyObj = new SeaEnemy(_seaEnemy, _fieldObj, _playerObj);
		_seaEnemies.Add(seaEnemyObj);
		foreach (SeaEnemy seaEnemy in _seaEnemies) {
			seaEnemy.destroy();
			seaEnemy.init();
		}

		_nextLevelObj.closePanel();
	}

	public void GameOver() {
		if ((_playerObj.IsSelfCrosed() || _seaEnemyObj.EnemiesHitTrackOrXonix(_seaEnemies) || _landEnemyObj.isHitXonix()) && !_gameIsOver) {
			_gameIsOver = true;
			_playerObj.decreaseLives();

			print("game over");

			if (_playerObj.getCountLives() < 0) {
				print("You lose!");
			}
		}
		if (_fieldObj.getSeaPercent() >= WIN_PERCENT && !_nextLevelObj.nextLevel()) {
			print("You win!");

			_nextLevelObj.displayPanel();
		}
	}

	void OnApplicationQuit() {
		Application.Quit();
	}
}
