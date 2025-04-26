using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerTransform;
    public PlayerRPG playerRPG;
    public Transform playerObj;
    public GameObject enemy;
    public Rigidbody rb;
    public GameObject targetLock;

    public float rotationSpeed;

    public bool isTargeting = false;

    public GameObject virtualCamera;
    public GameObject freeLookCamera;

    public int currentEnemyIndex = 0;

    // Start is called before the first frame update
    void Awake()
    {
        playerRPG = FindObjectOfType<PlayerRPG>();
    }
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy != null)
        {
            targetLock.transform.position = enemy.transform.position;
        }
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isTargeting = !isTargeting;

            if (isTargeting && playerRPG.yourEnemiesInRange.Count > 0)
            {
                currentEnemyIndex = 0;
                enemy = playerRPG.yourEnemiesInRange[currentEnemyIndex];
            }
        }
        
        if (playerRPG.yourEnemiesInRange.Count <= 0)
        {
            enemy = null;
        }
        if (enemy == null)
        {
            isTargeting = false;
        }

        if(isTargeting)
        {
            targetLock.SetActive(true);
            virtualCamera.SetActive(true);
            freeLookCamera.SetActive(false);
            TargetCam();

            if (Input.GetKeyDown(KeyCode.E))
            {
                NextTarget();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                PreviousTarget();
            }
        }
        else if(!isTargeting)
        {
            targetLock.SetActive(false);
            virtualCamera.SetActive(false);
            freeLookCamera.SetActive(true);
            FreeLookCam();
        }
    }

    public void FreeLookCam()
    {

        enemy = null;
        //Rotation Orient
        Vector3 viewDir = playerTransform.position - new Vector3(
            transform.position.x,
            playerTransform.position.y,
            transform.position.z);
        orientation.forward = viewDir.normalized;

        //rotating player object
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(inputDir != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }

    public void NextTarget()
    {
        currentEnemyIndex++;

        if (currentEnemyIndex >= playerRPG.yourEnemiesInRange.Count)
        {
            currentEnemyIndex = 0;
        }
        enemy = playerRPG.yourEnemiesInRange[currentEnemyIndex];
    }

    public void PreviousTarget()
    {
        currentEnemyIndex--;
        if (currentEnemyIndex < 0)
        {
            currentEnemyIndex = playerRPG.yourEnemiesInRange.Count - 1;
        }
        enemy = playerRPG.yourEnemiesInRange[currentEnemyIndex];
    }

    public void TargetCam()
    {   
        if (enemy != null)
        {
            Vector3 playerPreDir = enemy.transform.position - playerTransform.transform.position;
            playerPreDir.y = 0;
            Quaternion playerPreTargetRotation = Quaternion.LookRotation(playerPreDir);
            playerTransform.transform.rotation = playerPreTargetRotation;

            Vector3 playerObjDir = enemy.transform.position - playerObj.transform.position;
            playerObjDir.y = 0;
            Quaternion playerObjTargetRotation = Quaternion.LookRotation(playerObjDir);
            playerObj.transform.rotation = playerObjTargetRotation;

            orientation.forward = playerPreDir.normalized;
        }
        else if (enemy == null && playerRPG.yourEnemiesInRange.Count > 0)
        {
            NextTarget();
            Debug.Log("Enemy null, skip");
        }
        else if (enemy == null && playerRPG.yourEnemiesInRange.Count < 1 && isTargeting)
        {
            isTargeting = false;
        }
    
        
    }
}
