using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeSpawner : MonoBehaviour
{
    public float timeToDespawn;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = timeToDespawn;
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0f) time -= Time.deltaTime;
        if(time <= 0f){
            Destroy(gameObject);
            time = timeToDespawn;
        }
    }
}
