using UnityEngine;
using Unity.Netcode;

public class Square : NetworkBehaviour
{
    //Vector2 to show position on chessboard (0,0) = A1
    public Vector2Int position;
    //Color of the square, probably won't be used
    private Color color;

    public Piece occupyingPiece;

    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void Initialize(Color col, Vector2Int pos)
    {
        this.color = col;
        this.occupyingPiece = null;
        this.position = pos;
    }

    private void OnMouseEnter()
    {
        //Highlight(Color.red);
        Debug.Log("Square pos: " + PositionToChessNotation());
    }

    private void OnMouseExit()
    {
        //ResetHighlight();
    }

    public void Highlight(Color highlightColor)
    {
        renderer.material.color = highlightColor;
    }

    public void ResetHighlight()
    {
        renderer.material.color = this.color;
    }

    public string PositionToChessNotation()
    {
        char file = (char)('a' + this.position.y);

        int rank = 8 - this.position.x;

        return $"{file}{rank}"; 
    }
}
