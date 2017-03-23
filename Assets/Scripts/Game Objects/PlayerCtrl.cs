using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {
	private GameObject _player;
	private GameObject _track;
	private GameObject _playerInLand;
	private GameObject _playerInSea;
	List<SeaEnemy> _seaEnemies;
	private Field _field;

	private int _x,
				_y,
				_z,
				_direction,
				_count_lives = 3;
	

	private bool _isWater,
				_inWater,
				_inLand,
				_isSelfCross;

	public PlayerCtrl(GameObject playerInLand, GameObject playerInSea, Field field, GameObject track, List<SeaEnemy> seaEnemies) {
		_field = field;
		_track = track;
		_playerInLand = playerInLand;
		_seaEnemies = seaEnemies;
		_playerInSea = playerInSea;

		Init();
	}

	public void Init() {
		_y = Field.HEIGHT - 1;
		_x = Field.WIDTH / 2 - 1;
		_z = 0;
		_direction = 0;
		_isWater = false;

		_player = Instantiate(_playerInLand, new Vector3(_x, _y, _z), Quaternion.identity) as GameObject;
	}

	public void Destroy() {
		Destroy(_player);
	}

	public void Move() {
		if (_direction == 2) _x--;
		if (_direction == -2) _x++;
		if (_direction == 1) _y++;
		if (_direction == -1) _y--;

		if (_y > Field.HEIGHT - 1) _y = Field.HEIGHT - 1;
		if (_x > Field.WIDTH - 1) _x = Field.WIDTH - 1;
		if (_y < 0) _y = 0;
		if (_x < 0) _x = 0;

		_player.transform.position = new Vector3(_x, _y, _z);

		_isSelfCross = _field.field[_x, _y].tag == "Track";

		if (_field.field[_x, _y].tag == "Land" && _isWater) {

			Destroy();
			_player = Instantiate(_playerInLand, new Vector3(_x, _y, _z), Quaternion.identity) as GameObject;

			_direction = 0;
			_isWater = false;
			if(Time.timeScale != 0) {
				_field.FillTrackArea(_seaEnemies);
				_field.DeleteTrack();
			}
		}
		if (_field.field[_x, _y].tag == "Sea") {
			_isWater = true;

			Destroy();
			_player = Instantiate(_playerInSea, new Vector3(_x, _y, _z), Quaternion.identity) as GameObject;

			Destroy(_field.field[_x, _y]);
			_field.field[_x, _y] = Instantiate(_track, new Vector3(_x, _y, 10), Quaternion.identity);
		}
	}

	public void GetDirection() {
		if (Input.touchCount > 0 && Input.touchCount < 2 && Input.GetTouch(0).phase == TouchPhase.Moved) {
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

			if (Mathf.Abs(touchDeltaPosition.x) > Mathf.Abs(touchDeltaPosition.y)) {
				if (touchDeltaPosition.x < 0) {
					_direction = 2;
				}
				else {
					_direction = -2;
				}
			}
			else {
				if (touchDeltaPosition.y < 0) {
					_direction = -1;
				}
				else {
					_direction = 1;
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.LeftArrow))
			_direction = 2;
		if (Input.GetKeyDown(KeyCode.RightArrow))
			_direction = -2;
		if (Input.GetKeyDown(KeyCode.UpArrow))
			_direction = 1;
		if (Input.GetKeyDown(KeyCode.DownArrow))
			_direction = -1;
	}

	//public void SetDirection(int direction) {
	//	_direction = direction;
	//}

	public bool IsSelfCrosed() {
		return _isSelfCross;
	}

	public void UpdateSelfCrosed() {
		_isSelfCross = _field.field[_x, _y].tag == "Track";
	}

	public int GetX() {
		return _x;
	}

	public int GetY() {
		return _y;
	}

	public void DecreaseLives() {
		_count_lives--;
	}

	public int GetCountLives() {
		return _count_lives;
	}

	public void SetCountLives(int lives) {
		_count_lives = lives;
	}

	public void AddCountLives(int lives) {
		_count_lives += lives;
	}
}
