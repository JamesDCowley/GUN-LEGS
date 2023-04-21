using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text healthP1Text;
    public Text healthP2Text;
    public Text p1AmmoText;
    public Text p2AmmoText;
    public Text roundText;
    public Player player1;
    public Player player2;
    public Weapon p1Weapon;
    public Weapon p2Weapon;
    GameManager game;

    void Start()
    {
        game = GameManager.instance;
        p1Weapon = player1.GetComponent<Weapon>();
        p2Weapon = player2.GetComponent<Weapon>();
        healthP1Text.text = "HP " + player1.health.ToString();
        healthP2Text.text = "HP " + player2.health.ToString();
        p1AmmoText.text = p1Weapon.ammo.ToString();
        p2AmmoText.text = p2Weapon.ammo.ToString();
        roundText.text = "ROUND " + game.level.ToString();
    }


    public void ChangeHealth(int playerNum){
        if(playerNum == 1) healthP1Text.text = "HP " + player1.health.ToString();
        else if(playerNum == 2) healthP2Text.text = "HP " + player2.health.ToString();
        else Debug.LogWarning("playerNum not valid! - health ui");
    }
    public void ChangeAmmo(int playerNum){
        if(playerNum == 1){ 
            if(p1Weapon.ammo <= 0) p1AmmoText.text = "RELOADING...";
            else p1AmmoText.text = "AMMO " + p1Weapon.ammo.ToString();
        }
        else if(playerNum == 2){
            if(p2Weapon.ammo <= 0) p2AmmoText.text = "RELOADING...";
            else p2AmmoText.text = "AMMO " + p2Weapon.ammo.ToString();
        } 
        else Debug.LogWarning("playerNum not valid! - ammo ui");
    }
    public void ChangeRound(int round){
        roundText.text = "Round " + round.ToString();
    }
}
