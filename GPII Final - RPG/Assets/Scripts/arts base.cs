using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class artsbase : MonoBehaviour
{
    public EnemyBase enemy;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindWithTag("Enemy")?.GetComponent<EnemyBase>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Purely debugging
        if(Input.GetKey(KeyCode.Space))
        {
            ArtsDamage();
            Debug.Log("Arts Update Triggered.");
        }
    }

    public void ArtsDamage()
    {
        enemy.health--;
        Debug.Log("Arts Damage Triggered.");
    }
}
