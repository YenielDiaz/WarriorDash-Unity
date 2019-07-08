using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config parameters
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f; //we use it to make sure the player does not go over edges of screen

    [Header("Projectiles")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 2f;
    [SerializeField] GameObject fireballPrefab;
    [SerializeField] float fireballSpeed = 5f;
    //multishot vectors
    [SerializeField] Vector2 shot1Velocity = new Vector2(0, 5);
    [SerializeField] Vector2 shot2Velocity = new Vector2(-2, 5);
    [SerializeField] Vector2 shot3Velocity = new Vector2(2, 5);

    /*
    [SerializeField] AudioClip laserSFX;
    [Range(0, 1)] [SerializeField] float laserSFXVolume = 0.7f;
    */

    [Header("Status")]
    [SerializeField] float health = 1000;
    /*
    [SerializeField] AudioClip deathSFX;
    [Range(0, 1)] [SerializeField] float deathSFXVolume = 0.7f;
    */
    [SerializeField] float secondsOfFlicker = 0.1f;
    [SerializeField] GameObject powerUp;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    Coroutine fireCoroutine;

    //cached references
    Level level;
    GameSession gameSession;
    SkillController skillCont;
    PowerUpIcon powerUpIcon;

    void Start()
    {
        level = FindObjectOfType<Level>();
        skillCont = FindObjectOfType<SkillController>();
        powerUpIcon = FindObjectOfType<PowerUpIcon>();
        SetupMoveBoundaries();
    }


    // Call Move method and Fire method every frame
    void Update()
    {
        Move();
        Fire();
    }

    //frame independent movement
    private void Move()
    {
        //returns x based on the input
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        //returns y based on the input
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    //method calls the fireContinously coroutine as long as the fire button is held down
    private void Fire()
    {
        HandleFire1();

        HandleFire2();

        HandleFire3();
    }

    //what to do when Fire1 button is pressed
    private void HandleFire1()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(FireContinously());

        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    //what to do when Fire2 button is pressed (alt fire)
    private void HandleFire2()
    {
        //added this to shoot special move, lets see if it works
        if (!skillCont.GetIsCooldown())
        {
            if (Input.GetButtonDown("Fire2"))
            {
                skillCont.SetIsCoolDown(true);
                var fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
                fireball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, fireballSpeed);
            }
        }
    }

    private void HandleFire3()
    {
        if (Input.GetButtonDown("Fire3") && powerUp != null)
        {
            int ammoLeft = powerUp.GetComponent<Pickup>().GetAmmoCount();
            if (powerUp.GetComponent<MultiShotPickup>() != null) MultiShot();
            ammoLeft--;
            powerUp.GetComponent<Pickup>().SetAmmoCount(ammoLeft);

            if (ammoLeft < 1)
            {
                powerUp = null;
                powerUpIcon.SetPowerUp(null);
                powerUpIcon.Reset();
            }
        }
        
    }

    private void MultiShot()
    {
        var shot1 = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        var shot2 = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        var shot3 = Instantiate(laserPrefab, transform.position, Quaternion.identity);

        shot1.GetComponent<Rigidbody2D>().velocity = shot1Velocity;
        shot2.GetComponent<Rigidbody2D>().velocity = shot2Velocity;
        shot3.GetComponent<Rigidbody2D>().velocity = shot3Velocity;

    }

    //coroutine that lets player fire bullets continuously
    IEnumerator FireContinously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab,
                transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
           // AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position,
           //     laserSFXVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }


    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer)
        {
            Pickup pickup = collision.gameObject.GetComponent<Pickup>();
            
            if (!pickup) return;

            ProcessPickup(pickup);
            return;
        }

        ProcessHit(damageDealer);
    }

    //dictates what happens if player gets a pickup
    private void ProcessPickup(Pickup pickup)
    {
        health += pickup.GetHealthRegained(); //every pickup has a heal amount
        powerUp = pickup.GetPickupPrefab();
        Pickup powerUpPickup = powerUp.GetComponent<Pickup>();
        powerUpPickup.ResetAmmoCount();

        powerUpIcon.SetPowerUp(powerUp);
        powerUpIcon.SetText(powerUpPickup.GetAmmoCount());
        powerUpIcon.SetIcon(powerUpPickup.GetSprite()); // call the setIcon method for the HUD powerUp icon
    }

    //dictates what happens when player gets hit
    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
            level.LoadGameOver();
        }
        else
        {
            StartCoroutine(Flicker());
        }
    }

    //handles the death of the player
    private void Die()
    {
        Destroy(gameObject);
        //AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }

    //getter method that returns health
    public int GetHealth() { return (int)health; }

    private IEnumerator Flicker()
    {
        Color originalColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(secondsOfFlicker);
        GetComponent<SpriteRenderer>().color = originalColor;

    }


    //PowerUp Getter and Setter
    public GameObject GetPowerUp() { return powerUp; }
    public void SetPickup(GameObject pickup) { powerUp = pickup;}

    //Position Getter
    public Vector3 GetCurrentPos() { return transform.position;  }

    //with this method we convert our boundaires into world units in order to make sure units do not go past boundaires
    //regardless of the screen that it is played on
    private void SetupMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding * 4;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
