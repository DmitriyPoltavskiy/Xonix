using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {
	private GameObject _land;
	private GameObject _sea;

	public const int WIDTH = 72;
	public const int HEIGHT = 38;

	private int _score = 0;

	private float _currentSeaArea;

	public GameObject[,] field = new GameObject[WIDTH, HEIGHT];

	public Field(GameObject land, GameObject sea) {
		_land = land;
		_sea = sea;
	}

	public void Init() {
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

	public void Destroy() {
		int x_start = 0;
		int y_start = 0;

		int current_pos_x = x_start;
		int current_pos_y = y_start;

		for (int y = 0; y < HEIGHT; y++, current_pos_y++, current_pos_x = x_start)
			for (int x = 0; x < WIDTH; x++, current_pos_x++)
				Destroy(field[x, y]);
	}

	public void FillArea(int x, int y) {
		if (field[x, y].tag != "Sea" || field[x, y].tag == "Temp")
			return;

		field[x, y].tag = "Temp";

		if (field[x + 1, y].tag == "Sea") FillArea(x + 1, y);
		if (field[x - 1, y].tag == "Sea") FillArea(x - 1, y);
		if (field[x, y + 1].tag == "Sea") FillArea(x, y + 1);
		if (field[x, y - 1].tag == "Sea") FillArea(x, y - 1);
	}

	public void FillTrackArea(List<SeaEnemy> seaEnemies) {
		_currentSeaArea = 0;

		for (int i = 0; i < seaEnemies.Count; i++) {
			FillArea(seaEnemies[i].GetX(), seaEnemies[i].GetY());
		}

		for (int y = 0; y < HEIGHT; y++)
			for (int x = 0; x < WIDTH; x++) {
				if (field[x, y].tag == "Track" || field[x, y].tag == "Sea") {
					Destroy(field[x, y]);
					field[x, y] = Instantiate(_land, new Vector3(x, y, 10), Quaternion.identity);
					_score++;
				}
				if (field[x, y].tag == "Temp") {
					field[x, y].tag = "Sea";
					_currentSeaArea++;
				}
			}
	}

	public void DeleteTrack() {
		for (int y = 0; y < HEIGHT; y++) {
			for (int x = 0; x < WIDTH; x++) {
				if (field[x, y].tag == "Track") {
					Destroy(field[x, y]);
					field[x, y] = Instantiate(_sea, new Vector3(x, y, 10), Quaternion.identity);
				}
			}
		}
	}

	public float GetSeaPercent() {
		float seaArea = (WIDTH - 4) * (HEIGHT - 4);
		float seaPercent = _currentSeaArea / seaArea * 100;
		if (seaPercent == 0)
			return 1;
		return 100 - seaPercent;
	}

	public int GetScore() {
		return _score;
	}

	public void SetScore(int score) {
		_score = score;
	}
}
