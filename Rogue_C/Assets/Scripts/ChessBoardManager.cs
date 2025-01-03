using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class ChessBoardManager : MonoBehaviour
{
    [SerializeField] private GameObject WhiteSquare;
    [SerializeField] private GameObject BlackSquare;
    [SerializeField] private float squareSize = 1f;
    [SerializeField] private GameObject Pawn;
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

                GameObject pawn = Instantiate(Pawn);
                Vector3 pawnPos = new Vector3(i * squareSize, 0, j * squareSize);

                square.transform.position = position;
                pawn.transform.position = pawnPos;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
