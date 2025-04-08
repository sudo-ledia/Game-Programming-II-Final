using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public string characterName;
    public int health;
    public TextMeshProUGUI displayPlayerHealth;


    // Start is called before the first frame update
    void Start()
    {
        displayPlayerHealth = GameObject.FindGameObjectWithTag("PlayerHealth")?.GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        DisplayHealth();
        Health();
        
    }

    public void DisplayHealth()
    {
        displayPlayerHealth.text = "Health: " + health;
    }

    public virtual void Health()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
