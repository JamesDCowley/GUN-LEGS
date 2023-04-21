using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    public int ammo;
    public Gun gun;
    public Gun[] weapons;
    public GameObject bulletPrefab;
    public Transform[] firePoints;
    public SpriteRenderer weaponSprite;
    Player player;
    AudioManager aud;
    CameraShake cameraShake;
    UI ui;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        aud = AudioManager.instance;
        ui = FindObjectOfType<UI>();
        gun = weapons[0];
        SetGun();
        Reload();
    }
    
    //Spawns bullet at firepoint depending on the number of firepoints
    public void Shoot(){
        if(ammo > 0){
            aud.Play("hit");
            switch(gun.firePoints){
                case 1 : 
                    GameObject bullet = Instantiate(bulletPrefab, firePoints[0].position, firePoints[0].rotation);
                    bullet.GetComponent<Bullet>().SetBulletType(gun);
                    StartCoroutine(cameraShake.Shake(.15f, .1f));
                    ammo--;
                    break;

                case 3 :
                    foreach(Transform pos in firePoints){
                        Bullet bulle = Instantiate(bulletPrefab, pos.position, pos.rotation).GetComponent<Bullet>();
                        bulle.SetBulletType(gun);
                    }
                    StartCoroutine(cameraShake.Shake(.15f, .15f));
                    ammo--;
                    break;
                default : Debug.LogWarning("NUM FIREPOINTS INVALID!");break;
            }
        }
        else{
            Debug.Log("OUT OF AMMO!");
        }
    }

    //sets weapon to specific gun (pistol is default)
    public void SetGun() { SetGun("pistol"); }
    public void SetGun(string weapon){
        gun = Array.Find(weapons, g => g.gunName == weapon);
        if(gun == null) {Debug.LogWarning("Weapon " + weapon + " doesn't exist!"); gun = weapons[0];}
        ChangeSprite();
        Reload();
    }
    public void SetGun(Gun weapon){
        gun = weapon;
        ChangeSprite();
        Reload();
    }
    
    //switches gun between pistol and shotgun
    public void SwitchGun(){
        switch(gun.gunName){
            case "pistol" : SetGun("shotgun");break;
            case "shotgun" : SetGun("pistol");break;
            default : SetGun();break;
        }
    }

    public void ChangeSprite(){
        weaponSprite.sprite = gun.sprite;
    }

    //reloads weapon
    public void Reload(){
        ammo = gun.maxAmmo;
        ui.ChangeAmmo(player.playerNum);
    }
}


