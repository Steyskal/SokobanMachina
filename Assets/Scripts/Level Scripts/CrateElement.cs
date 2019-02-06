using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateElement : Element 
{
	private Transform _transform;

	//private ExitElement _exitElement;

	private void Awake()
	{
		_transform = transform;

		//_exitElement = null;
	}

	public override bool Move (Direction direction)
	{
		Vector3 directionVector = direction.Vector3 ();

		try
		{
			Vector2 newPosition = (Vector2)_transform.position + (Vector2)directionVector;
			Element destinationElement = _Level [(int)newPosition.x, (int)newPosition.y];

			if(destinationElement)
			{
				if(destinationElement is ExitElement)
					if(destinationElement.Move(direction))
					{
						_Level[(int)_transform.position.x,(int)_transform.position.y] = null;

						_transform.position = newPosition;

						_Level[(int)_transform.position.x,(int)_transform.position.y] = this;

						//_exitElement = destinationElement as ExitElement;	

						return true;
					}
			}
			else
			{
				_Level[(int)_transform.position.x,(int)_transform.position.y] = null;

				_transform.position = newPosition;

				_Level[(int)_transform.position.x,(int)_transform.position.y] = this;

				//_exitElement = null;

				return true;
			}
		}
		catch (System.Exception ex)
		{
			Debug.Log ("Unable to move crate out of bounds.");
		}

		return false;
	}
}
