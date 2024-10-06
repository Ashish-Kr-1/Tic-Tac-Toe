using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VM : MonoBehaviour
{
    //This is the value manager
    //To store data between scenes and load them easily
    public static bool playComp = true;
    public static int winner = 1;
    public static int score1 = 0;
    public static int score2 = 0;
    public static int rounds = 0;
    public static int opponent = rounds%2 == 0? 2 : 1;
    public static bool playIntro = true;
}
