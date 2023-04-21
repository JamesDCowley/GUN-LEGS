using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float waitTime;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        time = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(time >= 0){
            time -= Time.deltaTime;
        }
        if(time <= 0){
            Instantiate(objectToSpawn, transform.position , Quaternion.identity);
            time = waitTime;
        }

    }
}
