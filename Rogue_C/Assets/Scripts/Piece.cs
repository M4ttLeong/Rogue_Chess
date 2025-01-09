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

    [SerializeField] private LayerMask squareLayerMask;

    private void Start()
    {
        //availMoves need to be updated after each turn
        availMoves = GetAvailableMoves(chessboardManager);
    }
    private void OnMouseEnter()
    {
        if (chessboardManager.getCanHoverOverPieces())
        {
            ShowAvailableMoves(availMoves);
            //DebugHighLight(Color.red);
        }
    }

    private void OnMouseExit()
    {
        if (chessboardManager.getCanHoverOverPieces())
        {
            //ResetHighlight();
            HideAvailableMoves(availMoves);
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
        //need to set canHoverOverPieces in the chessboard to false while dragging
        chessboardManager.setCanHoverOverPieces(false);
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);  // Horizontal plane at y = 0
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float enter))
            {
                Vector3 mousePosition = ray.GetPoint(enter);  // Get the world point where the ray intersects the plane
                transform.position = new Vector3(mousePosition.x, 0.5f, mousePosition.z);  // Move to that point
            }
        }
    }

    private void OnMouseUp()
    {
        HideAvailableMoves(availMoves);
        if (isDragging)
        {
            Debug.Log("Mouse click released");
            Square startingSquare = chessboardManager.GetSquare(position.x, position.y);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, squareLayerMask))
            {
                Square targetSquare = hit.collider.GetComponent<Square>();
                Debug.Log("TARGET SQUARE: " + targetSquare);
                if (targetSquare != null)
                {
                    //Need to check if the square we're moving to is valid
                    if (availMoves.Contains(targetSquare.position))
                    {
                        //Is there already a piece on that square?
                        if (targetSquare.occupyingPiece != null)
                        {
                            Destroy(targetSquare.occupyingPiece.gameObject);
                        }
                        //Need to change occupying piece to null for square the piece was on previously
                        startingSquare.occupyingPiece = null;

                        targetSquare.occupyingPiece = this;

                        //update the position of the piece as well
                        this.position = targetSquare.position;

                        transform.position = new Vector3(targetSquare.position.y, 0f, 7 - targetSquare.position.x);

                        availMoves = GetAvailableMoves(chessboardManager);
                        //Debug.Log("Let go of piece at square: " + square.PositionToChessNotation(square.position));
                    }
                    else
                    {
                        transform.position = new Vector3(this.position.y, 0f, 7 - this.position.x);
                    }
                }
            } else
            {
                //We did not hit a square in our raycast
                transform.position = new Vector3(this.position.y, 0f, 7 - this.position.x);

            }
            isDragging = false;
            chessboardManager.setCanHoverOverPieces(true);
            
        }
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
