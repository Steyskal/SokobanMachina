using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
	public int LevelNumber = 0;
	public Text LevelText;

	public void SetLevelNumber(int levelNumber)
	{
		LevelNumber = levelNumber;
		LevelText.text = levelNumber.ToString ("00");
	}

	public void LoadLevel()
	{
		SceneManager.LoadScene (LevelNumber);
	}
}
