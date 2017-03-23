using UnityEngine;

public class NextLevel : MonoBehaviour {
	private const int WIN_PERCENT = 75;
	private GameObject _panel;
	private Field _field;

	public NextLevel(GameObject panel, Field field) {
		_panel = panel;
		_field = field;
	}

	public void wonLevel() {
		if(_field.getSeaPercent() >= WIN_PERCENT) {
			Time.timeScale = 0;
			_panel.SetActive(true);
		}
	}

	public void closePanel() {
		Time.timeScale = 1;
		_panel.SetActive(false);
	}
}
