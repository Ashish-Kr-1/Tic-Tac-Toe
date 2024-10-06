using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject[] anims = new GameObject[3];
    public GameObject[] normals = new GameObject[3];

    void Start(){

        for(int i=0;i<3;i++){
            anims[i].SetActive(VM.playIntro);
            normals[i].SetActive(!VM.playIntro);
        }

    }
    public void Button_isClicked(int button_id)
    {
        if (button_id == 0) VM.playComp = true;
        if (button_id == 1) VM.playComp = false;

        //Change Scene 
        if (button_id == 0 || button_id == 1) SceneManager.LoadScene("TicTacToe");
        if (button_id == 2){
            VM.playIntro = true;
            VM.score1 = 0;
            VM.score2 = 0;
            VM.winner = 0;
            Application.Quit();
        }
    }
}
