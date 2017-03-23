using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	float _nextMoveTime = 0,
		_deltaMoveTime = 0.04f,
		_levelDuration = 60;

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

		_timerObj = new Timer(_levelDuration);

		_fieldObj = new Field(_land, _sea);
		_fieldObj.Init();

		_seaEnemyObj = new SeaEnemy(_seaEnemy, _fieldObj);
		_seaEnemies.Add(_seaEnemyObj);
		_seaEnemyObj.InitSeaEnemies(_seaEnemies);

		_playerObj = new PlayerCtrl(_playerInLand, _playerInSea, _fieldObj, _track, _seaEnemies);

		_seaEnemyObj.Init(_playerObj);

		_landEnemyObj = new LandEnemy(_landEnemy, _fieldObj, _playerObj);

		_nextLevelObj = new NextLevel(_nextLevel, _fieldObj);

		_info = new InfoCtrl(_score, _lvl, _xn, _full, _time, _fieldObj, _seaEnemyObj, _playerObj, _timerObj);

		_gameOverObj = new GameOver(_gameOver, _playerObj, _seaEnemyObj, _seaEnemies, _landEnemyObj, _timerObj);
	}

	void Update() {
		_playerObj.GetDirection();
		_info.UpdateInfo();

		while (Time.time >= _nextMoveTime) {
			_nextMoveTime = Time.time + _deltaMoveTime;

			_playerObj.Move();
			_landEnemyObj.Move();
			foreach (SeaEnemy seaEnemy in _seaEnemies) {
				seaEnemy.Move();
			}

			_nextLevelObj.WonLevel();
			_gameOverObj.GameIsOver();
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

	public void ClosePanel() {
		if (_gameOver.activeSelf) {
			if (_playerObj.GetCountLives() > 0) {
				_fieldObj.DeleteTrack();
				_gameOverObj.ClosePanel();

				_playerObj.Destroy();
				_playerObj.Init();
				_playerObj.UpdateSelfCrosed();

				_landEnemyObj.Destroy();
				_landEnemyObj.Init();

				_timerObj.UpdateTime();
			}
			else {
				_gameOverObj.ClosePanel();

				_fieldObj.Destroy();
				_fieldObj.Init();
				_fieldObj.FillTrackArea(_seaEnemies);

				_playerObj.Destroy();
				_playerObj.Init();
				_playerObj.UpdateSelfCrosed();
				_playerObj.SetCountLives(3);

				_landEnemyObj.Destroy();
				_landEnemyObj.Init();

				for (int i = _seaEnemies.Count - 1; i >= 0; i--) {
					_seaEnemies[i].IsHitTrackOrXonix();
					if (_seaEnemies.Count > 1) {
						_seaEnemies[i].Destroy();
						_seaEnemies.RemoveAt(i);
					}
				}

				_timerObj.UpdateTime();
				_fieldObj.SetScore(0);
			}
		}

		if (_nextLevel.activeSelf) {
			_fieldObj.Destroy();
			_fieldObj.Init();
			_fieldObj.FillTrackArea(_seaEnemies);

			_playerObj.Destroy();
			_playerObj.Init();
			_playerObj.AddCountLives(1);

			_landEnemyObj.Destroy();
			_landEnemyObj.Init();

			SeaEnemy seaEnemyObj = new SeaEnemy(_seaEnemy, _fieldObj);
			_seaEnemies.Add(seaEnemyObj);
			foreach (SeaEnemy seaEnemy in _seaEnemies) {
				seaEnemy.Destroy();
				seaEnemy.Init(_playerObj);
			}

			_nextLevelObj.ClosePanel();

			_timerObj.UpdateTime();
		}
	}

	void OnApplicationQuit() {
		Application.Quit();
	}
}
