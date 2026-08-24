using System.Collections;
using UnityEngine;
public class Snake_Script : MonoBehaviour
{

    public GameObject Snake;
    public float SnakeSpeed;
    public float TargetDown;
    public float TargetDown2;
    public float TargetUp;
    public float TargetUp2;
    public float TargetLeft;
    public float TargetRight;
    public float TargetRight2;
    public LogicScript logic;
    private bool SnakeIsMoving = false;
    public bool Started = false;
    //private bool SnakeIsMoving = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (logic.eventCycle.Count > 0 && logic.eventCycle[0] == "Snake" && !Started)
        {
            StartCoroutine(SnakeSequence());
            Started = true;
        }
        if (SnakeIsMoving == true && Input.anyKeyDown)
        {
            logic.GameOver();
            logic.eventCycle.RemoveAt(0);
            logic.eventCooldowns.RemoveAt(0);
            logic.TimerText.text = logic.CurrentPhase.ToString() + " / " + logic.TotalEvents;
            Started = false;
        }
    }

    IEnumerator SnakeSequence()
    {
        Debug.Log("Starting SnakeScript, cooldown:" + logic.eventCooldowns[0]);
        yield return new WaitForSeconds(logic.eventCooldowns[0]);

        StartCoroutine(SnakeMovement());

    }


        IEnumerator SnakeMovement()
    {
        SnakeIsMoving = true; //pozdějc se použije pro game over když se klikne

       while (transform.position.y >= TargetDown)
        {
            transform.Translate(Vector3.down * SnakeSpeed * Time.deltaTime);
            //Debug.Log ("pohyb dolů");
            yield return null;

        }


        while (transform.position.x <= TargetRight)
        {
            transform.Translate(Vector3.right * SnakeSpeed * Time.deltaTime);
            //Debug.Log("pohyb doprava");
            yield return null;
        }

        while (transform.position.y <= TargetUp)
        {
            transform.Translate(Vector3.up * SnakeSpeed * Time.deltaTime);
           //Debug.Log("pohyb nahoru");
            yield return null;
        }


        while (transform.position.x <= TargetRight2)
        {
            transform.Translate(Vector3.right * SnakeSpeed * Time.deltaTime * 1/2);
            //Debug.Log("pohyb doprava");
            yield return null;
        }

        while (transform.position.y >= TargetDown2)
        {
            transform.Translate(Vector3.down * SnakeSpeed * Time.deltaTime);
            Debug.Log("pohyb dolů");
            yield return null;

        }
        while (transform.position.x >= TargetLeft)
        {
            transform.Translate(Vector3.left * SnakeSpeed * Time.deltaTime);
            //Debug.Log("pohyb doleva");
            yield return null;

        }
        while (transform.position.y <= TargetUp2)
        {
            transform.Translate(Vector3.up* SnakeSpeed * Time.deltaTime);
            //Debug.Log("pohybnahoru");
            yield return null;
        }

        SnakeIsMoving = false;
        logic.eventCycle.RemoveAt(0);
        logic.eventCooldowns.RemoveAt(0);
        logic.CurrentPhase++;
        Started = false;
    }
    }


   

