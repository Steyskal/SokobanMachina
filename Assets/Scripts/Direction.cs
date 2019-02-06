using System;

using UnityEngine;

[Serializable]
public enum Direction
{
	Up,
	Down,
	Left,
	Right
}

public static class DirectionExtension
{
	public static Vector3 Vector3(this Direction direction)
	{
		switch (direction)
		{
			case Direction.Up:
				return UnityEngine.Vector3.up;

			case Direction.Down:
				return UnityEngine.Vector3.down;

			case Direction.Right:
				return UnityEngine.Vector3.right;

			case Direction.Left:
				return UnityEngine.Vector3.left;

			default:
				return UnityEngine.Vector3.zero;
		}
	}
}
