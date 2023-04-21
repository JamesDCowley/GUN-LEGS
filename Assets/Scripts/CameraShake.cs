using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //Shakes camera whenever player is hit or a gun is shot
    public IEnumerator Shake(float duration, float magnitude){
        Vector3 originalPos = transform.localPosition;

        float time = 0.0f;

        while(time < duration){
            float x = Random.Range(-1f,1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            
            transform.localPosition = new Vector3(x, y, originalPos.z);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
