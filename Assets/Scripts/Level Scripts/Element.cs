using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Element : MonoBehaviour
{
	protected Element[,] _Level;

	public virtual void Initialize(Element[,] level)
	{
		_Level = level;
	}

	public virtual bool Move(Direction direction)
	{
		return false;
	}
}
