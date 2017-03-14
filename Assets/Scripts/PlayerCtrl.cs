using UnityEngine;

public class PlayerCtrl : MonoBehaviour {
	public GameObject Player;
	public GameObject Track;

	private int _x,
				_y,
				_z,
				_direction,
				_count_lives = 3;

	private bool _isWater,
				_isSelfCross;

	void Start () {
		init();
	}

	void Update () {
		move();
	}

	void init() {
		_y = Field.HEIGHT;
		_x = Field.WIDTH / 2 - 1;
		_z = 0;
		_direction = 0;
		_isWater = false;
		Player = Instantiate(Player, new Vector3(_x, _y, _z), Quaternion.identity) as GameObject;
	}

	void move() {
		_direction = getDirection();

		if (_direction == 2) _x--;
		if (_direction == -2) _x++;
		if (_direction == 1) _y++;
		if (_direction == -1) _y--;

		if (_y > Field.HEIGHT - 1) _y = Field.HEIGHT - 1;
		if (_x > Field.WIDTH - 1) _x = Field.WIDTH - 1;
		if (_y < 0) _y = 0;
		if (_x < 0) _x = 0;

		Player.transform.position = new Vector3(_x, _y, _z);

		_isSelfCross = Field.Instance.field[_x, _y].tag == "Track";

		if (Field.Instance.field[_x, _y].tag == "Land" && _isWater) {
			_direction = 0;
			_isWater = false;
			Field.Instance.fillTrackArea();
			print(Field.Instance.getSeaPercent());
		}
		if (Field.Instance.field[_x, _y].tag == "Sea") {
			_isWater = true;
			Field.Instance.field[_x, _y] = Instantiate(Track, new Vector3(_x, _y, 10), Quaternion.identity);
		}
	}

	int getDirection() {
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
}
