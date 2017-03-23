using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	float _nextMoveTime = 0,
		  _deltaMoveTime = 0.04f;

	[SerializeField]
	private GameObject _playerInLand;
	[SerializeField]
	private GameObject _playerInSea;
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

	private Timer _timerObj;

	void Start() {
		Time.timeScale = 0;
		_startGame.SetActive(true);

		_timerObj = new Timer();

		_fieldObj = new Field(_land, _sea);
		_fieldObj.init();

		_seaEnemyObj = new SeaEnemy(_seaEnemy, _fieldObj);
		_seaEnemies.Add(_seaEnemyObj);
		_seaEnemyObj.initSeaEnemies(_seaEnemies);

		_playerObj = new PlayerCtrl(_playerInLand, _playerInSea, _fieldObj, _track, _seaEnemies);

		_seaEnemyObj.init(_playerObj);

		_landEnemyObj = new LandEnemy(_landEnemy, _fieldObj, _playerObj);

		_nextLevelObj = new NextLevel(_nextLevel, _fieldObj);

		_info = new InfoCtrl(_score, _lvl, _xn, _full, _time, _fieldObj, _seaEnemyObj, _playerObj, _timerObj);

		_gameOverObj = new GameOver(_gameOver, _playerObj, _seaEnemyObj, _seaEnemies, _landEnemyObj, _timerObj);
	}

	void Update() {
		_playerObj.getDirection();
		_info.update();

		while (_nextMoveTime <= Time.time) {
			_nextMoveTime = Time.time + _deltaMoveTime;


			_playerObj.move();
			_landEnemyObj.move();
			foreach (SeaEnemy seaEnemy in _seaEnemies) {
				seaEnemy.move();
			}

			_nextLevelObj.wonLevel();
			_gameOverObj.gameOver();
		}

		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}

	public void StartGame() {
		Time.timeScale = 1;
		_startGame.SetActive(false);
	}

	public void OnApplicationPaused() {
		if(Time.timeScale == 0) {
			Time.timeScale = 1;
			_paused.SetActive(false);
		}
		else {
			Time.timeScale = 0;
			_paused.SetActive(true);
		}
	}

	public void closePanel() {
		if (_gameOver.activeSelf) {
			if (_playerObj.getCountLives() >= 0) {
				_fieldObj.clearTrack();
				_gameOverObj.closePanel();

				_playerObj.destroy();
				_playerObj.init();
				_playerObj.updateSelfCrosed();

				_landEnemyObj.destroy();
				_landEnemyObj.init();

				_timerObj.UpdateTime();
			}
			else {
				_gameOverObj.closePanel();

				_fieldObj.destroy();
				_fieldObj.init();
				_fieldObj.fillTrackArea(_seaEnemies);

				_playerObj.destroy();
				_playerObj.init();
				_playerObj.updateSelfCrosed();
				_playerObj.setCountLives(3);

				_landEnemyObj.destroy();
				_landEnemyObj.init();

				for (int i = _seaEnemies.Count - 1; i >= 0; i--) {
					_seaEnemies[i].isHitTrackOrXonix();
					if (_seaEnemies.Count > 1) {
						_seaEnemies[i].destroy();
						_seaEnemies.RemoveAt(i);
					}
				}

				_timerObj.UpdateTime();
				_fieldObj.SetScore(0);
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

			_timerObj.UpdateTime();
		}
	}

	void OnApplicationQuit() {
		Application.Quit();
	}
}
