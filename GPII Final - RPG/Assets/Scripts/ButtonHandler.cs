using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public Button[] buttons; //my 8 attack buttons
    public KeyCode[] assignableKeys; //rebindable keys for buttons

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (Input.GetKeyDown(assignableKeys[i]))
            {
                buttons[i].onClick.Invoke();
            }
        }
    }
}
