using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject comp, p1, p2, sc1, sc2;
    public GameObject comp_h, p1_h, p2_h; //happy sprites
    public TextMeshProUGUI result1;
    public TextMeshProUGUI result2;
    public TextMeshProUGUI score1;
    public TextMeshProUGUI score2;
    string s;
    public float animDelay, loserDelay;
    float timer;
    bool done1 = false, done2 = false;
    
    // Start is called before the first frame update
    void Start()
    {
        (VM.winner==2?comp_h:comp).SetActive(VM.playComp);
        (VM.winner==2?p2_h:p2).SetActive(!VM.playComp);

        p1_h.SetActive(VM.winner==1);
        p1.SetActive(!(VM.winner==1));

        if(VM.winner==0){
            s = "Game";
        }
        if(VM.winner==1){
            VM.score1++;
            s = VM.playComp?"Player":"Player 1";
        }
        if(VM.winner==2){
            VM.score2++;
            s = VM.playComp?"Computer":"Player 2";
        }

        if(VM.winner==0){
            sc1.SetActive(true);
            sc2.SetActive(true);
        }
        score1.text = ""+VM.score1;
        score2.text = ""+VM.score2;
        result1.text = s;
        result2.text = VM.winner==0?"DRAW":"WON";

        VM.rounds += 1;

        timer = animDelay;     
    }

    public void takeAction(int act){
        if(act==1){
            SceneManager.LoadScene("TicTacToe");
            VM.winner = 0;
        }
        if(act==2){
            SceneManager.LoadScene("Main Menu");
            VM.playIntro = false;
            VM.score1 = 0;
            VM.score2 = 0;
            VM.winner = 0;
            VM.rounds = 0;
        }
    }

    void FixedUpdate() {
        timer-= Time.deltaTime;

        if((timer<=0)&&(VM.winner!=0)&&(!done1)){
            sc1.SetActive(VM.winner==1);
            sc2.SetActive(VM.winner==2);
            done1 = true;
        }
        if((timer<=-loserDelay)&&(VM.winner!=0)&&(!done2)){
            (VM.winner==1?sc2:sc1).SetActive(true);
            done2 = true;
        }
    }
}
