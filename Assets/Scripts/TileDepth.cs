using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDepth : MonoBehaviour
{
	SpriteRenderer[] tiles;

	void Start()
	{
		tiles = GetComponentsInChildren<SpriteRenderer>();
		
		foreach(SpriteRenderer tile in tiles)
			tile.sortingOrder = Mathf.RoundToInt(tile.transform.position.y) * -10;
	}
}
