using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember : HeroBase
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayHealth();
        Health();
    }

    public override void Health()
    {
        if (health <= 0)
        {
            displayCharacterHealth.text = "Dead";
        }
    }
}
