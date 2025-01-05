using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class ChessBoardManager : MonoBehaviour
{
    [SerializeField] private GameObject WhiteSquare;
    [SerializeField] private GameObject BlackSquare;
    [SerializeField] private float squareSize = 1f;
    [SerializeField] private GameObject WhitePawn;
    [SerializeField] private GameObject BlackPawn;
    [SerializeField] private GameObject WhiteRook;
    [SerializeField] private GameObject BlackRook;
    [SerializeField] private GameObject WhiteKnight;
    [SerializeField] private GameObject BlackKnight;
    [SerializeField] private GameObject WhiteBishop;
    [SerializeField] private GameObject BlackBishop;
    [SerializeField] private GameObject WhiteKing;
    [SerializeField] private GameObject BlackKing;
    [SerializeField] private GameObject WhiteQueen;
    [SerializeField] private GameObject BlackQueen;

    Square[,] chessboard = new Square[8, 8];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //InitBoard();
        InitBoard2();
    }

    public Square[,] GetChessBoard()
    {
        return chessboard;
    }

    private void InitBoard2()
    {
        Color squareColor;
        //0,0 corresponds to a8 on a chessboard a white square
        for (int i = 0; i < 8; i++)
        {
            if (i % 2 == 0)
            {
                squareColor = Color.white;
            }
            else
            {
                squareColor = Color.black;
            }

            for (int j = 0; j < 8; j++)
            {
                //chessboard[i, j] = new Square(new Vector2(i, j), squareColor);

                //Create visual representation
                GameObject squarePrefab = squareColor == Color.white ? WhiteSquare : BlackSquare;
                GameObject squareVisual = Instantiate(squarePrefab);

                Vector3 position = new Vector3(j * squareSize, -0.5f, 7 - i * squareSize);

                squareVisual.transform.position = position;

                squareVisual.transform.parent = transform;

                //Initialize the square game part

                Square squareComp = squareVisual.GetComponent<Square>();
                squareComp.Initialize(squareColor);

                chessboard[i,j] = squareComp;


                //alternate the square color for the next iteration of j
                if (squareColor == Color.black)
                {
                    squareColor = Color.white;
                }
                else
                {
                    squareColor = Color.black;
                }
            }
        }

        InitPawns();
        InitRooks();
        InitKnights();
        InitBishops();
        InitQueens();
        InitKings();
    }

    private void InitPawns()
    {
        //Pawns, they're on i = 6 (white) and 1 (black
        //White Pawns
        for (int i = 0; i < 8; ++i)
        {
            //Create white pawn representation
            GameObject whitePawn = Instantiate(WhitePawn);
            Vector3 pos = new Vector3(i * squareSize, 0, 1 * squareSize);
            whitePawn.transform.position = pos;

            //For each white pawn need to initialize the actual gameplay piece
            Piece whitePawnComp = whitePawn.GetComponent<Piece>();
            whitePawnComp.Initialize(Piece.PieceColor.White, new Vector2Int(6, i), this);

            //In the chessboard set that this piece is here
            chessboard[6, i].occupyingPiece = whitePawnComp;
        }


        //Black Pawns
        for (int i = 0; i < 8; ++i)
        {
            GameObject blackPawn = Instantiate(BlackPawn);
            Vector3 pos = new Vector3(i * squareSize, 0, 6 * squareSize);
            blackPawn.transform.position = pos;

            //For each black pawn need to initialize the actual gameplay piece
            Piece blackPawnComp = blackPawn.GetComponent<Piece>();
            blackPawnComp.Initialize(Piece.PieceColor.Black, new Vector2Int(1, i), this);

            //In the chessboard set that this piece is here
            chessboard[1, i].occupyingPiece = blackPawnComp;
        }
    }

    private void InitRooks()
    {
        //White Rooks
        for (int i = 0; i < 2; ++i)
        {
            GameObject whiteRook = Instantiate(WhiteRook);
            Vector3 pos = new Vector3(i * 7, 0, 0);
            whiteRook.transform.position = pos;

            //Init white rook gameplay piece
            Piece whiteRookComp = whiteRook.GetComponent<Piece>();
            whiteRookComp.Initialize(Piece.PieceColor.White, new Vector2Int(7, i * 7), this);
            chessboard[7, i * 7].occupyingPiece = whiteRookComp;
        }

        //Black Rooks
        for (int i = 0; i < 2; ++i)
        {
            GameObject blackRook = Instantiate(BlackRook);
            Vector3 pos = new Vector3(i * 7, 0, 7 * squareSize);
            blackRook.transform.position = pos;

            //Init white rook gameplay piece
            Piece blackRookComp = blackRook.GetComponent<Piece>();
            blackRookComp.Initialize(Piece.PieceColor.Black, new Vector2Int(0, i * 7), this);
            chessboard[0, i * 7].occupyingPiece = blackRookComp;
        }
    }

    private void InitKnights()
    {
        for (int i = 0; i < 2; ++i)
        {
            GameObject whiteKnight = Instantiate(WhiteKnight);
            Vector3 pos = new Vector3((i * 5) + 1, 0, 0);
            whiteKnight.transform.position = pos;

            Piece whiteKnightComp = whiteKnight.GetComponent<Piece>();
            whiteKnightComp.Initialize(Piece.PieceColor.White, new Vector2Int(7, (i * 5) + 1), this);
            chessboard[7, (i * 5) + 1].occupyingPiece = whiteKnightComp;
        }

        //Black Knights
        for (int i = 0; i < 2; ++i)
        {
            GameObject blackKnight = Instantiate(BlackKnight);
            Vector3 pos = new Vector3((i * 5) + 1, 0, 7 * squareSize);
            blackKnight.transform.position = pos;

            Piece blackKnightComp = blackKnight.GetComponent<Piece>();
            blackKnightComp.Initialize(Piece.PieceColor.Black, new Vector2Int(0, (i * 5) + 1), this);
            chessboard[0, (i * 5) + 1].occupyingPiece = blackKnightComp;
        }
    }

    private void InitBishops()
    {
        //White Bishops
        for (int i = 0; i < 2; ++i)
        {
            GameObject whiteBishop = Instantiate(WhiteBishop);
            Vector3 pos = new Vector3((i * 3) + 2, 0, 0);
            whiteBishop.transform.position = pos;

            Piece whiteBishopComp = whiteBishop.GetComponent<Piece>();
            whiteBishopComp.Initialize(Piece.PieceColor.White, new Vector2Int(7, (i * 3) + 2), this);
            chessboard[7, (i * 3) + 2].occupyingPiece = whiteBishopComp;
        }

        //Black Bishops
        for (int i = 0; i < 2; ++i)
        {
            GameObject blackBishop = Instantiate(BlackBishop);
            Vector3 pos = new Vector3((i * 3) + 2, 0, 7 * squareSize);
            blackBishop.transform.position = pos;

            Piece blackBishopComp = blackBishop.GetComponent<Piece>();
            blackBishopComp.Initialize(Piece.PieceColor.Black, new Vector2Int(0, (i * 3) + 2), this);
            chessboard[0, (i * 3) + 2].occupyingPiece = blackBishopComp;
        }
    }

    private void InitQueens()
    {
        //White Queen
        GameObject whiteQueen = Instantiate(WhiteQueen);
        Vector3 pos3 = new Vector3(3 * squareSize, 0, 0);
        whiteQueen.transform.position = pos3;

        Piece whiteQueenComp = whiteQueen.GetComponent<Piece>();
        whiteQueenComp.Initialize(Piece.PieceColor.White, new Vector2Int(7, 3), this);
        chessboard[7,3].occupyingPiece= whiteQueenComp;

        //Black Queen

        GameObject blackQueen = Instantiate(BlackQueen);
        Vector3 pos4 = new Vector3(3 * squareSize, 0, 7 * squareSize);
        blackQueen.transform.position = pos4;

        Piece blackQueenComp = blackQueen.GetComponent<Piece>();
        blackQueenComp.Initialize(Piece.PieceColor.Black, new Vector2Int(0, 3), this);
        chessboard[0, 3].occupyingPiece = blackQueenComp;
    }

    private void InitKings()
    {
        //White King
        GameObject whiteKing = Instantiate(WhiteKing);
        Vector3 pos1 = new Vector3(4 * squareSize, 0, 0);
        whiteKing.transform.position = pos1;

        Piece whiteKingComp = whiteKing.GetComponent<Piece>();
        whiteKingComp.Initialize(Piece.PieceColor.White, new Vector2Int(7,4), this);
        chessboard[7, 4].occupyingPiece = whiteKingComp;

        //Black King
        GameObject blackKing = Instantiate(BlackKing);
        Vector3 pos2 = new Vector3(4 * squareSize, 0, 7 * squareSize);
        blackKing.transform.position = pos2;

        Piece blackKingComp = blackKing.GetComponent<Piece>();
        blackKingComp.Initialize(Piece.PieceColor.Black, new Vector2Int(0, 4), this);
        chessboard[0, 4].occupyingPiece = blackKingComp;
    }

    private void InitBoard()
    {
        Color squareColor;
        //Create the board made up of squares
        for (int i = 0; i < 8; i++)
        {
            if (i % 2 == 0)
            {
                squareColor = Color.black;
            }
            else
            {
                squareColor = Color.white;
            }

            for (int j = 0; j < 8; j++)
            {
                //chessboard[i, j] = new Square(new Vector2(i, j), squareColor);

                //create visual representation
                GameObject squarePrefab = squareColor == Color.white ? WhiteSquare : BlackSquare;
                GameObject square = Instantiate(squarePrefab);

                Vector3 position = new Vector3(i * squareSize, -0.5f, j * squareSize);

                square.transform.position = position;

                square.transform.parent = transform;

                //alternate the square color for the next iteration of j
                if (squareColor == Color.black)
                {
                    squareColor = Color.white;
                }
                else
                {
                    squareColor = Color.black;
                }
            }
        }

        //Initialize each of the pieces
        //Pawns, they're on i = 1 and 6
        //White Pawns
        for(int i = 0; i < 8; ++i)
        {
            //Create white pawn representation
            GameObject whitePawn = Instantiate(WhitePawn);
            Vector3 pos = new Vector3(i * squareSize, 0, 1 * squareSize);
            whitePawn.transform.position = pos;

            //For each white pawn need to initialize the actual gameplay piece
            Piece whitePawnComp = whitePawn.GetComponent<Piece>();
            whitePawnComp.Initialize(Piece.PieceColor.White, new Vector2Int(i, 1), this);

            //In the chessboard set that this piece is here
            chessboard[i, 1].occupyingPiece = whitePawnComp;
        }


        //Black Pawns
        for (int i = 0; i < 8; ++i)
        {
            GameObject blackPawn = Instantiate(BlackPawn);
            Vector3 pos = new Vector3(i * squareSize, 0, 6 * squareSize);
            blackPawn.transform.position = pos;
        }

        //White Rooks
        for(int i =0; i < 2; ++i)
        {
            GameObject whiteRook = Instantiate(WhiteRook);
            Vector3 pos = new Vector3(i * 7, 0, 0);
            whiteRook.transform.position = pos;
        }

        //Black Rooks
        for (int i = 0; i < 2; ++i)
        {
            GameObject blackRook = Instantiate(BlackRook);
            Vector3 pos = new Vector3(i * 7, 0, 7 * squareSize);
            blackRook.transform.position = pos;
        }

        //White Knights
        for (int i = 0; i < 2; ++i)
        {
            GameObject whiteKnight = Instantiate(WhiteKnight);
            Vector3 pos = new Vector3((i * 5) + 1, 0, 0);
            whiteKnight.transform.position = pos;
        }

        //Black Knights
        for (int i = 0; i < 2; ++i)
        {
            GameObject blackKnight = Instantiate(BlackKnight);
            Vector3 pos = new Vector3((i * 5) + 1, 0, 7 * squareSize);
            blackKnight.transform.position = pos;
        }

        //White Bishops
        for (int i = 0; i < 2; ++i)
        {
            GameObject whiteBishop = Instantiate(WhiteBishop);
            Vector3 pos = new Vector3((i * 3) + 2, 0, 0);
            whiteBishop.transform.position = pos;
        }

        //Black Bishops
        for (int i = 0; i < 2; ++i)
        {
            GameObject blackBishop = Instantiate(BlackBishop);
            Vector3 pos = new Vector3((i * 3) + 2, 0, 7 * squareSize);
            blackBishop.transform.position = pos;
        }

        //White King
        GameObject whiteKing = Instantiate(WhiteKing);
        Vector3 pos1 = new Vector3(4 * squareSize, 0, 0);
        whiteKing.transform.position = pos1;

        //Black King
        GameObject blackKing = Instantiate(BlackKing);
        Vector3 pos2 = new Vector3(4 * squareSize, 0, 7 * squareSize);
        blackKing.transform.position = pos2;

        //White Queen
        GameObject whiteQueen = Instantiate(WhiteQueen);
        Vector3 pos3 = new Vector3(3 * squareSize, 0, 0);
        whiteQueen.transform.position = pos3;

        //Black Queen

        GameObject blackQueen = Instantiate(BlackQueen);
        Vector3 pos4 = new Vector3(3 * squareSize, 0, 7 * squareSize);
        blackQueen.transform.position = pos4;
    }

    public Piece isSquareOccupied(int x, int y)
    {
        Debug.Log("Is there a piece in this square: " + chessboard[x, y].occupyingPiece);
        return chessboard[x,y].occupyingPiece;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
