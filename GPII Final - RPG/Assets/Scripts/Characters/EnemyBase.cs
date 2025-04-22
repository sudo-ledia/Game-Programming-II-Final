using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public string characterName;
    public int health;
    public TextMeshProUGUI displayEnemyHealth;
    public TextMeshProUGUI targetDirection;

    public GameObject hero;

    public GameManager gameManager;
    public CameraControl cameraControl;

    public float lowInterval;
    public float highInterval;
    public float attackTimer = 0f;
    public float attackInterval;

    public int currentHeroIndex = 0;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindWithTag("Player");

        hero = gameManager.enemiesInRange[currentHeroIndex];
        //?.GetComponent<Transform>()
        cameraControl = FindObjectOfType<CameraControl>();
        attackInterval = Random.Range(lowInterval, highInterval);
    }
    // Update is called once per frame
    void Update()
    {
        DisplayInfo();
        Health();
        Attack();
        // CallPlayerDir();
    }

    public void DisplayInfo()
    {
        displayEnemyHealth.text = "Health: " + health;
        targetDirection.text = "Direction: " + GetDirectionOf(hero.transform);
    }

    public virtual void Health()
    {
        if(health <= 0)
        {
            cameraControl.NextTarget();
            gameManager.RemoveEnemyFromList(this.gameObject);
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

    public virtual void Attack()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackInterval)
        {
            // if npc allies or something, this will have to be changed to who the enemy is targeting
            hero.GetComponent<HeroBase>().health -= 1;
            attackTimer = 0f;
            attackInterval = Random.Range(lowInterval, highInterval);
        }
    }

    // public virtual void CallPlayerDir()
    // {
    //     var dir = GetDirectionOf(player.transform);
    //     // Debug.Log("Player is to my: " + dir);
    // }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.AddEnemyToList(this.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.RemoveEnemyFromList(this.gameObject);
        }
    }
}
