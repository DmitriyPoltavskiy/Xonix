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
	private GameObject _statePanel;
	[SerializeField]
	private GameObject _startGame;
	[SerializeField]
	private GameObject _paused;
	[SerializeField]
	private GameObject _nextLevel;
	[SerializeField]
	private GameObject _gameOver;

	private const int WIN_PERCENT = 75;
	private bool _appIsPaused = false,
				_appIsStarted = false,
				_gameIsWon = false,
				_gameIsOver = false,
				_tapToPlay = false;


	void Start() {
		initStates();

		_fieldObj = new Field(_land, _sea);
		_playerObj = new PlayerCtrl(_player, _fieldObj, _track);
		_seaEnemyObj = new SeaEnemy(_seaEnemy, _fieldObj, _playerObj);
		_landEnemyObj = new LandEnemy(_landEnemy, _fieldObj, _playerObj);
		_info = new InfoCtrl(_score, _xn, _full, _time, _fieldObj, _playerObj);
	}

	void Update() {
		if (!_gameIsOver && !_gameIsWon && !_appIsPaused && _appIsStarted) {
			_playerObj.move();
		}
	}

	void FixedUpdate () {
		GameOver();

		_info.update();

		updateStates();

		if (!_gameIsOver && !_gameIsWon && !_appIsPaused && _appIsStarted) {
			_seaEnemyObj.move();
			_landEnemyObj.move();
		}

		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}

	void initStates() {
		_statePanel.SetActive(true);
		_startGame.SetActive(true);
	}

	void updateStates() {
		if (_appIsPaused) {
			_statePanel.SetActive(true);
			_paused.SetActive(true);
		}
		if (_gameIsWon) {
			_statePanel.SetActive(true);
			_nextLevel.SetActive(true);
		}
		if (_gameIsOver) {
			_statePanel.SetActive(true);
			_gameOver.SetActive(true);
		}
		
	}

	public void StartGame() {
		_startGame.SetActive(false);
		_statePanel.SetActive(false);

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

	public void GameOver() {
		if ((_playerObj.IsSelfCrosed() || _seaEnemyObj.isHitTrackOrXonix() || _landEnemyObj.isHitXonix()) && !_gameIsOver) {
			_gameIsOver = true;
			_playerObj.decreaseLives();

			print("game over");

			if (_playerObj.getCountLives() > 0) {
				print("You lose!");
			}
		}
		if (_fieldObj.getSeaPercent() >= WIN_PERCENT && !_gameIsWon) {
			_gameIsWon = true;

			print("You win!");
		}
	}

	public void OnApplicationQuit() {
		Application.Quit();
	}
}
