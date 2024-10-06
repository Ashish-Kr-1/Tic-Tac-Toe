using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    internal Vector3 touchPosition;
    internal int[,] grid;
    internal int col_no, row_no, boxno, turn, winner;

    private WinConditionManager win_manager;
    private PlacementControlSystem player;
    private Minimax AI;
    private bool tapLock = false;

    public GameObject winStroke;
    public GameObject winStroke2;
    public float sceneDelay;
    internal float timer;


    /*
     * Variable Grid reprents the 3x3 board
     * Inside Grid :
     * 1 represents X
     * 0 represents nothing
     * 2 represents O
     */

    /*
     * variable turn keep track of the current turn
     * If its value is : 
     * 1 then it means X turn
     * -1 then it means O turn
     * 0 then it means game is stoped
     */

    void Start()
    {
        player = this.GetComponent<PlacementControlSystem>();
        win_manager = this.GetComponent<WinConditionManager>();
        AI = this.GetComponent<Minimax>();

        turn = 1; 
        grid = new int[3, 3];

        for(int i=0;i<3;i++)
        for(int j=0;j<3;j++)
        grid[i,j]=0;

        col_no = 0;
        row_no = 0;
        winner = 0;
        turn = VM.rounds % 2 == 0 ? 1 : -1;

        timer = sceneDelay;
        /**Set the timer to delay. For example, timer = 5sec... 
           now we do timer = timer - timePassed
           if(timer<=0) we execute the code 
           and set timer = delay again*/
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {

            //This 3D vectors represents the touch position on screen, with centre of device as the origin
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchPosition.z = 0;

            //Dividing the screen into 3 columns and 3 rows to simplify input
            col_no = Colno(touchPosition.x);
            row_no = Rowno(touchPosition.y);
            boxno = row_no * 3 + col_no + 1;

            if (turn == 1 && !tapLock) player1turn();                              //player1 turn
            if (turn == -1 && !tapLock && !VM.playComp) player2turn();       //player2 turn
        }

        if (turn == -1 && VM.playComp){
            AI.move();
            if (win_manager.isWinning(grid))
            {
                turn = 0;
                VM.winner = 2;
                showStroke();
            }
        }    //computer turn

        if (win_manager.isDraw(grid)){
            turn = 0;
            VM.winner = 0;
        }
    }

    void FixedUpdate(){

        if (turn!=0) tapLock = false;
        else{
            if(VM.winner!=0){
                timer-= Time.deltaTime;
                if(timer <= 0){
                    SceneManager.LoadScene("Game Over");
                }
            }
            else{ 
                timer-= Time.deltaTime;
                if(timer<=sceneDelay*2/3)
                SceneManager.LoadScene("Game Over");
            }
        }
        
        /**Using fixed update to create little bit of delay btw consecutive taps
           if turn is not 0 we open the tapLock otherwise we lock the tapping and end
           the game.*/

    }

    void player1turn()
    {
        player.place(row_no, col_no);
        tapLock = true;
        //This locks the tapping until next fixed update

        if (win_manager.isWinning(grid))
        {
            turn = 0;
            VM.winner = 1;
            showStroke();
        }
    }

    void player2turn()
    {
        player.place(row_no, col_no); 
        tapLock = true;
        if (win_manager.isWinning(grid))
        {
            turn = 0;
            VM.winner = 2;
            showStroke();
        }
    }

    void showStroke(){
        int i = WinConditionManager.winShape;
        if(i<4){
            Instantiate(winStroke, GameObject.Find("/Grid/Box Centers/"+ (i*3-1)).transform.position, Quaternion.Euler(new Vector3(0,0,90f)));
        }
        else if(i<7){
            Instantiate(winStroke, GameObject.Find("/Grid/Box Centers/"+ i).transform.position, Quaternion.identity);
        }
        else{
            Instantiate(winStroke2, GameObject.Find("/Grid/Box Centers/5").transform.position, Quaternion.Euler(new Vector3(0,0,-45f+90*(i%2))));
            //For diagnol strokes
        }
    }

    int Colno(float x)
    {
        if((x>=-2.36)&&(x<=-0.92))
        return 0;
        else if((x>-0.92)&&(x<=0.70))
        return 1;
        else if((x>0.70)&&(x<=2.34))
        return 2;
        else
        return -1;
    }

    int Rowno(float y)
    {
        if((y>=-2.98)&&(y<=-1.30))
        return 2;
        else if((y>-1.30)&&(y<=0.36))
        return 1;
        else if((y>0.36)&&(y<=1.71))
        return 0;
        else
        return -1;
    }
}
