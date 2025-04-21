using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Targeting")]
    public List<GameObject> enemiesInRange = new List<GameObject>();
    
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
}
