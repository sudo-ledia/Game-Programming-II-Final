using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerRPG : PlayerMovement
{
    // private GameManager gameManager;
    public CameraControl cameraControl;
    public GameObject enemy;

    public string atkDirection = "None";

    public float autoTimer = 0f;
    public float autoRelease = 3f;

    public float cooldownTimer = 0f;
    public float cooldownRelease = 3f;

    public string currentState = "FreeRoam";
    public bool isBattling = false;

    public TextMeshProUGUI stateText;


    // Start is called before the first frame update
    void Start()
    {
        
        StartFunctions();
        // gameManager = FindObjectOfType<GameManager>();
        cameraControl = FindObjectOfType<CameraControl>();
        stateText = GameObject.FindWithTag("CurrentState")?.GetComponent<TextMeshProUGUI>();
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
        
        stateText.text = "Current State: " + currentState;
        enemy = cameraControl.enemy;

        cooldownTimer += Time.deltaTime;

        if (enemy != null)
        {
            if (Input.GetKeyDown(KeyCode.Return) && isBattling == false)
            {
                isBattling = true;
                currentState = "BattleAuto";
            }

            if (cooldownTimer >= cooldownRelease)
            {

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    currentState = "BattleSpecial";
                    cooldownTimer = 0f;
                    FrontAttack();
                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    currentState = "BattleSpecial";
                    cooldownTimer = 0f;
                    BackAttack();
                }

                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    currentState = "BattleSpecial";
                    cooldownTimer = 0f;
                    SideAttack();
                }
            }

            
            
            if (isBattling)
            {
                if (currentState == "BattleAuto")
                {
                    autoTimer += Time.deltaTime;
                    AutoAttack();
                }
                else if (currentState == "BattleSpecial")
                {
                    autoTimer = autoRelease - 0.01f;
                    cooldownTimer += Time.deltaTime;
                    if (cooldownTimer >= cooldownRelease)
                    {
                        cooldownTimer = 0f;
                        currentState = "BattleAuto";
                    }
                }
            }
        }
        else if (enemy == null)
        {
            isBattling = false;
            currentState = "FreeRoam";
            autoTimer = autoRelease - 0.01f;
        }
    }

    public void AutoAttack()
    {
        if (autoTimer >= autoRelease)
        {
            DirDmgMultiplier(1, 1);
            autoTimer = 0f;
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
        enemy.GetComponent<EnemyBase>().currentState = "Attacking";

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
