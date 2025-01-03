using UnityEngine;

public class Square
{
    //Vector2 to show position on chessboard (0,0) = A1
    public Vector2 position;
    private Color color;
    public Piece occupyingPiece;
    
    public Square(Vector2 pos, Color col)
    {
        this.position = pos;
        this.color = col;
        this.occupyingPiece = null;
    }
}
