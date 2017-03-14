using UnityEngine;

public static class SwipeInput {

	public static void SetDirection() {
#if !UNITY_ANDROID && !UNITY_EDITOR
        if (Input.touchCount > 0 && Input.touchCount < 2 && Input.GetTouch(0).phase == TouchPhase.Moved) {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            if (Mathf.Abs(touchDeltaPosition.x) > Mathf.Abs(touchDeltaPosition.y)) {
                if (touchDeltaPosition.x < 0) {
						
				}
                else {
						
				}
            }
            else {
                if (touchDeltaPosition.y < 0) {
				
				}
                else {
        
				}
            }
        }
#elif UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow)) {
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow)) {
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow)) {
		}
#endif
	}
}
