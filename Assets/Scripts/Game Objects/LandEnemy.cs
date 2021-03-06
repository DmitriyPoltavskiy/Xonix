﻿using UnityEngine;

public class LandEnemy : MonoBehaviour {
	private int _dx, 
				_dy, 
				_x, 
				_y, 
				_z = 0;

	private GameObject _landEnemy;
	private Field _field;
	private PlayerCtrl _player;

	public LandEnemy(GameObject landEnemy, Field field, PlayerCtrl player) {
		_field = field;
		_player = player;
		_landEnemy = landEnemy;

		Init();
	}

	public void Init() {
		do {
			_x = Random.Range(2, Field.WIDTH - 2);
			_y = Random.Range(0, 3);
		}
		while (
			_x <= 0 ||
			_y <= 0 ||
			_field.field[_x, _y].tag == "Sea"
		);

		_dx = Random.Range(0, 2) == 0 ? 1 : -1;
		_dy = Random.Range(0, 2) == 0 ? 1 : -1;

		_landEnemy = Instantiate(_landEnemy, new Vector3(_x, _y, _z), Quaternion.identity) as GameObject;
	}

	public void Destroy() {
		Destroy(_landEnemy);
	}

	void UpdateDirection() {
		if (_x <= 0 || _x >= Field.WIDTH - 1) _dx = -_dx;
		if (_y <= 0 || _y >= Field.HEIGHT - 1) _dy = -_dy;

		if (_field.field[_x + _dx, _y].tag == "Sea" || _field.field[_x + _dx, _y].tag == "Track") _dx = -_dx;
		if (_field.field[_x, _y + _dy].tag == "Sea" || _field.field[_x, _y + _dy].tag == "Track") _dy = -_dy;
	}

	public void Move() {
		UpdateDirection();
		_x += _dx;
		_y += _dy;
		_landEnemy.transform.position = new Vector3(_x, _y, _z);
	}

	public bool IsHitXonix() {
		if (_x + _dx == _player.GetX() && _y + _dy == _player.GetY()) return true;
		if (_x - _dx == _player.GetX() && _y - _dy == _player.GetY()) return true;
		if (_x + _dx == _player.GetX() && _y - _dy == _player.GetY()) return true;
		if (_x - _dx == _player.GetX() && _y + _dy == _player.GetY()) return true;
		if (_x + _dx == _player.GetX() && _y == _player.GetY()) return true;
		if (_x - _dx == _player.GetX() && _y == _player.GetY()) return true;
		if (_x == _player.GetX() && _y + _dy == _player.GetY()) return true;
		if (_x == _player.GetX() && _y - _dy == _player.GetY()) return true;

		return false;
	}
}
