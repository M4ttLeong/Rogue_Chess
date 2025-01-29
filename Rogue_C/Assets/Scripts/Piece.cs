using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public abstract class Piece : NetworkBehaviour
{
    public enum PieceColor { White, Black }
    public PieceColor color { get; private set; }

    private Renderer renderer;

    //Going to store piece position on the piece itself and in the square
    public Vector2Int position { get; private set; }

    private Vector3 originalPosition;
    private bool isDragging = false;

    private List<Vector2Int> availMoves;

    private ChessBoardManager chessboardManager = ChessBoardManager.Instance;
    //Could set up the current square in the init step

    [SerializeField] private LayerMask squareLayerMask;

    private void Start()
    {
        //availMoves need to be updated after each turn
        availMoves = GetAvailableMoves(chessboardManager);
    }


    private void OnMouseEnter()
    {
        //I think everytime you hover over the piece, get it's available moves. This might not be best
        availMoves = GetAvailableMoves(chessboardManager);
        Debug.Log("Can hover over pieces?" + chessboardManager.getCanHoverOverPieces());
        if (chessboardManager.getCanHoverOverPieces())
        {
            if (NetworkManager.Singleton.IsClient)
            {
                ShowAvailableMoves();
                //DebugHighLight(Color.red);
            }
        }
    }

    private void OnMouseExit()
    {
        if (chessboardManager.getCanHoverOverPieces())
        {
            //ResetHighlight();
            HideAvailableMoves();
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
        if (isDragging && IsOwner)
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
        
        if (isDragging)
        {
            isDragging = false;
            chessboardManager.setCanHoverOverPieces(true);
            HideAvailableMoves();

            //Square startingSquare = chessboardManager.GetSquare(position.x, position.y);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, squareLayerMask))
            {
                Square targetSquare = hit.collider.GetComponent<Square>();
                if (targetSquare != null)
                {
                    RequestMoveServerRpc(targetSquare.position);
                    /*
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
                        //Debug.Log("Let go of piece at square: " + square.PositionToChessNotation(square.position))
                        
                    }
                    else
                    {
                        transform.position = new Vector3(this.position.y, 0f, 7 - this.position.x);
                    }*/
                } else
                {
                    ResetPiecePosition();
                }
            }
            else
            {
                //We did not hit a square in our raycast
                ResetPiecePosition();

            }
            

        }
        
    }
    

    public virtual void Initialize(PieceColor color, Vector2Int position, ChessBoardManager cBM)
    {
        this.color = color;
        this.position = position;
        //this.chessboardManager = cBM;
        this.renderer = GetComponent<Renderer>();
    }

    //In order for a piece to get a list of available moves, it would need to see the chessboard
    public abstract List<Vector2Int> GetAvailableMoves(ChessBoardManager cBM);

    private void ShowAvailableMoves()
    {
        foreach (Vector2Int move in this.availMoves)
        {
            chessboardManager.GetChessBoard()[move.x, move.y].Highlight(Color.red);
        }
    }

    private void HideAvailableMoves()
    {
        foreach (Vector2Int move in this.availMoves)
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

    /*Networking code*/

    [ServerRpc(RequireOwnership = false)]
    public void HighlightMovesServerRpc()
    {
        //Not sure you actually want this networked? Highlighting might be best for just your client
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestMoveServerRpc(Vector2Int targetPosition)
    {
        Square startingSquare = chessboardManager.GetSquare(position.x, position.y);
        Debug.Log("Starting Square is : " + startingSquare.PositionToChessNotation());

        Square targetSquare = chessboardManager.GetSquare(targetPosition.x, targetPosition.y);
        Debug.Log("Starting Square is : " + targetSquare.PositionToChessNotation());

        if (availMoves.Contains(targetPosition))
        {
            //Capturing logic
            if(targetSquare.occupyingPiece != null && targetSquare.occupyingPiece != this)
            {
                Destroy(targetSquare.occupyingPiece.gameObject);
            }

            //Update board
            startingSquare.occupyingPiece = null;
            targetSquare.occupyingPiece = this;
            position = targetPosition;

            //Notify clients of update
            UpdatePiecePositionClientRpc(targetPosition);
        } else
        {
            ResetPiecePositionClientRpc();
        }
    }

    private void ResetPiecePosition()
    {
        //Return the piece to it's last valid position
        transform.position = new Vector3(position.y, 0f, 7 - position.x);
    }

    [ClientRpc]
    private void UpdatePiecePositionClientRpc(Vector2Int targetPosition)
    {
        transform.position = new Vector3(targetPosition.y, 0f, 7 - targetPosition.x);
    }

    [ClientRpc]
    private void ResetPiecePositionClientRpc()
    {
        ResetPiecePosition();
    }
}

