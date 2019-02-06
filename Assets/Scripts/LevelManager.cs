﻿using System.Collections;
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
	public LevelData LevelData;

	[Header("Theme Data")]
	public ThemeData ThemeData;

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

		for (int i = -1; i <= LevelData.Size.x; i++)
		{
			for (int j = -1; j <= LevelData.Size.y; j++)
			{
				if ((i == -1) || (j == -1) || (i == LevelData.Size.x) || (j == LevelData.Size.y)) {
					BlockElement blockClone = Instantiate (BlockPrefab, new Vector3 (i, j, 0.0f), Quaternion.identity, blockParent);
					blockClone.GetComponent<SpriteRenderer> ().sprite = ThemeData.BlockSprite;
				}
				else
				{
					GameObject groundClone = Instantiate (GroundPrefab, new Vector3 (i, j, 0.0f), Quaternion.identity, groundParent);
					groundClone.GetComponent<SpriteRenderer> ().sprite = ThemeData.GroundSprite;
				}
			}	
		}

		level = new Element[LevelData.Size.x, LevelData.Size.y];

		PlayerElement playerElementClone = Instantiate (PlayerPrefab, (Vector2)LevelData.PlayerPosition, Quaternion.identity, _transform);
		playerElementClone.Initialize (level);
		playerElementClone.GetComponent<SpriteRenderer> ().sprite = ThemeData.PlayerSprite;

		level [LevelData.PlayerPosition.x, LevelData.PlayerPosition.y] = playerElementClone;

		ExitElement exitElementClone = Instantiate (ExitPrefab, (Vector2)LevelData.ExitPosition, Quaternion.identity, _transform);
		exitElementClone.GetComponent<SpriteRenderer> ().sprite = ThemeData.ExitSprite;

		level [LevelData.ExitPosition.x, LevelData.ExitPosition.y] = exitElementClone;

		foreach (Vector2Int blockPosition in LevelData.BlockPositions)
		{
			BlockElement blockElementClone = Instantiate (BlockPrefab, (Vector2)blockPosition, Quaternion.identity, _transform);
			blockElementClone.GetComponent<SpriteRenderer> ().sprite = ThemeData.BlockSprite;

			level [blockPosition.x, blockPosition.y] = blockElementClone;
		}
			
		foreach (Vector2Int cratePosition in LevelData.CratePositions)
		{
			CrateElement crateElementClone = Instantiate (CratePrefab, (Vector2)cratePosition, Quaternion.identity, _transform);
			crateElementClone.Initialize (level);
			crateElementClone.GetComponent<SpriteRenderer> ().sprite = ThemeData.CrateSprite;

			level [cratePosition.x, cratePosition.y] = crateElementClone;
		}
	}
}
