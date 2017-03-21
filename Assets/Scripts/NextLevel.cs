using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour {
	private const int WIN_PERCENT = 60;
	private bool _nextLevel = false;
	private GameObject _panel;
	private Field _field;

	public NextLevel(GameObject panel, Field field) {
		_panel = panel;
		_field = field;
	}

	public void wonLevel() {
		if(_field.getSeaPercent() >= WIN_PERCENT && !_nextLevel) {
			_panel.SetActive(true);
			_nextLevel = true;
		}
		//displayPanel();
	}

	public void displayPanel() {
		//_panel.SetActive(true);
		_nextLevel = true;
	}

	public void closePanel() {
		//_field.getSeaPercent();
		_panel.SetActive(false);
		_nextLevel = false;
	}

	public bool nextLevel() {
		return _nextLevel;
	}
}
