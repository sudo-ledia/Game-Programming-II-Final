using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    public string characterName;
    public int health;
    public TextMeshProUGUI displayEnemyHealth;
    public TextMeshProUGUI targetDirection;

    public GameObject hero;
    public GameObject player;

    public GameManager gameManager;
    public CameraControl cameraControl;

    public float lowInterval;
    public float highInterval;
    public float attackTimer = 0f;
    public float attackInterval;

    public int currentHeroIndex = 0;

    public string currentState = "Roaming";

    public NavMeshAgent agent;
    public float range;
    public Transform centrePoint;

    public List<GameObject> heroesInRange = new List<GameObject>();

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        //?.GetComponent<Transform>()
        cameraControl = FindObjectOfType<CameraControl>();
        attackInterval = Random.Range(lowInterval, highInterval);

    }
    // Update is called once per frame
    void Update()
    {
        BaseUpdate();
        CustomUpdate();
    }

    private void BaseUpdate()
    {
        DisplayInfo();
        Health();
        HasTarget();
        PurgeHeroes();

        if (hero != null && currentState == "Attacking")
        {
            Attack();
        }
        else if (hero == null && currentState == "Attacking")
        {
            currentState = "Roaming";
        }
        
        if (currentState == "Roaming")
        {
            Roaming();
        }

        if (heroesInRange.Count <= 0)
        {
            hero = null;
        }
    }

    protected virtual void CustomUpdate()
    {

    }

    public void DisplayInfo()
    {
        displayEnemyHealth.text = "Health: " + health;
        targetDirection.text = "Direction: " + GetDirectionOf(player.transform);
    }

    public virtual void Health()
    {
        if(health <= 0)
        {
            cameraControl.NextTarget();
            Destroy(gameObject);
        }   
    }

    public enum RelativeDirection
    {
        Front,
        Back,
        Side
    }

    public RelativeDirection GetDirectionOf(Transform target)
    {
        Vector3 toTarget = (target.position - transform.position).normalized;

        // SignedAngle parameters are (From, to, and angle)

        /* this function in particular takes the enemies transform.forward angle and compares
        it to the players transform.position based on the y axis,
        which in this case is the global vector3.up - chris note for remembering*/

        float angle = Vector3.SignedAngle(transform.forward, toTarget, Vector3.up);

        if (angle >= -45f && angle <= 45f)
        {
            return RelativeDirection.Front;
        }
        else if (angle > 45f && angle <= 135f)
        {
            return RelativeDirection.Side;
        }  
        else if (angle < -45f && angle >= -135f)
        {
            return RelativeDirection.Side;
        }   
        else
        {
            return RelativeDirection.Back;
        }
    }

    // Enemy Attack Logic

    public void HasTarget()
    {
        if (heroesInRange.Count > 0)
        {
            hero = heroesInRange[0];
        }
        else if (heroesInRange.Count <= 0)
        {
            return;
        }
    }

    public virtual void Attack()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackInterval)
        {
            // if npc allies or something, this will have to be changed to who the enemy is targeting
            hero.GetComponent<HeroBase>().health -= 1;
            attackTimer = 0f;
            attackInterval = Random.Range(lowInterval, highInterval);

            Vector3 enemyDir = hero.transform.position - transform.position;
            enemyDir.y = 0;
            Quaternion enemyTargetRotation = Quaternion.LookRotation(enemyDir);
            transform.rotation = enemyTargetRotation;

            transform.forward = enemyDir.normalized;
        }
    }

    public void Roaming()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            agent.SetDestination(point);
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    // Adding heroes to list
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

    public void PurgeHeroes()
    {
        heroesInRange.RemoveAll(item => item == null);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"
        || other.gameObject.tag == "PartyMember")
        {
            AddHeroesToList(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player"
        || other.gameObject.tag == "PartyMember")
        {
            RemoveHeroesFromList(other.gameObject);
        }

        
    }
}
