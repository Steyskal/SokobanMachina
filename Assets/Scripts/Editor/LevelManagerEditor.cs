using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor
{
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
			Debug.Log ("Save Level As Asset");
	}
}
