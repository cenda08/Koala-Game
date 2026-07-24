using UnityEngine;
using System.Collections;

public class PhoneScript : MonoBehaviour
{

    public LogicScript logic;
    public GameObject HintScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
      
    }

    // Update is called once per frame
    void Update()
    {
        void OnMouseDown()
        {

            HintScreen.SetActive(true);
        }
    }
}
