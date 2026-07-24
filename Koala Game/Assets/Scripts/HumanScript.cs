using UnityEngine;
using System.Collections;

public class HumanScript : MonoBehaviour

{

    //Scale na konci:X 1,17 Y 2,79
    //Box collider kterej je trigger: jede zeshora a zvětšuje se, když se dotknou humancollider a koalacollider (trigger)
    //Target x position je 6,89 (Tam furt asi bude ve stejný linii) a y je 2,79

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject Human;
    public float HumanSpeed;
    public float TargetDown;
    public LogicScript logic;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        StartCoroutine (HumanSequence());
    }


    // Update is called once per frame
    void Update()
    {
        //if (se hitne human collider s koala colliderem)
        //logic.GameOver();

        //if když se human pohybuje a udělá se prvně scroll button a pak namíření na human (1 + 1 OnMouseDown jak v BirdScript) tak se skončí coroutine, ale odečte se 15 sleep score 
        
    }

    IEnumerator HumanSequence()
    {
        yield return new WaitForSeconds(60);

        StartCoroutine(HumanMovement());

    }

    IEnumerator HumanMovement()
    {
        while (transform.position.y >= TargetDown)
        {
            transform.Translate(Vector3.down * HumanSpeed * Time.deltaTime);
            Debug.Log("pohyb člověka dolů");
            yield return null;

            //TU PŘIDAT NĚCO NA ZVĚTŠOVÁNÍ SCALE DOKUD NEHITNE TO CO JSEM PSALA

        }



    }
}
  
