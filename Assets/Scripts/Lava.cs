using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public int damage;
    bool collisionStay = false;
    public bool destoryObject;
    Collision2D collision = null;
    Player player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionStay = true;
        this.collision = collision;
        player = collision.gameObject.GetComponent<Player>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collisionStay = false;
        this.collision = collision;
        player = null;
    }

    void Update()
    {
        if (collisionStay)
        {
            if(player != null) player.TakeDamage(damage);
            else if(destoryObject) Destroy(collision.gameObject);
            Debug.Log("lava hit");
        }
    }
}
