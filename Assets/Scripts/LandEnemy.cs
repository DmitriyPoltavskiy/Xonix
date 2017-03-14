using UnityEngine;

public class LandEnemy : MonoBehaviour {
	public GameObject Land_enemy;

	private int _dx, _dy, _x, _y, _z = 0;

	void Start() {
		init();
	}

	void FixedUpdate() {
		move();
	}

	void init() {
		do {
			_x = Random.Range(0, Field.WIDTH - 1);
			_y = Random.Range(0, Field.HEIGHT - 1);
		}
		while (
			_x <= 0 ||
			_y <= 0 ||
			_x >= Field.WIDTH - 1 ||
			_y >= Field.HEIGHT - 1 ||
			Field.Instance.field[_x, _y].tag == "Sea"
		);

		_dx = Random.Range(0, 1) == 0 ? 1 : -1;
		_dy = Random.Range(0, 1) == 0 ? 1 : -1;

		Land_enemy = Instantiate(Land_enemy, new Vector3(_x, _y, _z), Quaternion.identity) as GameObject;
	}

	void updateDXandDY() {
		if (_x <= 0 || _x >= Field.WIDTH - 1) _dx = -_dx;
		if (_y <= 0 || _y >= Field.HEIGHT - 1) _dy = -_dy;

		if (Field.Instance.field[_x + _dx, _y].tag == "Sea") _dx = -_dx;
		if (Field.Instance.field[_x, _y + _dy].tag == "Sea") _dy = -_dy;
	}

	void move() {
		updateDXandDY();
		_x += _dx;
		_y += _dy;	
		Land_enemy.transform.position = new Vector3(_x, _y, _z);
	}
}
