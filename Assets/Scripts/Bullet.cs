using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage;
    Rigidbody2D rb;
    Sprite sprite;
    Gun gun;
    SpriteRenderer bulletSprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletSprite = GetComponent<SpriteRenderer>();
        rb.velocity = (Vector2)transform.TransformDirection(Vector3.down) * speed;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Player player = hitInfo.GetComponent<Player>();
        if(player != null && hitInfo.tag != "NonHittable") player.TakeDamage(damage);
        if(hitInfo.tag != "NonHittable") Destroy(gameObject);            
    }

    public void SetBulletType(Gun gun){
        this.gun = gun;
        damage = gun.damage;
        speed = gun.bulletSpeed;
        if(gun.bulletSprite != null) bulletSprite.sprite = gun.bulletSprite;
    }
}
