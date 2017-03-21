﻿using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {
	private GameObject _land;
	private GameObject _sea;

	public const int WIDTH = 82;
	public const int HEIGHT = 42;

	private int _score = 0;

	private float _currentSeaArea;

	public GameObject[,] field = new GameObject[WIDTH, HEIGHT];

	public Field(GameObject land, GameObject sea) {
		_land = land;
		_sea = sea;
	}

	public void init() {
		int x_start = 0;
		int y_start = 0;
		int z_start = 10;

		int current_pos_x = x_start;
		int current_pos_y = y_start;

		for (int y = 0; y < HEIGHT; y++, current_pos_y++, current_pos_x = x_start)
			for (int x = 0; x < WIDTH; x++, current_pos_x++)
				field[x, y] =
					(x < 2 || x > WIDTH - 3 || y < 2 || y > HEIGHT - 3) ?
					Instantiate(_land, new Vector3(current_pos_x, current_pos_y, z_start), Quaternion.identity) as GameObject :
					Instantiate(_sea, new Vector3(current_pos_x, current_pos_y, z_start), Quaternion.identity) as GameObject;
    }

	public void destroy() {
		int x_start = 0;
		int y_start = 0;
		int z_start = 10;

		int current_pos_x = x_start;
		int current_pos_y = y_start;

		for (int y = 0; y < HEIGHT; y++, current_pos_y++, current_pos_x = x_start)
			for (int x = 0; x < WIDTH; x++, current_pos_x++)
				Destroy(field[x, y]);
	}

	public void fillArea(int x, int y) {
		if (field[x, y].tag != "Sea")
			return;

		field[x, y].tag = "Temp";

		for (int dx = -1; dx < 2; dx++)
			for (int dy = -1; dy < 2; dy++)
				fillArea(x + dx, y + dy);
	}

	public void fillTrackArea(List<SeaEnemy> seaEnemies) {
		_currentSeaArea = 0;

		//foreach (SeaEnemy enemy in seaEnemies)
		//	fillArea(enemy.getX(), enemy.getY());

		//foreach (SeaEnemy enemy in seaEnemies)
		//	fillArea(enemy.getX(), enemy.getY());
		for (int i = 0; i < seaEnemies.Count; i++) {
			fillArea(seaEnemies[i].getX(), seaEnemies[i].getY());
		}

		for (int y = 0; y < HEIGHT; y++)
			for (int x = 0; x < WIDTH; x++) {
				if (field[x, y].tag == "Track" || field[x, y].tag == "Sea") {
					field[x, y] = Instantiate(_land, new Vector3(x, y, 10), Quaternion.identity);
					_score += 10;
				}
				if (field[x, y].tag == "Temp") {
					field[x, y].tag = "Sea";
					_currentSeaArea++;
				}
			}
	}

	public void clearTrack() {
		var track = GameObject.FindGameObjectsWithTag("Track");
		for (int i = 0; i < track.Length; i++) {
			Destroy(track[i]);
		}
		for (int y = 0; y < HEIGHT; y++)
			for (int x = 0; x < WIDTH; x++)
				if (field[x, y].tag == "Track")
					field[x, y] = Instantiate(_sea, new Vector3(x, y, 10), Quaternion.identity);
	}

	//public void clearTrack() {
	//	for (int y = 0; y < HEIGHT; y++)
	//		for (int x = 0; x < WIDTH; x++)
	//			if (field[x, y].tag == "Track")
	//				field[x, y] = Instantiate(_sea, new Vector3(x, y, 10), Quaternion.identity);
	//}

	public float getSeaPercent() {
		float seaArea = (WIDTH - 4) * (HEIGHT - 4);
		float seaPercent = _currentSeaArea / seaArea * 100;
		if (seaPercent == 0)
			return 1;
		return 100 - seaPercent;
	}

	public int getScore() {
		return _score;
	}
}