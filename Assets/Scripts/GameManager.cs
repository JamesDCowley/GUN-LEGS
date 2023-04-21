using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int level;
    public int p1Points;
    public int p2Points;
    public int pointsToWin;
    public int winningPlayer;
    public GameObject stat;
    public ScoreboardUI scoreboard;
    public static GameManager instance;
    Scene scene;

    // Start is called before the first frame update
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        scene = SceneManager.GetActiveScene();

        //Creates singleton (one object throughout scenes)
        if(instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        //init level vars
        if(level == 0){
            level = 1;
            p1Points = 0;
            p2Points = 0;
        }
        
    }

    //Find static transision when scene loaded and make it inactive
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        stat = GameObject.FindGameObjectWithTag("BG");
        stat.SetActive(false);
        scoreboard = stat.GetComponentInChildren<ScoreboardUI>();
    }

    
    public void GameOver(int playerNum){
        if(playerNum == 1) {p2Points++; Debug.Log("p2 win");}
        else if(playerNum == 2) {p1Points++; Debug.Log("p1 win");}
        
        level++;

        //player 1 win
        if(p1Points >= pointsToWin){
            Debug.Log("player 1 win");
            winningPlayer = 1;
            SceneManager.LoadScene("Game Over");
            return;
        }
        //player 2 win
        if(p2Points >= pointsToWin){
            Debug.Log("player 2 win");
            winningPlayer = 2;
            SceneManager.LoadScene("Game Over");
            return;
        }
        
        stat.SetActive(true);

        //delete winning player so player doesn't die in the transition
        GameObject player = GameObject.Find("Player");
        if(player == null) player = GameObject.Find("Player 2");
        player.SetActive(false);
        StartCoroutine(waiter());


        
    }

    public void Reset(){
        level = 1;
        p1Points = 0;
        p2Points = 0;
    }
    //wait three seconds then load next level
    IEnumerator waiter(){
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(Random.Range(1,9), LoadSceneMode.Single);
    }
}
