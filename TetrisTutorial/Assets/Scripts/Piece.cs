using UnityEngine;

public class Piece : MonoBehaviour {

	public Board board { get; private set; }
	public TetrominoData data { get; private set; }
	public Vector3Int[] cells { get; private set; }
	public Vector3Int position { get; private set; }
	public int rotationIndex;
	
	public void Init(Board board, Vector3Int position, TetrominoData data) {
		this.board = board;
		this.position = position;
		this.data = data;
		this.rotationIndex = 0;

		if (this.cells == null) {
			this.cells = new Vector3Int[data.cells.Length];
		}

		for (int i = 0; i < data.cells.Length; i++) {
			this.cells[i] = (Vector3Int)data.cells[i];
		}
	}

	private void Update() {
		this.board.Clear(this);

		if (Input.GetKeyDown(KeyCode.W)) {
			Rotate();
		}
		
		if (Input.GetKeyDown(KeyCode.A)) {
			Move(Vector2Int.left);
		} else if (Input.GetKeyDown(KeyCode.D)) {
			Move(Vector2Int.right);
		}

		if (Input.GetKeyDown(KeyCode.S)) {
			Move(Vector2Int.down);
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			HardDrop();
		}
		
		this.board.Set(this);
	}

	private void HardDrop() {
		while (Move(Vector2Int.down)) {
			continue;
		}
	}

	private bool Move(Vector2Int translation) {
		Vector3Int newPosition = this.position;
		newPosition.x += translation.x;
		newPosition.y += translation.y;

		bool valid = this.board.IsValidPos(this, newPosition);

		if (valid) {
			this.position = newPosition;
		}

		return valid;
	}

	private void Rotate() {
		this.rotationIndex = Wrap(this.rotationIndex++, 0, 4);
		float[] matrix = Data.RotationMatrix;

		for (int i = 0; i < this.cells.Length; i++) {
			Vector3 cell = this.cells[i];

			int x, y;

			switch (this.data.tetromino) {
				case Tetromino.I:
				case Tetromino.O:
					cell.x -= 0.5f;
					cell.y -= 0.5f;
					x = Mathf.CeilToInt(cell.x * matrix[0] + cell.y * matrix[1]);
					y = Mathf.CeilToInt(cell.x * matrix[2] + cell.y * matrix[3]);
					break;
				default:
					x = Mathf.RoundToInt(cell.x * matrix[0] + cell.y * matrix[1]);
					y = Mathf.RoundToInt(cell.x * matrix[2] + cell.y * matrix[3]);
					break;
			}

			this.cells[i] = new Vector3Int(x, y, 0);
		}
	}

	private int Wrap(int input, int min, int max) {
		return min + (input - min) % (max - min);
	}
}
