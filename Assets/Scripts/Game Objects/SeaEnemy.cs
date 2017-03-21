using System.Collections.Generic;
using UnityEngine;

public class SeaEnemy : MonoBehaviour {
    private int _dx,
				_dy,
				_x,
				_y,
				_z;

	private GameObject _seaEnemy;
	private List<SeaEnemy> _seaEnemies;
	private GameObject _seaEnemyPrefab;
	private Field _field;
	private PlayerCtrl _player;

	public SeaEnemy(GameObject seaEnemy, Field field) {
		_seaEnemyPrefab = seaEnemy;
		_field = field;
	}

	public void init(PlayerCtrl player) {
		_player = player;

		_x = Random.Range(2, Field.WIDTH - 2);
		_y = Random.Range(2, Field.HEIGHT - 2);

		_dx = Random.Range(0, 1) == 0 ? 1 : -1;
		_dy = Random.Range(0, 1) == 0 ? 1 : -1;

		_seaEnemy = Instantiate(_seaEnemyPrefab, new Vector3(_x, _y, _z), Quaternion.identity) as GameObject;
	}

	public void initSeaEnemies(List<SeaEnemy> seaEnemies) {
		_seaEnemies = seaEnemies;
	}

	public void destroy() {
		DestroyImmediate(_seaEnemy);
	}

	public void UpdateDirection() {
		if (_field.field[_x + _dx, _y].tag == "Land") _dx = -_dx;
		if (_field.field[_x, _y + _dy].tag == "Land") _dy = -_dy;
	}

	public void move() {
		UpdateDirection();
		_x += _dx;
		_y += _dy;
		_seaEnemy.transform.position = new Vector3(_x, _y, _z);
	}

	public bool isHitTrackOrXonix() {
		UpdateDirection();

		if (_field.field[_x, _y].tag == "Track") return true;
		if (_x + _dx == _player.getX() && _y + _dy == _player.getY()) return true;

		return false;
	}

	public bool EnemiesHitTrackOrXonix() {
		for (int i = 0; i < _seaEnemies.Count; i++)
			if (_seaEnemies[i].isHitTrackOrXonix())
				return _seaEnemies[i].isHitTrackOrXonix();

		//foreach (SeaEnemy seaEnemy in _seaEnemies)
		//	if (seaEnemy.isHitTrackOrXonix())
		//		return true;
		return false;
	}

	public int getSeaEnemiesCount() {
		return _seaEnemies.Count;
	}

	public int getX() {
		return _x;
	}

	public int getY() {
		return _y;
	}
}
