using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Asteroid : MonoBehaviour
{
    float speed;

    public UnityEvent asteroidBlownUp = new UnityEvent(); 
    public UnityEvent asteroidDestroyed = new UnityEvent();


    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }


    public void Initialize (float speed)
    { 
        this.speed = speed;
    }

    public void AsteroidBlownUp()
    {
        asteroidBlownUp.Invoke();

        Destroy(gameObject);
    }

    public void AsteroidIsDestroyed()
    {
        asteroidDestroyed.Invoke();

        Destroy(gameObject);
    }
   

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet"))
        {
            Destroy(col.gameObject);

            AsteroidBlownUp();
        }
        else if(!col.CompareTag("Asteroid"))
            AsteroidIsDestroyed();
    }
}