using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardUI : MonoBehaviour
{
    public Text p1Score;
    public Text p2Score;
    GameManager game;

    // Start is called before the first frame update
    void Start()
    {
        //sets the score on the static transition
        game = GameManager.instance;
        p1Score.text = game.p1Points.ToString();
        p2Score.text = game.p2Points.ToString();
    }
}
