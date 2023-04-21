using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text winMessage;
    GameManager game;
    AudioManager aud;
    public int numLevels;
    public GameObject textField;

    void Start(){
        aud = AudioManager.instance;
        game = GameManager.instance;
        aud.StopAll();
        aud.Play("mainmenu");
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game Over")){
            winMessage = GetComponentInChildren<Text>();
            WinnerText(game.winningPlayer);
        }
        game.Reset();
    }

    //starts game at first level
    public void StartGame(int numLevel){
        game.pointsToWin = numLevel;
        Debug.Log("Start game");
        aud.Stop("mainmenu");
        aud.Play("battle");
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    //for exit button: closes game out
    public void EndGame(){
        Debug.Log("End game");
        Application.Quit();
    }

    public void GameOverButton(){
        game.Reset();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    //sets text on game over screen
    public void WinnerText(int playerNum){
        winMessage.text = "Player " + playerNum + " wins!";
    }
    
    //stores input from custom level field
    public void StoreLevelSelect(){
        numLevels = int.Parse(textField.GetComponent<Text>().text);
        StartGame(numLevels);
    }
}
