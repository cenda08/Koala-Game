using UnityEngine;
using System.Collections;

public class PhoneScript : MonoBehaviour
{
    public GameObject Phone;
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
        
    }

    IEnumerator OnMouseDown()
    {

        HintScreen.SetActive(true);
        yield return new WaitForSeconds(7);
        HintScreen.SetActive(false);
        yield return null;
    }
}
