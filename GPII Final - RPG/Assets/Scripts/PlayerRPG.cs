using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRPG : PlayerMovement
{
    private GameManager gameManager;
    public CameraControl cameraControl;
    public GameObject enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        StartFunctions();
        gameManager = FindObjectOfType<GameManager>();
        cameraControl = FindObjectOfType<CameraControl>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        MyInput();
        DragHandler();

        enemy = cameraControl.enemy;

        if (enemy != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                enemy.GetComponent<EnemyBase>().health--;
                Debug.Log("Enemy defeated.");
            }
        }
        else if (enemy == null)
        {
            return;
        }
        

    }
}
