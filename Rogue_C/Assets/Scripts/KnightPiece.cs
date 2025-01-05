using System.Collections.Generic;
using UnityEngine;

public class KnightPiece : Piece
{
    public override List<Vector2Int> GetAvailableMoves(ChessBoardManager cBM)
    {
        //First find out the pieces's current position:
        Vector2Int currPos = this.position;

        List<Vector2Int> moves = new List<Vector2Int>();
        List<Vector2Int> possibleMoves = new List<Vector2Int>();

        //Moves available to a knight
        //Knight has 8 potential moves
        //Easiest thing to do is just check all 8

        Vector2Int possibleMove1 = new Vector2Int(currPos.x + 2, currPos.y + 1);
        Vector2Int possibleMove2 = new Vector2Int(currPos.x + 2, currPos.y - 1);
        Vector2Int possibleMove3 = new Vector2Int(currPos.x - 2, currPos.y + 1);
        Vector2Int possibleMove4 = new Vector2Int(currPos.x - 2, currPos.y - 1);

        Vector2Int possibleMove5 = new Vector2Int(currPos.x + 1, currPos.y + 2);
        Vector2Int possibleMove6 = new Vector2Int(currPos.x + 1, currPos.y - 2);
        Vector2Int possibleMove7 = new Vector2Int(currPos.x - 1, currPos.y + 2);
        Vector2Int possibleMove8 = new Vector2Int(currPos.x - 1, currPos.y - 2);

        possibleMoves.Add(possibleMove1);
        possibleMoves.Add(possibleMove2);
        possibleMoves.Add(possibleMove3);
        possibleMoves.Add(possibleMove4);
        possibleMoves.Add(possibleMove5);
        possibleMoves.Add(possibleMove6);
        possibleMoves.Add(possibleMove7);
        possibleMoves.Add(possibleMove8);

        foreach(Vector2Int move in possibleMoves)
        {
            if (IsWithinBounds(move))
            {
                if(!cBM.isSquareOccupied(move.x, move.y))
                {
                    moves.Add(move);
                } else if(cBM.isSquareOccupied(move.x, move.y).color != this.color)
                {
                    moves.Add(move);
                }
            }
        }

        return moves;
    }
}
