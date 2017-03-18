using UnityEngine;

public class PlayerCtrl : MonoBehaviour {
	private GameObject _player;
	private GameObject _track;
	private GameObject _playerInLand;
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

	public PlayerCtrl(GameObject playerInLand, Field field, GameObject track) {
		_field = field;
		_track = track;
		_playerInLand = playerInLand;
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
		_direction = getDirection();

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
			_direction = 0;
			_isWater = false;
			_field.fillTrackArea();
		}
		if (_field.field[_x, _y].tag == "Sea") {
			_isWater = true;
			_field.field[_x, _y] = Instantiate(_track, new Vector3(_x, _y, 10), Quaternion.identity);
		}
	}

	int getDirection() {
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

		return _direction;
	}

	public void setDirection(int direction) {
		_direction = direction;
	}

	public bool IsSelfCrosed() {
		return _isSelfCross;
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
}
