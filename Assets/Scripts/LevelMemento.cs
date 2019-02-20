using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelMemento
{
	[SerializeField]
	private List<LevelData> _levelStates = new List<LevelData>();

	public LevelMemento(LevelData defaultLevelState)
	{
		AddLevelState (defaultLevelState);
	}

	public void AddLevelState(LevelData levelState)
	{
		_levelStates.Add (levelState);
	}

	public LevelData GetLastLevelState()
	{
		if (_levelStates.Count == 1)
			return _levelStates [0];

		// da li se moze stavit -1 kao index
		LevelData lastLevelState = _levelStates[_levelStates.Count - 1];
		_levelStates.Remove (lastLevelState);

		return lastLevelState;
	}
}
