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
    public float TargetLeft2;
    public float TargetRight;
    public float TargetRight2;
    public LogicScript logic;
    public GameObject koala;
    private bool SnakeIsMoving = false;
    public bool Started = false;
    public Transform SnakeTarget;
    private Coroutine snakeMovementCoroutine;
    [SerializeField] public Animator SnakeAnimator;
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
            StartCoroutine(SnakeEaten());
        }
    }

    IEnumerator SnakeSequence()
    {
        Debug.Log("Starting SnakeScript, cooldown:" + logic.eventCooldowns[0]);
        yield return new WaitForSeconds(logic.eventCooldowns[0]);

        snakeMovementCoroutine = StartCoroutine(SnakeMovement());

    }


        IEnumerator SnakeMovement()
    {
        SnakeIsMoving = true; //pozdějc se použije pro game over když se klikne

            //

            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
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
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
            
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
            while (transform.position.x >= TargetLeft2)
            {
                transform.Translate(Vector3.left * SnakeSpeed * Time.deltaTime);
                //Debug.Log("pohyb doleva");
                yield return null;

            }

            SnakeIsMoving = false;
        logic.eventCycle.RemoveAt(0);
        logic.eventCooldowns.RemoveAt(0);
        logic.CurrentPhase++;
        Started = false;
    }
    

    IEnumerator SnakeEaten()
    {
        StopCoroutine(snakeMovementCoroutine);
        SnakeIsMoving = false;
        while (Vector3.Distance(transform.position, SnakeTarget.position) > 0.01f)
        {
        //had přijede ke koale
            transform.position = Vector3.MoveTowards(
                transform.position,
                SnakeTarget.position,
                SnakeSpeed * Time.deltaTime
            );
            yield return null;
        }
        //pro jistotu umisteni na target
        transform.position = SnakeTarget.position;
            //spustí animaci sežrání
            SnakeAnimator.SetBool("SnakeIsEating", true);
        //čeka na sezrani
            yield return new WaitForSeconds(1.5f);
        koala.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        //game over
        logic.GameOver();
        //blbosti s randomizerem
            logic.eventCycle.RemoveAt(0);
            logic.eventCooldowns.RemoveAt(0);
            logic.TimerText.text = logic.CurrentPhase.ToString() + " / " + logic.TotalEvents;
            Started = false;
          
    
    
}

}


