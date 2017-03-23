using UnityEngine;

public class NextLevel : MonoBehaviour {
	private const int WIN_PERCENT = 75;
	private GameObject _panel;
	private Field _field;

	public NextLevel(GameObject panel, Field field) {
		_panel = panel;
		_field = field;
	}

	public void WonLevel() {
		if(_field.GetSeaPercent() >= WIN_PERCENT) {
			Time.timeScale = 0;
			_panel.SetActive(true);
		}
	}

	public void ClosePanel() {
		Time.timeScale = 1;
		_panel.SetActive(false);
	}
}
