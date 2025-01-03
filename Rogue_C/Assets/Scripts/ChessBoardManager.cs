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
        InitBoard();
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
                chessboard[i, j] = new Square(new Vector2(i, j), squareColor);

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
            GameObject whitePawn = Instantiate(WhitePawn);
            Vector3 pos = new Vector3(i * squareSize, 0, 1 * squareSize);
            whitePawn.transform.position = pos;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
