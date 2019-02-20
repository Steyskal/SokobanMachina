using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectPanel : MonoBehaviour
{
	//public int NumOfLevels;
	public LevelButton LevelButtonPrefab;

	private void Awake()
	{
		int numOfScenes = SceneManager.sceneCountInBuildSettings;

		for (int i = 1; i < numOfScenes; i++)
		{
			LevelButton levelButtonClone = Instantiate (LevelButtonPrefab, transform);
			levelButtonClone.SetLevelNumber (i);
		}
	}
}
