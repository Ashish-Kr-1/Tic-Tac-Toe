using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Minimax : MonoBehaviour
{
    private GameManager GameManager;
    private WinConditionManager win_manager;
    private PlacementControlSystem AI;

    void Start()
    {
        GameManager = this.GetComponent<GameManager>();
        win_manager = this.GetComponent<WinConditionManager>();
        AI = this.GetComponent<PlacementControlSystem>();
    }

    public void move()
    {
        int[,] grid = GameManager.grid;
        place(VM.opponent, grid, 0);
    }

    public float place(int player, int[,] grid, int depth)
    {
        int win_state = win_manager.checkWin(grid);
        if (win_state != -9) return win_state;

        List<float> score = new List<float>();
        List<int> col = new List<int>();
        List<int> row = new List<int>();
        
        for(int i = 0; i < 3; i++) 
            for (int j = 0; j < 3; j++)
                if (grid[i, j] == 0)
                {
                    grid [i, j] = player;
                    int _player = (player == 2) ? 1 : 2;
                    score.Add(place(_player, grid, depth + 1)/(depth+1));
                    if(depth == 0) {col.Add(i); row.Add(j);}
                    grid [i, j] = 0;
                }

        float avg = get_average(score);   

        float min = 1, max = -1;
        for(int i = 0; i < score.Count; i++)
        {
            if (score[i] > max) max = score[i];
            if (score[i] < min) min = score[i];
        }
        
        if (depth == 0)
        {
            float search = player == 1 ? max : min;
            for(int i = 0; i < score.Count; i++)
            if (score[i] == search) {AI.place(col[i], row[i]); break;}
        }
        if(player == 1) return max;
        if(player == 2) return min;
        return avg;
    }

    float get_average(List<float> numbers)
    {
        float average = 0;
        for(int i = 0; i < numbers.Count; i++)
        average += numbers[i];

        return average/numbers.Count;
    }
}
