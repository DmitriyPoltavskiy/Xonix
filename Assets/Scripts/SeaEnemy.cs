using UnityEngine;

public class SeaEnemy : MonoBehaviour {
    public GameObject seaEnemy;

    private int _countSeaEnemy = 1;

    private int _dx,
				_dy,
				_x,
				_y,
				_z;

    void Start () {
		init();
	}

	void FixedUpdate() {
		move();
	}

	void init() {
		_x = Random.Range(2, Field.WIDTH - 2);
		_y = Random.Range(2, Field.HEIGHT - 2);

		_dx = Random.Range(0, 1) == 0 ? 1 : -1;
		_dy = Random.Range(0, 1) == 0 ? 1 : -1;

		seaEnemy = Instantiate(seaEnemy, new Vector3(_x, _y, _z), Quaternion.identity) as GameObject;
	}

	void newDirection() {
		if (Field.Instance.field[_x + _dx, _y].tag == "Land") _dx = -_dx;
		if (Field.Instance.field[_x, _y + _dy].tag == "Land") _dy = -_dy;
	}

	void move() {
		newDirection();
		_x += _dx;
		_y += _dy;
		seaEnemy.transform.position = new Vector3(_x, _y, _z);
	}
}
