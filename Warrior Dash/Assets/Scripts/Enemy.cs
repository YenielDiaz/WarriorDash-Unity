using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] protected int enemyHealth = 100;
    [SerializeField] int scoreValue = 200;

    [Header("Shooting")]
    float shotCounter; //attribute to allow us to control how often enemy shoots
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed;

    /*
    [Header("SFX")]
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float deathDuration = 1f;
    [SerializeField] AudioClip deathSFX;
    [Range(0, 1)] [SerializeField] float deathSFXVolume = 0.7f;
    [SerializeField] AudioClip laserSFX;
    [Range(0, 1)] [SerializeField] float laserSFXVolume = 0.7f;
    */

    [Header("Drops")]
    [Range(0,10)][SerializeField] float dropRate = 2;
    [SerializeField] GameObject[] dropPrefabs;
    [SerializeField] Vector2 dropSpeed = new Vector2(0, 2);

    protected virtual void Start()
    {
        //setup shot counter
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    void Update()
    {
        CountdownAndShoot();
    }

    //when entering collision with an object that is an instance of damageDealer, process hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer) { return; }

        ProcessHit(damageDealer);
    }

    //reduce health based on the damage of the damageDealer that collided and die if health reached zero
    protected virtual void ProcessHit(DamageDealer damageDealer)
    {
        enemyHealth -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    //destroy gameobject and trigger death vfx,sfx
    protected virtual void Die()
    {
        int generatedInt = Random.Range(0, 11);
        if(generatedInt < dropRate  && FindObjectOfType<Pickup>() == null)
        {
            //spawn the drop
            var drop = Instantiate(dropPrefabs[0], transform.position, Quaternion.identity);
            //dropped item spawns without prefabSlot set o we have to set it when we spawn it
            drop.GetComponent<Pickup>().SetPickupPrefab(dropPrefabs[0]);
            //make it fall downwards
            drop.GetComponent<Rigidbody2D>().velocity = -dropSpeed;
        }
        FindObjectOfType<GameSession>().AddToScore(scoreValue); //add to score based on value
        Destroy(gameObject);
        /*
        GameObject explosion = Instantiate(explosionPrefab,
            transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, deathDuration);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        */
    }

    //reduce shotCounter, shoot when shotCounter = 0, then reset shotCounter
    protected void CountdownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            /*
            AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position,
                laserSFXVolume);
            */
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    //make enemy shoot
    protected void Fire()
    {
        GameObject laser = Instantiate(laserPrefab,
            transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

    private int GetHealth() { return enemyHealth; }
    private void setHealth(int health) { enemyHealth = health; }



}
