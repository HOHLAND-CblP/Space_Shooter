using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed;

    float distance;
    float maxDistance = 20;
    float deltaDistance;

    

    void Update()
    {
        deltaDistance = speed * Time.deltaTime;
        transform.Translate(Vector2.up * deltaDistance);
        distance += deltaDistance;

        if (distance > maxDistance)
            Destroy(gameObject);
    }

    public void BulletInitialize(Color32 color, float speed)
    {
        this.speed = speed;
        GetComponent<SpriteRenderer>().color = color;
    }
}