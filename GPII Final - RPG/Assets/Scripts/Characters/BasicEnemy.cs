using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        // hero = GameObject.FindWithTag("Player");
        // hero = heroesInRange[currentHeroIndex];
        player = GameObject.FindWithTag("Player");
        cameraControl = FindObjectOfType<CameraControl>();
    }

    // Update is called once per frame
    protected override void CustomUpdate()
    {
        
    }
}
