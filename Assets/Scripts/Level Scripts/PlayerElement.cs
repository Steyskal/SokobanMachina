using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerElement : Element
{
	private Transform _transform;

	private void Awake()
	{
		_transform = transform;

		InputManager.Instance.OnInputReceived.AddListener (OnInputReceivedListener);
	}

	public override bool Move(Direction direction)
	{
		Vector3 directionVector = direction.Vector3 ();

		try
		{
			Vector2 newPosition = (Vector2)_transform.position + (Vector2)directionVector;
			Element destinationElement = _Level [(int)newPosition.x, (int)newPosition.y];

			if(destinationElement)
			{
				if(destinationElement.Move(direction))
				{
					_Level[(int)_transform.position.x,(int)_transform.position.y] = null;

					//vizualni dio !SAMO
					_transform.position += directionVector;

					//this
					//PlayerElement = GetComponent<PlayerElement>();
					_Level[(int)_transform.position.x,(int)_transform.position.y] = this;

					// provijeriti da li smo na zadnjem levelu
					if(destinationElement is ExitElement)
						SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

					return true;
				}

				Debug.Log("Unable to move, player is being blocked.");
			}
			else
			{
				_Level[(int)_transform.position.x,(int)_transform.position.y] = null;
	
				//vizualni dio !SAMO
				_transform.position += directionVector;

				_Level[(int)_transform.position.x,(int)_transform.position.y] = this;

				return true;
			}
		}
		catch (System.Exception ex)
		{
			Debug.LogWarning ("Unable to move player out of bounds.");
		}

		return false;
	}

	private void PrintLevelState()
	{
		string levelState = "";

		for (int y = _Level.GetLength(1) - 1; y >= 0; y--)
		{
			for (int x = 0; x < _Level.GetLength(0); x++)
			{
				Element element = _Level [x, y];

				if (element is PlayerElement)
					levelState += "P";
				else if (element is ExitElement)
					levelState += "E";
				else if (element is BlockElement)
					levelState += "B";
				else if (element is CrateElement)
					levelState += "C";
				else
					levelState += "-";
			}

			levelState += "\n";
		}

		Debug.Log (levelState);
	}

	private void OnInputReceivedListener(Direction direction)
	{
		LevelData levelState = ScriptableObject.CreateInstance<LevelData> ();
		levelState.Size = new Vector2Int (_Level.GetLength (0), _Level.GetLength (1));
		levelState.BlockPositions = new List<Vector2Int> ();
		levelState.CratePositions = new List<Vector2Int> ();

		for (int y = _Level.GetLength(1) - 1; y >= 0; y--)
		{
			for (int x = 0; x < _Level.GetLength(0); x++)
			{
				Element element = _Level [x, y];

				if (element is PlayerElement)
					levelState.PlayerPosition = new Vector2Int (x, y);
				else if (element is ExitElement)
					levelState.ExitPosition = new Vector2Int (x, y);
				else if (element is BlockElement)
					levelState.BlockPositions.Add (new Vector2Int (x, y));
				else if (element is CrateElement)
					levelState.CratePositions.Add (new Vector2Int (x, y));
			}
		}

		FindObjectOfType<LevelManager> ().LevelMemento.AddLevelState (levelState);

		Move (direction);
		PrintLevelState ();
	}
}
