using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRPG : PlayerMovement
{
    // private GameManager gameManager;
    public CameraControl cameraControl;
    public GameObject enemy;

    public string atkDirection = "None";
    
    // Start is called before the first frame update
    void Start()
    {
        StartFunctions();
        // gameManager = FindObjectOfType<GameManager>();
        cameraControl = FindObjectOfType<CameraControl>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        MyInput();
        DragHandler();
        DisplayHealth();
        PurgeEnemies();
        Health();

        enemy = cameraControl.enemy;

        if (enemy != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                FrontAttack();
            }

            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                BackAttack();
            }

            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SideAttack();
            }
        }
        else if (enemy == null)
        {
            return;
        }

    }

    public void FrontAttack()
    {
        atkDirection = "Front";
        Debug.Log("Front attack.");
        DirDmgMultiplier(3, 2);
    }

    public void BackAttack()
    {
        atkDirection = "Back";
        Debug.Log("Back attack.");
        DirDmgMultiplier(3, 2);
    }

    public void SideAttack()
    {
        atkDirection = "Side";
        Debug.Log("Side attack.");
        DirDmgMultiplier(3, 2);
    }

    public void DirDmgMultiplier(int baseDamage, int dmgMultiplier)
    {
        string relativeDir = enemy.GetComponent<EnemyBase>().GetDirectionOf(transform).ToString();

        if (atkDirection == relativeDir)
        {
            enemy.GetComponent<EnemyBase>().health -= baseDamage * dmgMultiplier;
            Debug.Log("Damage Multiplied");
        }
        else
        {
            enemy.GetComponent<EnemyBase>().health -= baseDamage;
        }
    }

    public override void Health()
    {
        if (health <= 0)
        {
            displayCharacterHealth.text = "Dead";
        }
    }
}
