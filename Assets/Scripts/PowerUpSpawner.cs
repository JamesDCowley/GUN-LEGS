using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public float respawnTime;
    float time = 0f;
    public GameObject powerUp;
    public float xRange;
    public float yRange;

    // Update is called once per frame
    void Update()
    {
        if(time > 0f){
            time -= Time.deltaTime;
        }
        if(time <= 0f){
            SpawnPowerUp();
            time = respawnTime;
        }
    }

    //Random Position - No parameters
    public void SpawnPowerUp(){
        Vector2 position = new Vector2(Random.Range(-xRange,xRange),Random.Range(-yRange, yRange));
        Instantiate(powerUp, position, Quaternion.identity);
    }

    //Set Position - Parameters
    public void SpawnPowerUp(Vector2 position){
        Instantiate(powerUp, position, Quaternion.identity);
    }
}
