using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    

    float transparency = 1f;
    public float fadeRate = 0.01f;
    SpriteRenderer spriteRender;

    bool isTouching;

    void Start(){
        spriteRender = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision){
        isTouching = true;
    }

    void OnCollisionExit2D(Collision2D collision){
        isTouching = false;
    }

    void Update(){
        if(isTouching){
            transparency -= fadeRate * Time.deltaTime;
            if(transparency <= 0f) Destroy(gameObject);
            spriteRender.color = new Color(spriteRender.color.r, spriteRender.color.b, spriteRender.color.g ,transparency);
            
        }
    }
}
