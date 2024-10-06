using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionManager : MonoBehaviour
{
    private GameManager GameManager;
    internal static int winShape = 0;

    void Start()
    {
        GameManager = this.GetComponent<GameManager>();
    }

    public int checkWin(int[,] Grid)
    {
        //return 1 means X won
        //return 0 means draw
        //return -1 means O won
        //return -9 means neither won nor draw (game is not finished)

        for(int i = 0; i < 3; i++)
        {
            //check rows
            if (Grid[i, 0] == 1 && Grid[i, 1] == 1 && Grid[i, 2] == 1) {
                winShape = i+1;
                return 1;
            }
            if (Grid[i, 0] == 2 && Grid[i, 1] == 2 && Grid[i, 2] == 2) {
                winShape = i+1;
                return -1;
            }

            //check coloumn
            if (Grid[0, i] == 1 && Grid[1, i] == 1 && Grid[2, i] == 1) {
                winShape = 4 + i;
                return 1;
            }
            if (Grid[0, i] == 2 && Grid[1, i] == 2 && Grid[2, i] == 2) {
                winShape = 4 + i;
                return -1;
            }
        }

        //check 1st diagonal
        if (Grid[0, 0] == 1 && Grid[1, 1] == 1 && Grid[2, 2] == 1) {
            winShape = 7;
            return 1;
        }
        if (Grid[0, 0] == 2 && Grid[1, 1] == 2 && Grid[2, 2] == 2) {
            winShape = 7;
            return -1;
        }

        //check 2nd diagonal
        if (Grid[2, 0] == 1 && Grid[1, 1] == 1 && Grid[0, 2] == 1) {
            winShape = 8;
            return 1;
        }
        if (Grid[2, 0] == 2 && Grid[1, 1] == 2 && Grid[0, 2] == 2) {
            winShape = 8;
            return -1;
        }

        //If there is any empty cell then return -9
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (Grid[i, j] == 0)
                    return -9;

        //If there is no empty cell and there are no winning state then its a draw and return 0
        return 0;
    }

    public bool isWinning(int[,] Grid)
    {
        int value = checkWin(Grid);
        return value == 1 || value == -1;
    }

    public bool isDraw(int[,] Grid)
    {
        return checkWin(Grid) == 0;
    }
}
