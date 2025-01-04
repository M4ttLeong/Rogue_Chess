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

    public void Initialize(Color col)
    {
        this.color = col;
        this.occupyingPiece = null;
    }

    private void OnMouseEnter()
    {
        //Highlight(Color.red);
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
}
