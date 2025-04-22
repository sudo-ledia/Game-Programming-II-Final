using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Targeting")]
    public List<GameObject> enemiesInRange = new List<GameObject>();

    public List<GameObject> heroesInRange = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEnemyToList(GameObject enemy)
    {
        if (!enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Add(enemy);
            Debug.Log("Enemy added: " + enemy.name);
        }
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        if (enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Remove(enemy);
            Debug.Log("Enemy removed: " + enemy.name);
        }
    }

    public void AddHeroesToList(GameObject hero)
    {
        if (!heroesInRange.Contains(hero))
        {
            heroesInRange.Add(hero);
            Debug.Log("Hero added: " + hero.name);
        }
    }

    public void RemoveHeroesFromList(GameObject hero)
    {
        if (heroesInRange.Contains(hero))
        {
            heroesInRange.Remove(hero);
            Debug.Log("Hero removed: " + hero.name);
        }
    }
}
