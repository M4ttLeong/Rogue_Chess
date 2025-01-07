using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public enum PieceColor { White, Black }
    public PieceColor color { get; private set; }

    private Renderer renderer;

    //Going to store piece position on the piece itself and in the square
    public Vector2Int position { get; private set; }

    private Vector3 originalPosition;
    private bool isDragging = false;

    private List<Vector2Int> availMoves;

    private ChessBoardManager chessboardManager;
    //Could set up the current square in the init step

    private void Start()
    {
        //availMoves need to be updated after each turn
        availMoves = GetAvailableMoves(chessboardManager);
    }
    private void OnMouseEnter()
    {
        ShowAvailableMoves(availMoves);
        //DebugHighLight(Color.red);
    }

    private void OnMouseExit()
    {
        //ResetHighlight();
        HideAvailableMoves(availMoves);
    }

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 mousePosition = hit.point;
                transform.position = new Vector3(mousePosition.x, 0.5f, mousePosition.z);
            }
        }
    }

    private void OnMouseUp()
    {
        Square startingSquare = chessboardManager.GetSquare(position.x, position.y);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Square square = hit.collider.GetComponent<Square>();
            if(square != null)
            {
                //Need to check if the square we're moving to is valid
                if (availMoves.Contains(square.position))
                {
                    //Need to change occupying piece to null for square the piece was on previously
                    startingSquare.occupyingPiece = null;

                    square.occupyingPiece = this;

                    //update the position of the piece as well
                    this.position = square.position;

                    transform.position = new Vector3(square.position.y, 0f, 7 - square.position.x);

                    availMoves = GetAvailableMoves(chessboardManager);
                    //Debug.Log("Let go of piece at square: " + square.PositionToChessNotation(square.position));
                } else
                {
                    transform.position = new Vector3(this.position.y, 0f, 7 - this.position.x);
                }
            }
        }
        isDragging = false;
        //ResetPosition();
    }

    private void ResetPosition()
    {
        transform.position = new Vector3(0,0,0);
    }

    public virtual void Initialize(PieceColor color, Vector2Int position, ChessBoardManager cBM)
    {
        this.color = color;
        this.position = position;
        this.chessboardManager = cBM;
        this.renderer = GetComponent<Renderer>();
    }

    //In order for a piece to get a list of available moves, it would need to see the chessboard
    public abstract List<Vector2Int> GetAvailableMoves(ChessBoardManager cBM);

    private void ShowAvailableMoves(List<Vector2Int> availMoves)
    {
        foreach (Vector2Int move in availMoves)
        {
            chessboardManager.GetChessBoard()[move.x, move.y].Highlight(Color.red);
        }
    }

    private void HideAvailableMoves(List<Vector2Int> availMoves)
    {
        foreach (Vector2Int move in availMoves)
        {
            chessboardManager.GetChessBoard()[move.x, move.y].ResetHighlight();
        }
    }

    private void DebugHighLight(Color highlightColor)
    {
        renderer.material.color = highlightColor;
        //highlight square piece is on as well
        Debug.Log("Pos: " + position.x + ',' + position.y);
        Debug.Log("Occupying piece: " + chessboardManager.GetChessBoard()[position.x, position.y].occupyingPiece);
        if (chessboardManager.GetChessBoard()[position.x, position.y].occupyingPiece != null)
        {
            chessboardManager.GetChessBoard()[position.x, position.y].Highlight(highlightColor);
        }
    }

    private void ResetHighlight()
    {
        renderer.material.color = Color.white;
        chessboardManager.GetChessBoard()[position.x, position.y].ResetHighlight();
    }

    protected bool IsWithinBounds(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < 8 && pos.y >= 0 && pos.y < 8;
    }

}
