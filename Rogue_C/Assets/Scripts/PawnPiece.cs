using System.Collections.Generic;
using UnityEngine;

public class PawnPiece : Piece
{
    private bool hasMoved;

    public override void Initialize(PieceColor color, Vector2Int position, ChessBoardManager cBM)
    {
        base.Initialize(color, position, cBM);
        hasMoved = false;
    }
    public override List<Vector2Int> GetAvailableMoves(ChessBoardManager cBM)
    {
        //Firstly, need to check the piece color to determine movement
        //Black pawn is at (1,0) for example so a forward movement is +
        //White pawn is at (6,0) for example so a forward movement is -

        int forwardMovement = (color == PieceColor.White) ? -1 : 1;

        //A few cases for available moves
        //1) Generally a pawn can move forwards 1 square
        //2) If a pawn hasn't moved yet, can move forward 2
        //3) If there a piece on either forward diagonal, the pawn can capture the piece and move there
        //4) If an enemy pawn moves forward 2 squares putting it side by side with your pawn, you can go to the enemy pawn's diagonal and capture
        //en passant. You only have this option the turn the enemy pawn moved foward 2 squares

        //First find out the pieces's current position:
        Vector2Int currPos = this.position;
        
        //Init list to return
        List<Vector2Int> moves = new List<Vector2Int>();
        
        /*Debug.Log("X: " + currPos.x + " Y: " + currPos.y);
        Debug.Log("X + 1: " + (currPos.x + forwardMovement) + " Y: " + currPos.y);
        Debug.Log("X + 2: " + (currPos.x + (2 * forwardMovement)) + " Y: " + currPos.y);
        Debug.Log("X + 1: " + (currPos.x + forwardMovement) + " Y + 1: " + (currPos.y + 1));*/
        //1)
        if (IsWithinBounds(new Vector2Int(currPos.x + forwardMovement, currPos.y)))
        {
            if (cBM.isSquareOccupied(currPos.x + forwardMovement, currPos.y) == null)
            {
                moves.Add(new Vector2Int(currPos.x + forwardMovement, currPos.y));
            }
        }

        //2)
        if (!hasMoved)
        {
            if (!cBM.isSquareOccupied(currPos.x + (2 * forwardMovement), currPos.y))
            {
                moves.Add(new Vector2Int(currPos.x + (2 * forwardMovement), currPos.y));
            }
        }

        //3)
        Piece piece1;
        Piece piece2;
        Debug.Log("LOG: " + (currPos.x + forwardMovement) + ',' + (currPos.y - 1));
        if (IsWithinBounds(new Vector2Int(currPos.x + forwardMovement, currPos.y - 1)))
        {
            piece1  = cBM.isSquareOccupied(currPos.x + forwardMovement, currPos.y - 1);
        } else
        {
            piece1 = null;
        }

        if (IsWithinBounds(new Vector2Int(currPos.x + forwardMovement, currPos.y + 1)))
        {
            piece2 = cBM.isSquareOccupied(currPos.x + forwardMovement, currPos.y + 1);
        } else
        {
            piece2 = null;
        }

        if (piece1 != null && piece1.color != this.color)
        {
            moves.Add(new Vector2Int(currPos.x + forwardMovement, currPos.y - 1));
        }

        if (piece2 != null && piece2.color != this.color)
        {
            moves.Add(new Vector2Int(currPos.x + forwardMovement, currPos.y + 1));
        }

        Debug.Log(moves.Count);

        return moves;
    }
}
