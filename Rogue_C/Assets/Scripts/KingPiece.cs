using System.Collections.Generic;
using UnityEngine;

public class KingPiece : Piece
{
    public override List<Vector2Int> GetAvailableMoves(ChessBoardManager cBM)
    {
        //First find out the pieces's current position:
        Vector2Int currPos = this.position;

        List<Vector2Int> moves = new List<Vector2Int>();

        //Moves available to a King
        //1) All 8 surrounding squares
        //2) Castling, need a bool to see if the king has moved yet, when calcing avail moves if there is a horizontal line to a castle that
        //hasn't moved either, and the king isn't in check, you can castle.

        //1)
        List<Vector2Int> possibleMoves = new List<Vector2Int>();

        Vector2Int possibleMove1 = new Vector2Int(currPos.x + 1, currPos.y);
        Vector2Int possibleMove2 = new Vector2Int(currPos.x + 1, currPos.y + 1);
        Vector2Int possibleMove3 = new Vector2Int(currPos.x, currPos.y + 1);
        Vector2Int possibleMove4 = new Vector2Int(currPos.x - 1, currPos.y + 1);
        Vector2Int possibleMove5 = new Vector2Int(currPos.x - 1, currPos.y);
        Vector2Int possibleMove6 = new Vector2Int(currPos.x - 1, currPos.y - 1);
        Vector2Int possibleMove7 = new Vector2Int(currPos.x, currPos.y - 1);
        Vector2Int possibleMove8 = new Vector2Int(currPos.x + 1, currPos.y - 1);

        possibleMoves.Add(possibleMove1);
        possibleMoves.Add(possibleMove2);
        possibleMoves.Add(possibleMove3);
        possibleMoves.Add(possibleMove4);
        possibleMoves.Add(possibleMove5);
        possibleMoves.Add(possibleMove6);
        possibleMoves.Add(possibleMove7);
        possibleMoves.Add(possibleMove8);

        foreach (Vector2Int move in possibleMoves)
        {
            if (IsWithinBounds(move))
            {
                if (!cBM.isSquareOccupied(move.x, move.y))
                {
                    moves.Add(move);
                }
                else if (cBM.isSquareOccupied(move.x, move.y).color != this.color)
                {
                    moves.Add(move);
                }
            }
        }

        //2) Castling 
        return moves;
    }
}
