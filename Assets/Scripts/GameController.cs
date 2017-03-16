using UnityEngine;

public class GameController : MonoBehaviour {
	private const int WIN_PERCENT = 75;
	private bool _appIsPaused = false,
				_gameIsWon = false,
				_gameIsOver = false,
				_tapToPlay = false;


	void Start() {
		Time.timeScale = 0;
	}

	void Update () {
		//GameOver();

		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}

	public void StartGame() {
		Time.timeScale = 1;

		foreach(GameObject tapToPlay in GameObject.FindGameObjectsWithTag("TapToPlay"))
			tapToPlay.SetActive(false);
	}

	public void GameOver() {
		if ((PlayerCtrl.Instance.IsSelfCrosed() || SeaEnemy.Instance.isHitTrackOrXonix() || LandEnemy.Instance.isHitXonix()) && !_gameIsOver) {
			_gameIsOver = true;
			PlayerCtrl.Instance.decreaseLives();

			// --> stop game
			PlayerCtrl.Instance.setDirection(0);
			Time.timeScale = 0;
			// <--

			print("game over");

			if (PlayerCtrl.Instance.getCountLives() > 0) {
				print("You lose!");
				Time.timeScale = 0;
			}
		}
		if (Field.Instance.getSeaPercent() >= WIN_PERCENT && !_gameIsWon) {
			_gameIsWon = true;

			// --> stop game
			PlayerCtrl.Instance.setDirection(0);
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
