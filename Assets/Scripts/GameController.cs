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
	private SeaEnemy _seaEnemyObj2;

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

	private const int WIN_PERCENT = 75;
	private bool _appIsPaused = false,
				_gameIsWon = false,
				_gameIsOver = false,
				_tapToPlay = false;


	void Start() {
		_fieldObj = new Field(_land, _sea);
		_playerObj = new PlayerCtrl(_player, _fieldObj);
		_seaEnemyObj = new SeaEnemy(_seaEnemy, _fieldObj, _playerObj);
		_seaEnemyObj2 = new SeaEnemy(_seaEnemy, _fieldObj, _playerObj);
		_landEnemyObj = new LandEnemy(_landEnemy, _fieldObj, _playerObj);
		_info = new InfoCtrl(_score, _xn, _full, _time, _fieldObj, _playerObj);

		//Time.timeScale = 0;
	}

	void Update() {
		if (!_gameIsOver) {
			_playerObj.move(_track);
		}
	}

	void FixedUpdate () {
		GameOver();

		if (!_gameIsOver) {
			_seaEnemyObj.move();
			_seaEnemyObj2.move();
			_landEnemyObj.move();
			_info.update();
		}

		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}

	public void StartGame() {
		Time.timeScale = 1;

		foreach(GameObject tapToPlay in GameObject.FindGameObjectsWithTag("TapToPlay"))
			tapToPlay.SetActive(false);
	}

	public void GameOver() {
		if ((_playerObj.IsSelfCrosed() || _seaEnemyObj.isHitTrackOrXonix() || _seaEnemyObj2.isHitTrackOrXonix() || _landEnemyObj.isHitXonix()) && !_gameIsOver) {
			_gameIsOver = true;
			_playerObj.decreaseLives();

			// --> stop game
			_playerObj.setDirection(0);
			Time.timeScale = 0;
			// <--

			print("game over");

			if (_playerObj.getCountLives() > 0) {
				print("You lose!");
				Time.timeScale = 0;
			}
		}
		if (_fieldObj.getSeaPercent() >= WIN_PERCENT && !_gameIsWon) {
			_gameIsWon = true;

			// --> stop game
			_playerObj.setDirection(0);
			Time.timeScale = 0;
			// <--

			print("You win!");
		}
	}

	public void OnApplicationQuit() {
		Application.Quit();
	}

	public void OnApplicationPaused() {
		if(!_appIsPaused) {
			Time.timeScale = 0;
			_appIsPaused = true;
		}
		else {
			Time.timeScale = 1;
			_appIsPaused = false;
		}
	}
}
