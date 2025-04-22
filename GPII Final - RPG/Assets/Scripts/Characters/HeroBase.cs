using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBase : CharacterBase
{

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            gameManager.AddHeroesToList(this.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            gameManager.RemoveHeroesFromList(this.gameObject);
        }
    }
}
