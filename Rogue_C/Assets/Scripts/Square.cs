using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
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
        Debug.Log("Square pos: " + PositionToChessNotation(this.position));
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

    public string PositionToChessNotation(Vector2Int position)
    {
        char file = (char)('a' + position.y);

        int rank = 8 - position.x;

        return $"{file}{rank}"; 
    }
}
