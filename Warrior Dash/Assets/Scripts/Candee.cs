using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candee : Enemy
{
    [SerializeField] float secondsOfFlicker = 0.1f;

    private new void ProcessHit(DamageDealer damageDealer)
    {
        enemyHealth -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (enemyHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Flicker());
        }
    }

    //makes enemy change color for a split second in order to notify it took damage
    //will be used when enemy gets hit
    private IEnumerator Flicker()
    {
        Color originalColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(secondsOfFlicker);
        GetComponent<SpriteRenderer>().color = originalColor;

    }

    //for now we have an empty update in order to prevent it from calling CoundDownAndShoot from parent class;
    private void Update()
    {
        
    }
}
