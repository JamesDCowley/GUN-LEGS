using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    Player player;
    Weapon weapon;
    Weapon tempWeapon;
    AudioManager aud;
    public string curPowerUp;
    public string[] powerUps;
    public SpriteRenderer powerUpSprite;
    public Sprite[] sprites;
    public bool random;
    int gunPickup;
    UI ui;

    // Start is called before the first frame update
    void Start()
    {
        aud = AudioManager.instance;
        ui = FindObjectOfType<UI>();
        //sets powerup to random powerup
        if(random){
            curPowerUp = powerUps[(int)Random.Range(0, powerUps.Length)];
        }
        powerUpSprite = GetComponent<SpriteRenderer>();
        tempWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<Weapon>();
        Debug.Log(tempWeapon.weapons.Length);
        if(curPowerUp == "gun"){
            gunPickup = (int)Random.Range(1, tempWeapon.weapons.Length);
        }
        ChangeSprite();
        
    }
    //give powerup when touched by player
    void OnTriggerEnter2D(Collider2D collider){
        player = collider.GetComponent<Player>();
        weapon = collider.GetComponent<Weapon>();
        if(player != null && weapon != null){
            aud.Play("powerup");
            ActivatePowerUp(curPowerUp);
            Destroy(gameObject);
        }
    }

    //power up effects
    public void ActivatePowerUp(string powerUpName){
        switch(powerUpName){
            case "health" :
                HealUp((int)Random.Range(5,20));
                Debug.Log("Health Pickup");
                break;
            case "ammo" :
                weapon.Reload();
                Debug.Log("Reload Pickup");
                break;
            case "gun":
                weapon.SetGun(weapon.weapons[gunPickup]);
                Debug.Log("Gun Pickup");
                break;
            case "shield":
                player.ActivateShield(10);
                Debug.Log("Shield Pickup");
                break;
            default :
                Debug.Log("power up invalid.");
                break;
        }
    }

    //sets the sprite for a powerup object
    public void ChangeSprite(){
        switch(curPowerUp){
            case "health" :
                powerUpSprite.sprite = sprites[0];
                break;
            case "ammo" : 
                powerUpSprite.sprite = sprites[1];
                break;
            case "gun" : 
                powerUpSprite.sprite = tempWeapon.weapons[gunPickup].sprite;
                break;
            case "shield" :
                powerUpSprite.sprite = sprites[2];
                break;
            default : Debug.Log("invalid powerup sprite");break;
        }
    }
    //heals the player (for medkit pwr up)
    public void HealUp(int healthToHeal){
        if(player.maxHealth < healthToHeal + player.health)
            player.health = player.maxHealth;
        else
            player.health += healthToHeal;
        ui.ChangeHealth(player.playerNum);
    }    
}
