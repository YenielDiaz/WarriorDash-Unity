using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toughee : Enemy
{
    [SerializeField] Sprite[] hitSprites;
    int spriteIndex = 0;

    //when entering collision with an object that is an instance of damageDealer, process hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer) { return; }

        ProcessHit(damageDealer);
    }
    //reduce health based on the damage of the damageDealer that collided and die if health reached zero
    protected new void ProcessHit(DamageDealer damageDealer)
    {
        enemyHealth -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (enemyHealth <= 0)
        {
            Die();
        }
        else
        {
            
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        spriteIndex++;
        if(hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite of " + gameObject.name + "is missing from array");
        }
    }
}
