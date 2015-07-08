using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GameController : MonoBehaviour {

    private int[,] _board = new int[13,12];

    private bool _p1Move;

    private int[][] _rows =   
    {
        new int[]{1,0},
        new int[]{2,0},
        new int[]{3,0},
        new int[]{-1,0},
        new int[]{-2,0},
        new int[]{-3,0}
    };

    private int[][] _colunms =   
    {
        new int[]{0,1},
        new int[]{0,2},
        new int[]{0,3},
        new int[]{0,-1},
        new int[]{0,-2},
        new int[]{0,-3}
    };

    private int[][] _diagonals =   
    {
        new int[]{1,1},
        new int[]{2,2},
        new int[]{3,3},
        new int[]{-1,1},
        new int[]{-2,2},
        new int[]{-3,3},
        new int[]{-1,-1},
        new int[]{-2,-2},
        new int[]{-3,-3},
        new int[]{1,-1},
        new int[]{2,-2},
        new int[]{3,-3}
    };

    public GameObject Grid;

    public GameObject[] Circles;

	// Use this for initialization
	void Start () {
	    GenerateBoard();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void GenerateBoard()
    {
        for (int x = 3; x < 10; x++)
        {
            for (int y = 3; y < 9; y++)
            {
                var createGrid = Instantiate(Grid, new Vector2(x, y), Quaternion.identity) as GameObject;
                createGrid.GetComponent<Grid>().XPosition = x;
                createGrid.GetComponent<Grid>().GameController = gameObject.GetComponent<GameController>();
            }    
        }
    }

    public void CreateCircle(int x)
    {
        bool isCreate = false;
        int yPos = 0;
        for (int y = 3; y < 9; y++)
        {
            if (_board[x, y] == 0)
            {
                _board[x, y] = (_p1Move ? 1 : 2);
                yPos = y;
                isCreate = true;
                break;
            }
        }
        if (!isCreate)
        {
            return;
        }
        var createCircle = Instantiate(Circles[_p1Move ? 0: 1],new Vector2(x,yPos),Quaternion.identity) as GameObject;
        createCircle.transform.DOMove(new Vector2(x, 9),Mathf.Max((5 - yPos) / 10f, 0.1f)).From().SetEase(Ease.Linear);
        _p1Move = !_p1Move;
    }
}
