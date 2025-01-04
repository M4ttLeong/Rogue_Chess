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

    private void OnMouseEnter()
    {
        availMoves = GetAvailableMoves(chessboardManager);
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
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 mousePosition = hit.point;
                transform.position = new Vector3(mousePosition.x, 0.5f, mousePosition.z);
            }
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        ResetPosition();
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
