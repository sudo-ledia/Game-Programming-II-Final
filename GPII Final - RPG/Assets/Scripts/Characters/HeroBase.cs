using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBase : CharacterBase
{
    
    [Header("Targeting")]
    public List<GameObject> yourEnemiesInRange = new List<GameObject>();

    public int yourEnemyIndex = 0;
    // public GameObject enemy;
    
    void Update()
    {
        PurgeEnemies();
    }
    public void AddYourEnemyToList(GameObject enemy)
    {
        if (!yourEnemiesInRange.Contains(enemy))
        {
            yourEnemiesInRange.Add(enemy);
            Debug.Log("Enemy added: " + enemy.name);
        }
    }

    public void RemoveYourEnemyFromList(GameObject enemy)
    {
        if (yourEnemiesInRange.Contains(enemy))
        {
            yourEnemiesInRange.Remove(enemy);
            Debug.Log("Enemy removed: " + enemy.name);
        }
    }

    public void PurgeEnemies()
    {
        yourEnemiesInRange.RemoveAll(item => item == null);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // gameManager.AddHeroesToList(this.gameObject);
            AddYourEnemyToList(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // gameManager.RemoveHeroesFromList(this.gameObject);
            RemoveYourEnemyFromList(other.gameObject);
        }
    }
}
