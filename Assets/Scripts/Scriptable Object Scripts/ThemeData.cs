using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Theme Data", menuName = "Sokoban/Theme Data", order = 2)]
public class ThemeData : ScriptableObject
{
	public Sprite PlayerSprite;
	public Sprite ExitSprite;
	public Sprite BlockSprite;
	public Sprite CrateSprite;
	public Sprite GroundSprite;
}
