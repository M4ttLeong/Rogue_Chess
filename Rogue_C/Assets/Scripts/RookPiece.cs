using System.Collections.Generic;
using UnityEngine;

public class RookPiece : Piece
{
    public override List<Vector2Int> GetAvailableMoves(ChessBoardManager cBM)
    {
        int forwardMovement = (color == PieceColor.White) ? -1 : 1;

        //First find out the pieces's current position:
        Vector2Int currPos = this.position;

        List<Vector2Int> moves = new List<Vector2Int>();

        //Moves available to a rook
        //Can move up to any unblocked number of squares vertically and horizontally
        //If the blocking piece is an enemy piece, that piece is a valid move square to capture the occupying piece

        //Check vertically forward
        Vector2Int possibleMove = new Vector2Int(currPos.x + forwardMovement, currPos.y);
        while (IsWithinBounds(possibleMove))
        {
            if (!cBM.isSquareOccupied(possibleMove.x, possibleMove.y))
            {
                moves.Add(possibleMove);
                possibleMove.x += forwardMovement;
            } else if(cBM.isSquareOccupied(possibleMove.x, possibleMove.y).color != this.color)
            {
                moves.Add(possibleMove);
                break;
            } else
            {
                break;
            }
        }

        //Check vertically backward
        possibleMove = new Vector2Int(currPos.x - forwardMovement, currPos.y);
        while (IsWithinBounds(possibleMove))
        {
            if (!cBM.isSquareOccupied(possibleMove.x, possibleMove.y))
            {
                moves.Add(possibleMove);
                possibleMove.x -= forwardMovement;
            }
            else if (cBM.isSquareOccupied(possibleMove.x, possibleMove.y).color != this.color)
            {
                moves.Add(possibleMove);
                break;
            }
            else
            {
                break;
            }
        }

        //Check horizontally left (white persepctive)
        possibleMove = new Vector2Int(currPos.x, currPos.y - 1);
        while (IsWithinBounds(possibleMove))
        {
            if (!cBM.isSquareOccupied(possibleMove.x, possibleMove.y))
            {
                moves.Add(possibleMove);
                possibleMove.y -= 1;
            }
            else if (cBM.isSquareOccupied(possibleMove.x, possibleMove.y).color != this.color)
            {
                moves.Add(possibleMove);
                break;
            }
            else
            {
                break;
            }
        }

        //Check horizontally right (white persepctive)
        possibleMove = new Vector2Int(currPos.x, currPos.y + 1);
        while (IsWithinBounds(possibleMove))
        {
            if (!cBM.isSquareOccupied(possibleMove.x, possibleMove.y))
            {
                moves.Add(possibleMove);
                possibleMove.y += 1;
            }
            else if (cBM.isSquareOccupied(possibleMove.x, possibleMove.y).color != this.color)
            {
                moves.Add(possibleMove);
                break;
            }
            else
            {
                break;
            }
        }

        return moves;
    }
}
