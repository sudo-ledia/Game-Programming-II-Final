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

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player")?.GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        DisplayInfo();
        Health();
        CallPlayerDir();
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
            Destroy(gameObject);
        }
    }

    public enum RelativeDirection
    {
        Front,
        Back,
        Left, 
        Right
    }

    public RelativeDirection GetDirectionOf(Transform target)
    {
        Vector3 toTarget = (target.position - transform.position).normalized;
        float angle = Vector3.SignedAngle(transform.forward, toTarget, Vector3.up);

        if (angle >= -45f && angle <= 45f)
        {
            return RelativeDirection.Front;
        }
        else if (angle > 45f && angle <= 135f)
        {
            return RelativeDirection.Right;
        }  
        else if (angle < -45f && angle >= -135f)
        {
            return RelativeDirection.Left;
        }   
        else
        {
            return RelativeDirection.Back;
        }
    }

    public virtual void CallPlayerDir()
    {
        var dir = GetDirectionOf(player);
        Debug.Log("Player is to my: " + dir);
    }
}
