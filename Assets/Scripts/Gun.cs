using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gun
{
    public string gunName;
    public Sprite sprite;
    public int maxAmmo;
    public float reloadTime;

    [Range(1f,20f)]
    public float recoil;
    public int firePoints;

    public Sprite bulletSprite;
    [Range(1,50)]
    public int damage;
    [Range(10f,50f)]
    public float bulletSpeed;

    
    
}
