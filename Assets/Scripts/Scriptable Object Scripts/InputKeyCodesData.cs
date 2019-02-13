using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Input Key Codes Data",
	menuName = "Sokoban/Input Key Codes Data",
	order = 3)]
public class InputKeyCodesData : ScriptableObject
{
	public List<KeyCode> UpKeyCodes;
	public List<KeyCode> DownKeyCodes;
	public List<KeyCode> LeftKeyCodes;
	public List<KeyCode> RightKeyCodes;
}
