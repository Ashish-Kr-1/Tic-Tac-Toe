using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementControlSystem : MonoBehaviour
{
    public GameObject X;
    public GameObject O;

    private GameManager GameManager;

    void Start()
    {
        GameManager = this.GetComponent<GameManager>();
    }

    internal void place(int i , int j)
    {
        int box = i * 3 + j + 1;
        if (GameManager.grid[i, j] == 0)
        {
            Transform placement_coords = GameObject.Find("/Grid/Box Centers/" + box).transform;

            //Check whose turn it is and based on that place X and O and also changes the turn and update the grid
            if (GameManager.turn == 1) 
            {
                GameManager.grid[i, j] = 1;
                Instantiate(X, placement_coords.position, placement_coords.rotation); 
                GameManager.turn = -1; 
            }

            else if (GameManager.turn == -1) 
            {
                GameManager.grid[i, j] = 2;
                Instantiate(O, placement_coords.position, placement_coords.rotation); 
                GameManager.turn = 1; 
            }
        }
    }
}
