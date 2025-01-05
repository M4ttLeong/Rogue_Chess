using System.Collections.Generic;
using UnityEngine;

public class QueenPiece : Piece
{
    public override List<Vector2Int> GetAvailableMoves(ChessBoardManager cBM)
    {
        //First find out the pieces's current position:
        Vector2Int currPos = this.position;

        List<Vector2Int> moves = new List<Vector2Int>();

        //Moves available to a Queen
        //Full movement capabilities of both the rook and the bishop
        //Just join their code

        //Check vertically forward
        Vector2Int possibleMove = new Vector2Int(currPos.x + 1, currPos.y);
        while (IsWithinBounds(possibleMove))
        {
            if (!cBM.isSquareOccupied(possibleMove.x, possibleMove.y))
            {
                moves.Add(possibleMove);
                possibleMove.x += 1;
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

        //Check vertically backward
        possibleMove = new Vector2Int(currPos.x - 1, currPos.y);
        while (IsWithinBounds(possibleMove))
        {
            if (!cBM.isSquareOccupied(possibleMove.x, possibleMove.y))
            {
                moves.Add(possibleMove);
                possibleMove.x -= 1;
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

        //Check vertically forward right
        possibleMove = new Vector2Int(currPos.x - 1, currPos.y + 1);

        while (IsWithinBounds(possibleMove))
        {
            if (!cBM.isSquareOccupied(possibleMove.x, possibleMove.y))
            {
                moves.Add(possibleMove);
                possibleMove.x -= 1;
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

        //Check vertically forward left
        possibleMove = new Vector2Int(currPos.x - 1, currPos.y - 1);

        while (IsWithinBounds(possibleMove))
        {
            if (!cBM.isSquareOccupied(possibleMove.x, possibleMove.y))
            {
                moves.Add(possibleMove);
                possibleMove.x -= 1;
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

        //Check vertically down right
        possibleMove = new Vector2Int(currPos.x + 1, currPos.y + 1);

        while (IsWithinBounds(possibleMove))
        {
            if (!cBM.isSquareOccupied(possibleMove.x, possibleMove.y))
            {
                moves.Add(possibleMove);
                possibleMove.x += 1;
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

        //Check vertically down left
        possibleMove = new Vector2Int(currPos.x + 1, currPos.y - 1);

        while (IsWithinBounds(possibleMove))
        {
            if (!cBM.isSquareOccupied(possibleMove.x, possibleMove.y))
            {
                moves.Add(possibleMove);
                possibleMove.x += 1;
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


        return moves;
    }
}
