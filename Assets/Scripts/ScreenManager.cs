using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// This class needs to handle the different screens that are used
// have serialized fields for private variables for:
// main menu
// character select screen
// transition -- win / lose / loading screen -- fade in / fade out
// story?

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;

    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject characterSelect;

    [SerializeField]
    private GameObject transitionScreen;

    [SerializeField]
    private GameObject fightScreen;


    [SerializeField]
    private List<Button> buttons = new List<Button>();

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
