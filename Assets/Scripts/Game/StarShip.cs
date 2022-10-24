using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StarShip : MonoBehaviour
{
    [Header("StarShip Parametrs")]
    [SerializeField] float maxSpeed;
    float moveH, moveV;
    [SerializeField] Vector2 spawnPosition;
    Vector2 maxShipPosition;
    
    int health;
    bool immunety;
    [Space]
    [SerializeField] float immunetyTime;

    


    [Header("Fire Parametrs")]
    [SerializeField] int rateOfFire;
    [SerializeField] float bulletSpeed;
    [SerializeField] Color bulletColor;
    [SerializeField] GameObject bulletPrefab;
    GameObject shotPoint;
    bool readyToShoot;


    [Header("Containers")]
    [SerializeField] GameObject bulletsContainer;


    SpriteRenderer sr;
    

    //Events
    public UnityEvent<int> damage = new UnityEvent<int>();
    public UnityEvent gameOver = new UnityEvent();


    void Start()
    {
        NewGame();

        shotPoint = transform.GetChild(0).gameObject;

        sr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");


        transform.position = new Vector3(Mathf.Clamp(transform.position.x + moveH * maxSpeed * Time.deltaTime, -maxShipPosition.x, maxShipPosition.x),
                                         Mathf.Clamp(transform.position.y + moveV * maxSpeed * Time.deltaTime, -maxShipPosition.y,maxShipPosition.y), 
                                         0);

        if (readyToShoot && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            Shoot();

            StartCoroutine(ShotDelay());
        }    


            if (immunety)
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.PingPong(Time.time * 4, 1));
    }




    public void Shoot()
    {
            GameObject bullet = Instantiate(bulletPrefab, shotPoint.transform.position, transform.rotation, bulletsContainer.transform);
            bullet.GetComponent<Bullet>().BulletInitialize(bulletColor, bulletSpeed);
    }

    void Damage()
    {
        health--;
        StartCoroutine(ImmunetyDelay());

        damage.Invoke(health);

        transform.position = spawnPosition;

        
        if (health == 0)
        {
            gameOver.Invoke();
            gameObject.SetActive(false);
        }
    }



    public void NewGame()
    {
        transform.position = spawnPosition;


        Camera mainCam = Camera.main;
        maxShipPosition = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height)) * 0.9f;


        health = 3;
        
        immunety = false;

        readyToShoot = true;
    }


    IEnumerator ShotDelay()
    {
        readyToShoot = false;
        yield return new WaitForSeconds(1f / rateOfFire);
        readyToShoot = true;
    }

    IEnumerator ImmunetyDelay()
    {
        immunety = true;
        yield return new WaitForSeconds(immunetyTime);
        immunety = false;

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!immunety && col.CompareTag("Asteroid"))
        { 
            Damage();
        }
    }
}