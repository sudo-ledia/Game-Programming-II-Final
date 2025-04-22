using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public string characterName;
    public int health;
    public TextMeshProUGUI displayCharacterHealth;

    public GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        displayCharacterHealth = GameObject.FindGameObjectWithTag("PlayerHealth")?.GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        DisplayHealth();
        Health();
        
    }

    public void DisplayHealth()
    {
        displayCharacterHealth.text = "Health: " + health;
    }

    public virtual void Health()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
