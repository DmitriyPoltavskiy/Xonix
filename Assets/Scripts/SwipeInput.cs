using UnityEngine;

public static class SwipeInput {

	public static int SetDirection(int _direction) {
#if !UNITY_ANDROID && !UNITY_EDITOR
        if (Input.touchCount > 0 && Input.touchCount < 2 && Input.GetTouch(0).phase == TouchPhase.Moved) {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            if (Mathf.Abs(touchDeltaPosition.x) > Mathf.Abs(touchDeltaPosition.y)) {
                if (touchDeltaPosition.x < 0) {
					_direction = -2;
				}
                else {
					_direction = 2;
				}
            }
            else {
                if (touchDeltaPosition.y < 0) {
					_direction = 1;
				}
                else {
					_direction = -1;
				}
            }
        }
#elif UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			_direction = 2;
		if (Input.GetKeyDown(KeyCode.RightArrow))
			_direction = -2;
		if (Input.GetKeyDown(KeyCode.UpArrow))
			_direction = 1;
		if (Input.GetKeyDown(KeyCode.DownArrow))
			_direction = -1;
#endif
		return _direction;
	}
}
