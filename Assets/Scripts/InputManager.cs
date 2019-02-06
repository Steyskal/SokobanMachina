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
	public List<KeyCode> UpKeyCodes;
	public List<KeyCode> DownKeyCodes;
	public List<KeyCode> LeftKeyCodes;
	public List<KeyCode> RightKeyCodes;

	public CustomUnityEvent<Direction> OnInputReceived = new CustomUnityEvent<Direction>();

	private void Awake()
	{
		if (Instance != this)
			Destroy (gameObject);

		OnInputReceived.AddListener (OnInputReceivedListener);
	}

	private void Update()
	{
		CheckKeyboardInput ();	
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
		if (IsOneKeyDown (UpKeyCodes))
			OnInputReceived.Invoke (Direction.Up);
		else if (IsOneKeyDown (DownKeyCodes))
			OnInputReceived.Invoke (Direction.Down);
		else if (IsOneKeyDown (LeftKeyCodes))
			OnInputReceived.Invoke (Direction.Left);
		else if (IsOneKeyDown (RightKeyCodes))
			OnInputReceived.Invoke (Direction.Right);
	}

	private void OnInputReceivedListener(Direction direction)
	{
		Debug.Log (direction);
	}
}
