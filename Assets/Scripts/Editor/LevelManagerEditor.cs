using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor
{
	//private static int _levelDataCounter = 0;

	private void SaveLevelDataAsAsset(LevelData levelData)
	{
		AssetDatabase.CreateAsset (levelData, "Assets/Level Data SOs/New Level Data.asset");
		AssetDatabase.SaveAssets ();

		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = levelData;

		//_levelDataCounter++;
	}

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		LevelManager levelManager = (LevelManager)target;

		if (GUILayout.Button ("Clear Level"))
			levelManager.ClearLevel ();

		if (GUILayout.Button ("Generate Level"))
			levelManager.GenerateLevel ();

		if (GUILayout.Button ("Generate Random Level"))
			levelManager.GenerateRandomLevel ();

		if (GUILayout.Button ("Save Level as Asset"))
			SaveLevelDataAsAsset (levelManager.LevelData);
	}
}
