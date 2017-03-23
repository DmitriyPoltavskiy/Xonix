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

		init();
	}

	public void init() {
		_y = Field.HEIGHT - 1;
		_x = Field.WIDTH / 2 - 1;
		_z = 0;
		_direction = 0;
		_isWater = false;

		_player = Instantiate(_playerInLand, new Vector3(_x, _y, _z), Quaternion.identity) as GameObject;
	}

	public void destroy() {
		Destroy(_player);
	}

	public void move() {
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

			destroy();
			_player = Instantiate(_playerInLand, new Vector3(_x, _y, _z), Quaternion.identity) as GameObject;

			_direction = 0;
			_isWater = false;
			if(Time.timeScale != 0) {
				_field.fillTrackArea(_seaEnemies);
				_field.clearTrack();
			}
		}
		if (_field.field[_x, _y].tag == "Sea") {
			_isWater = true;

			destroy();
			_player = Instantiate(_playerInSea, new Vector3(_x, _y, _z), Quaternion.identity) as GameObject;

			Destroy(_field.field[_x, _y]);
			_field.field[_x, _y] = Instantiate(_track, new Vector3(_x, _y, 10), Quaternion.identity);
		}
	}

	public void getDirection() {
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

	public void setDirection(int direction) {
		_direction = direction;
	}

	public bool isSelfCrosed() {
		return _isSelfCross;
	}

	public void updateSelfCrosed() {
		_isSelfCross = _field.field[_x, _y].tag == "Track";
	}

	public int getX() {
		return _x;
	}

	public int getY() {
		return _y;
	}

	public void decreaseLives() {
		_count_lives--;
	}

	public int getCountLives() {
		return _count_lives;
	}

	public void setCountLives(int lives) {
		_count_lives = lives;
	}
}
