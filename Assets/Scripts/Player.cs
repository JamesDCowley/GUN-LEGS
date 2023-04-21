using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour{
    Rigidbody2D rb;
    Transform player;
    Weapon weapon;
    CameraShake cameraShake;
    Animator animator;
    GameObject shield;
    AudioManager aud;
    GameManager game;
    UI ui;
    public float rotateForce;
    public bool autoReload;
    float speed;
    float time;
    float outofAmmoTime;
    float shieldTime;
    bool sheildActive;
    public int maxHealth;

    public int health;
    public int playerNum;

    //Variable init
    void Start()
    {
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Transform>();
        weapon = GetComponentInChildren<Weapon>();
        animator = GetComponent<Animator>();
        shield = gameObject.transform.GetChild(4).gameObject;
        aud = AudioManager.instance;
        game = GameManager.instance;
        ui = FindObjectOfType<UI>();
        time = weapon.gun.reloadTime;
        outofAmmoTime = 5f;
        shieldTime = 10f;
        sheildActive = false;
        shield.SetActive(false);
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        /*TIMING*/

        //Shooting cooldown
        if(time > 0f){
            animator.SetBool("isShooting", false);
            time -= Time.deltaTime;
        }
        //Reload
        if(autoReload && outofAmmoTime > 0f && weapon.ammo <= 0){
            outofAmmoTime -= Time.deltaTime;
        }
        //Shield active time
        if(sheildActive && shieldTime > 0f){
            shieldTime -= Time.deltaTime;

        }
        //Deactivate Shield
        else if(sheildActive && shieldTime <= 0){
            sheildActive = false;
            DeactivateShield();
        }

        /* MOVEMENT */

        //Shoot
        if(Input.GetKeyDown(playerNum == 1 ? "w" : "up") && time <= 0 && weapon.ammo > 0){
            rb.velocity = (Vector2)player.TransformDirection(Vector3.up) * weapon.gun.recoil;
            time = weapon.gun.reloadTime;
            weapon.Shoot();
            animator.SetBool("isShooting", true);
            ui.ChangeAmmo(playerNum);
        }
        //Running out of ammo with pistol
        else if(weapon.ammo <= 0 && outofAmmoTime <= 0){
            weapon.SetGun("pistol");
            weapon.Reload(); 
            outofAmmoTime = 10f;
        }
        //Runing out of ammo w/o pistol
        else if(weapon.ammo <= 0 && weapon.gun.gunName != "pistol"){
            weapon.SetGun("pistol");
            weapon.Reload();
            outofAmmoTime = 10f;
        }

        //When left key pressed, rotate character right
        if(Input.GetKey(playerNum == 1 ? "a" : "left")){
            player.Rotate(0,0,rotateForce * Time.deltaTime);
        }

        //When left key pressed, rotate character left
        if(Input.GetKey(playerNum == 1 ? "d" : "right")){
            player.Rotate(0,0,-rotateForce * Time.deltaTime);
        }
    }

    public void ActivateShield(int duration){
        shieldTime = duration;
        shield.SetActive(true);
        sheildActive = true;
        Debug.Log(shieldTime);
        
    }

    public void DeactivateShield(){
        sheildActive = false;
        shield.SetActive(false);
        
    }

    public void TakeDamage(int damage){
        Debug.Log("Player " + playerNum + " took " + damage + " damage.");
        health -= damage;
        ui.ChangeHealth(playerNum);
        StartCoroutine(cameraShake.Shake(.15f, .6f));
        if(health <= 0){
            Die();
        }
        else aud.Play("death");
    }

    public void Die(){
        Debug.Log("Player " + playerNum + " is dead!");
        aud.Play("death");
        Destroy(gameObject);
        game.GameOver(playerNum);
    }
}
