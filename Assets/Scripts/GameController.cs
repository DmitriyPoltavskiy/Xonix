using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	float _nextMoveTime = 0,
		  _deltaMoveTime = 0.02f;

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
	private GameObject _lvl;
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

	private GameOver _gameOverObj;

	private bool _appIsPaused = false,
				_appIsStarted = false,
				_slow = false,
				_tapToPlay = false;

	void Start() {
		_startGame.SetActive(true);

		_fieldObj = new Field(_land, _sea);
		_fieldObj.init();

		_seaEnemyObj = new SeaEnemy(_seaEnemy, _fieldObj);
		_seaEnemies.Add(_seaEnemyObj);
		_seaEnemyObj.initSeaEnemies(_seaEnemies);

		_playerObj = new PlayerCtrl(_player, _fieldObj, _track, _seaEnemies);
		_playerObj.init();

		_seaEnemyObj.init(_playerObj);

		_landEnemyObj = new LandEnemy(_landEnemy, _fieldObj, _playerObj);
		_landEnemyObj.init();

		_info = new InfoCtrl(_score, _lvl, _xn, _full, _time, _fieldObj, _seaEnemyObj, _playerObj);

		_nextLevelObj = new NextLevel(_nextLevel, _fieldObj);

		_gameOverObj = new GameOver(_gameOver, _playerObj, _seaEnemyObj, _seaEnemies, _landEnemyObj);
	}

	void Update() {
		_playerObj.getDirection();

		while (_nextMoveTime <= Time.time) {

			_nextLevelObj.wonLevel();

			_gameOverObj.gameOver();

			_info.update();

			updateStates();

			_nextMoveTime = Time.time + _deltaMoveTime;
			if (!_gameOverObj.getGameIsOver() && !_nextLevelObj.nextLevel() && !_appIsPaused && _appIsStarted) {
				_playerObj.move();

				foreach (SeaEnemy seaEnemy in _seaEnemies) {
					seaEnemy.move();
				}

				_landEnemyObj.move();
				_slow = true;
			}
		}

		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}

	//void FixedUpdate() {
	//	if (!_gameOverObj.getGameIsOver() && !_nextLevelObj.nextLevel() && !_appIsPaused && _appIsStarted) {
	//		//_playerObj.move();
	//		//if (!_slow) {
	//		//	foreach (SeaEnemy seaEnemy in _seaEnemies)
	//		//		seaEnemy.move();
	//		//	_landEnemyObj.move();
	//		//	_slow = true;
	//		//}
	//		//else
	//		//	_slow = false;
	//	}
	//}

	void updateStates() {
		if (_appIsPaused) {
			_paused.SetActive(true);
		}
		else if(!_appIsPaused) {
			_paused.SetActive(false);
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
		if (_gameOver.activeSelf) {
			if (_playerObj.getCountLives() >= 0) {
				_fieldObj.clearTrack();
				_gameOverObj.closePanel();

				_playerObj.destroy();
				_playerObj.init();

				_landEnemyObj.destroy();
				_landEnemyObj.init();
			}
			else {
				//_gameOverObj.SetGameIsOver(true);
				_gameOverObj.closePanel();
				//_startGame.SetActive(true);


				_fieldObj.destroy();
				_fieldObj.init();
				_fieldObj.fillTrackArea(_seaEnemies);

				_playerObj.destroy();
				_playerObj.init();
				_playerObj.updateSelfCrosed();
				_playerObj.setCountLives(3);

				_landEnemyObj.destroy();
				_landEnemyObj.init();

				foreach (SeaEnemy seaEnemy in _seaEnemies) {
					seaEnemy.isHitTrackOrXonix();
					if (_seaEnemies.Count > 1)
						seaEnemy.destroy();
				}
			}
		}

		if (_nextLevel.activeSelf) {
			_fieldObj.destroy();
			_fieldObj.init();
			_fieldObj.fillTrackArea(_seaEnemies);

			_playerObj.destroy();
			_playerObj.init();

			_landEnemyObj.destroy();
			_landEnemyObj.init();

			SeaEnemy seaEnemyObj = new SeaEnemy(_seaEnemy, _fieldObj);
			_seaEnemies.Add(seaEnemyObj);
			foreach (SeaEnemy seaEnemy in _seaEnemies) {
				seaEnemy.destroy();
				seaEnemy.init(_playerObj);
			}

			_nextLevelObj.closePanel();
		}
	}

	void OnApplicationQuit() {
		Application.Quit();
	}
}
