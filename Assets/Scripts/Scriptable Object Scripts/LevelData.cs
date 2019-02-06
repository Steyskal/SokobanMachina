using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "Sokoban/Level Data", order = 1)]
public class LevelData : ScriptableObject
{
	public Vector2Int Size;
	public Vector2Int PlayerPosition;
	public Vector2Int ExitPosition;
	public List<Vector2Int> BlockPositions;
	public List<Vector2Int> CratePositions;
}
