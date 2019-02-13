using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	#region Singleton
	private static InputManager _instance;

	public static InputManager Instance
	{
		get
		{
			if (_instance == null)
			{
				// ako netko zeli pristup prije nego se ovo postavi
				_instance = (InputManager)FindObjectOfType (typeof(InputManager));

				if (_instance == null)
					Debug.Log ("An instance of InputManager doesn't exist!");
			}

			return _instance;
		}
	}
	#endregion

	[Header("Input Key Codes")]
	public InputKeyCodesData InputKeyCodesData;

	public CustomUnityEvent<Direction> OnInputReceived = new CustomUnityEvent<Direction>();

	public float SwipeDeadzoneRadius;

	private Vector3 _swipeDirection;

	private void Awake()
	{
		if (Instance != this)
			Destroy (gameObject);

		OnInputReceived.AddListener (OnInputReceivedListener);
	}

	private void Update()
	{
		CheckKeyboardInput ();
		CheckMouseInput ();
		CheckMobileInput ();
	}

	private bool IsOneKeyDown(List<KeyCode> keyCodes)
	{
		foreach (KeyCode keyCode in keyCodes)
			if(Input.GetKeyDown(keyCode))
				return true;

		return false;
	}

	private void CheckKeyboardInput()
	{
		if (IsOneKeyDown (InputKeyCodesData.UpKeyCodes))
			OnInputReceived.Invoke (Direction.Up);
		else if (IsOneKeyDown (InputKeyCodesData.DownKeyCodes))
			OnInputReceived.Invoke (Direction.Down);
		else if (IsOneKeyDown (InputKeyCodesData.LeftKeyCodes))
			OnInputReceived.Invoke (Direction.Left);
		else if (IsOneKeyDown (InputKeyCodesData.RightKeyCodes))
			OnInputReceived.Invoke (Direction.Right);
	}

	private void CheckMouseInput()
	{
#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
		if (Input.GetMouseButtonDown (0))
			_swipeDirection = Input.mousePosition;
		else if (Input.GetMouseButtonUp (0))
		{
			_swipeDirection = Input.mousePosition - _swipeDirection;

			// _swipeDirection.sqrMagnitude > (SwipeDeadzoneRadius * SwipeDeadzoneRadius)
			if(_swipeDirection.magnitude > SwipeDeadzoneRadius)
				if (Mathf.Abs (_swipeDirection.x) > Mathf.Abs (_swipeDirection.y))
					OnInputReceived.Invoke (_swipeDirection.x >= 0 ? Direction.Right : Direction.Left);
				else
					OnInputReceived.Invoke (_swipeDirection.y >= 0 ? Direction.Up : Direction.Down);
		}
#endif
	}

	private void CheckMobileInput()
	{
#if UNITY_ANDROID
		if(Input.touches.Length != 0)
		{
			Touch touch = Input.touches[0];

			if(touch.phase == TouchPhase.Began)
				_swipeDirection = touch.position;
			else if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
			{
				_swipeDirection = (Vector3)touch.position - _swipeDirection;

				if(_swipeDirection.magnitude > SwipeDeadzoneRadius)
					if (Mathf.Abs (_swipeDirection.x) > Mathf.Abs (_swipeDirection.y))
						OnInputReceived.Invoke (_swipeDirection.x >= 0 ? Direction.Right : Direction.Left);
					else
						OnInputReceived.Invoke (_swipeDirection.y >= 0 ? Direction.Up : Direction.Down);
			}
		}
#endif
	}

	private void OnInputReceivedListener(Direction direction)
	{
		Debug.Log (direction);
	}
}
