﻿using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{

    private int[,] _board = new int[13, 12];

    private bool _p1Move;

    public GameObject Grid;

    public GameObject[] Circles;

    public bool IsWin;

    public GameObject[] WinText;

    // Use this for initialization
    private void Start()
    {
        GenerateBoard();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("R"))
        {
            DestroyLevel();
        }
    }

    private void GenerateBoard()
    {
        _p1Move = true;
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
        var createCircle =
            Instantiate(Circles[_p1Move ? 0 : 1], new Vector2(x, yPos), Quaternion.identity) as GameObject;
        createCircle.transform.DOMove(new Vector2(x, 9), Mathf.Max((5 - yPos)/10f, 0.1f)).From().SetEase(Ease.Linear);
        _p1Move = !_p1Move;
        
        if (WinTest(x, yPos) == 1)
        {
            Win(1);
        }
        else if (WinTest(x, yPos) == 2)
        {
            Win(2);
        }
        else
        {
            DrawTest();
        }
    }

    private int WinTest(int x, int y)
    {
        int inARow = 1;
        int player = _board[x, y];
        //colunms
        for (int inColunm = 1; inColunm < 5; inColunm++)
        {
            if (_board[x, y + inColunm] == player)
            {
                inARow++;
                continue;
            }
            break;
        }
        if (inARow >= 4)
        {
            return player;
        }
        for (int inColunm = 1; inColunm < 5; inColunm++)
        {
            if (_board[x, y - inColunm] == player)
            {
                inARow++;
                continue;
            }
            break;
        }
        if (inARow >= 4)
        {
            return player;
        }
        inARow = 1;
        //rows
        for (int inRow = 1; inRow < 5; inRow++)
        {
            if (_board[x + inRow, y] == player)
            {
                inARow++;
                continue;
            }
            break;
        }
        if (inARow >= 4)
        {
            return player;
        }
        for (int inRow = 1; inRow < 5; inRow++)
        {
            if (_board[x - inRow, y] == player)
            {
                inARow++;
                continue;
            }
            break;
        }
        if (inARow >= 4)
        {
            return player;
        }
        inARow = 1;
        //diagonalsR
        for (int inDiagonal = 1; inDiagonal < 5; inDiagonal++)
        {
            if (_board[x + inDiagonal, y + inDiagonal] == player)
            {
                inARow++;
                continue;
            }
            break;
        }
        if (inARow >= 4)
        {
            return player;
        }
        for (int inDiagonal = 1; inDiagonal < 5; inDiagonal++)
        {
            if (_board[x - inDiagonal, y - inDiagonal] == player)
            {
                inARow++;
                continue;
            }
            break;
        }
        if (inARow >= 4)
        {
            return player;
        }
        inARow = 1;
        //diagonalsL
        for (int inDiagonal = 1; inDiagonal < 5; inDiagonal++)
        {
            if (_board[x + inDiagonal, y - inDiagonal] == player)
            {
                inARow++;
                continue;
            }
            break;
        }
        if (inARow >= 4)
        {
            return player;
        }
        for (int inDiagonal = 1; inDiagonal < 5; inDiagonal++)
        {
            if (_board[x - inDiagonal, y + inDiagonal] == player)
            {
                inDiagonal++;
                continue;
            }
            break;
        }
        if (inARow >= 4)
        {
            return player;
        }
        return 0;
    }

    private void Win(int win)
    {
        IsWin = true;
        Invoke("DestroyLevel",5);
        WinText[win].SetActive(true);
    }

    private void DestroyLevel()
    {
        foreach (var text in WinText)
        {
            text.SetActive(false);
        }
        IsWin = false;
        foreach(var created in GameObject.FindGameObjectsWithTag("Created"))
        {
            Destroy(created);
        }
        for (int x = 0; x < 13; x++)
        {
            for (int y = 0; y < 12; y++)
            {
                _board[x, y] = 0;
            }
        }
        GenerateBoard();
    }

    private void DrawTest()
    {
        for (int x = 3; x < 10; x++)
        {
            for (int y = 3; y < 9; y++)
            {
                if (_board[x, y] == 0)
                {
                    return;
                }
            }
        }
        Win(0);
    }
}