using UnityEngine;
using System.Collections;

public class PhoneScript : MonoBehaviour
{
    public GameObject Phone;
    public LogicScript logic;
    public GameObject HintScreen;
    public GameObject Accept;
    public GameObject Decline;
    public bool CanClickHint = true;
    public GameObject vykricnik;
    private Coroutine phoneCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        StartCoroutine (PhoneSequence());
    }

    // Update is called once per frame
    void Update()
    {
       // if (CanClickHint == false &&)
    }

  

    IEnumerator PhoneSequence()
    {
        yield return new WaitForSeconds(45);

       phoneCoroutine = StartCoroutine(PhoneRings());
 
      
    }
    IEnumerator PhoneRings()
    {
        logic.Hint.SetActive(false);
        CanClickHint = false;
        vykricnik.SetActive(true);
        Accept.SetActive(true);
        Decline.SetActive(true);

            for (int l = 0; l<9; l++)
            {
                logic.ScoreDecrease(1);
            yield return new WaitForSeconds(1);
            }

        EndPhone();           
      
    }
    public void DeclineCall() {
       
        StopCoroutine(phoneCoroutine);
        EndPhone();
    }

    public void AcceptCall()
    {
        CanClickHint = false;
        Accept.SetActive(true);
        Decline.SetActive(false);
        vykricnik.SetActive(false);


        StopCoroutine(phoneCoroutine);


        StartCoroutine(AcceptTimer());


    }

    void EndPhone()
    {
        Accept.SetActive(false);
        Decline.SetActive(false);
        vykricnik.SetActive(false);

        CanClickHint = true;
    }

    void OnMouseDown()
    {
        if (CanClickHint)
        {
            StartCoroutine(Hint());
        }
    }


    IEnumerator Hint()
    {
        HintScreen.SetActive(true);
        yield return new WaitForSeconds(7);
        HintScreen.SetActive(false);
        logic.ScoreDecrease(3);
    }

    IEnumerator AcceptTimer()
    {
        for (int i = 0; i < 10; i++)
        {
            logic.ScoreDecrease(1);

            yield return new WaitForSeconds(1);
        }

        EndPhone();
    }
}