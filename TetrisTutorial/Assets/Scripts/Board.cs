using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour {
	public TetrominoData[] tetrominoes;
	public Tilemap tilemap { get; private set; }

	private void Awake() {
		this.tilemap = GetComponentInChildren<Tilemap>();
		
		for (int i = 0; i < this.tetrominoes.Length; i++) {
			this.tetrominoes[i].Init();
		}
	}

	private void Start() {
		SpawnPiece();
	}

	public void SpawnPiece() {
		int ran = Random.Range(0, tetrominoes.Length);
		TetrominoData data = this.tetrominoes[ran];
	}

	public void Set() {
		
	}
}
