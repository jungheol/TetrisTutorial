using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Tetromino {
	I,
	O,
	T,
	J,
	L,
	S,
	Z,
}

[System.Serializable]
public struct TetrominoData {
	public Tetromino tetromino;
	public Tile tile;
}
