using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember : HeroBase
{
    
    public float lowInterval;
    public float highInterval;
    public float attackTimer = 0f;
    public float attackInterval;

    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
        attackInterval = Random.Range(lowInterval, highInterval);
    }

    // Update is called once per frame
    void Update()
    {
        DisplayHealth();
        Health();

        if (enemy != null)
        {
            Attack();
        }
       
        PurgeEnemies();

        if (yourEnemiesInRange.Count > 0)
        {
            enemy = yourEnemiesInRange[0];
        }
        else if (yourEnemiesInRange.Count < 0)
        {
            return;
        }
    }

    public override void Health()
    {
        if (health <= 0)
        {
            displayCharacterHealth.text = "Dead";
        }
    }

    public virtual void Attack()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackInterval)
        {
            // if npc allies or something, this will have to be changed to who the enemy is targeting
            enemy.GetComponent<EnemyBase>().health -= 1;
            attackTimer = 0f;
            attackInterval = Random.Range(lowInterval, highInterval);

            Vector3 partyMemDir = enemy.transform.position - transform.position;
            partyMemDir.y = 0;
            Quaternion partyMemTargetRotation = Quaternion.LookRotation(partyMemDir);
            transform.rotation = partyMemTargetRotation;

            transform.forward = partyMemDir.normalized;
        }
    }
}
