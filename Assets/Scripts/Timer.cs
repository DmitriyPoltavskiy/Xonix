using UnityEngine;

public class Timer : MonoBehaviour {
	float _time;
	float _delay = 60.5f;

	public Timer() {
		_time = Time.time + _delay;
	}

	public bool TimeIsOver() {
		if (Time.time > _time) {
			return true;
		}
		return false;
	}

	public float GetTime(){
		return _time - Time.time;
	}

	public void UpdateTime() {
		_time = Time.time + _delay;
	}
}
