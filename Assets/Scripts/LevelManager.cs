using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
	[Header("Level Prefabs")]
	public PlayerElement PlayerPrefab;
	public ExitElement ExitPrefab;
	public BlockElement BlockPrefab;
	public CrateElement CratePrefab;
	public GameObject GroundPrefab;

	[Header("Level Data")]
	public Vector2Int Size;
	public Vector2Int PlayerPosition;
	public Vector2Int ExitPosition;
	public List<Vector2Int> BlockPositions;
	public List<Vector2Int> CratePositions;

	private Transform _transform;

	private Element[,] level;

	private void Awake()
	{
		_transform = transform;

		GenerateLevel ();
	}

	private void GenerateLevel()
	{
		Transform blockParent = new GameObject ("BlockParent").transform;
		blockParent.SetParent (_transform);

		Transform groundParent = new GameObject ("GroundParent").transform;
		groundParent.SetParent (_transform);

		for (int i = -1; i <= Size.x; i++)
		{
			for (int j = -1; j <= Size.y; j++)
			{
				if((i==-1) || (j==-1) || (i==Size.x) || (j==Size.y))
					Instantiate (BlockPrefab, new Vector3 (i, j, 0.0f), Quaternion.identity, blockParent);
				else
					Instantiate (GroundPrefab, new Vector3 (i, j, 0.0f), Quaternion.identity, groundParent);
			}	
		}

		level = new Element[Size.x, Size.y];

		PlayerElement playerElementClone = Instantiate (PlayerPrefab, (Vector2)PlayerPosition, Quaternion.identity, _transform);
		playerElementClone.Initialize (level);

		level [PlayerPosition.x, PlayerPosition.y] = playerElementClone;

		ExitElement exitElementClone = Instantiate (ExitPrefab, (Vector2)ExitPosition, Quaternion.identity, _transform);
		level [ExitPosition.x, ExitPosition.y] = exitElementClone;

		foreach (Vector2Int blockPosition in BlockPositions)
		{
			BlockElement blockElementClone = Instantiate (BlockPrefab, (Vector2)blockPosition, Quaternion.identity, _transform);
			level [blockPosition.x, blockPosition.y] = blockElementClone;
		}
			
		foreach (Vector2Int cratePosition in CratePositions)
		{
			CrateElement crateElementClone = Instantiate (CratePrefab, (Vector2)cratePosition, Quaternion.identity, _transform);
			crateElementClone.Initialize (level);

			level [cratePosition.x, cratePosition.y] = crateElementClone;
		}
	}
}
