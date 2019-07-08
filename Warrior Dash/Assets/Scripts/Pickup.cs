using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] protected Player player;
    [SerializeField] protected int healthRegained = 100;
    [SerializeField] protected GameObject pickupPrefab;
    [SerializeField] protected int originalAmmoCount = 20; //number of uses before powerUp dissappears
    [SerializeField] protected int ammoCount = 20; //current number of uses left


    public virtual void Benefit()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    public void ResetAmmoCount() { ammoCount = originalAmmoCount; }

    //Getters
    public int GetHealthRegained() { return healthRegained; }
    public Sprite GetSprite() { return GetComponent<SpriteRenderer>().sprite; }
    public int GetAmmoCount() { return ammoCount; }
    public GameObject GetPickupPrefab() { return pickupPrefab; }

    //Setters
    public void SetAmmoCount(int newCount) { ammoCount = newCount; }
    public void SetPickupPrefab(GameObject prefab) { pickupPrefab = prefab; }
}
