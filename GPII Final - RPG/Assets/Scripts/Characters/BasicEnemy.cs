using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyBase
{
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
}
