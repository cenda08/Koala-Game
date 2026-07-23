using UnityEngine;
using System.Collections;

public class BirdScript : MonoBehaviour

{
    public GameObject Bird;
    public float BirdSpeed;
    public float TargetRight;
    public LogicScript logic;
    public SpriteRenderer spriteRenderer;
   // public Sprite BirdSingsSprite;
    //public Sprite CalmBirdSprite;// DO BUDOUCNA NA ZMĚNU SPRITU A ANIMACE
    private bool canClickBird = false;
    private bool BirdStopped = true;
    public float TargetLeft;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer.color = Color.yellow;
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        StartCoroutine(BirdSequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator BirdSequence()
    {
        yield return new WaitForSeconds(15);

        StartCoroutine(BirdOn());
    }

    IEnumerator BirdOn()
    {
     

        while (transform.position.x <= TargetRight)
        {
            transform.Translate(Vector3.right * BirdSpeed * Time.deltaTime);
            Debug.Log("pták doprava");
            yield return null;

        }

        yield return new WaitForSeconds(5);
        StartCoroutine(BirdSingsSequence());
    }
    

IEnumerator BirdSingsSequence()
        {
        for (int i = 0; i<3; i++)
        {
            yield return StartCoroutine(BirdSings());
            float RandomPauseTime = Random.Range(5f, 10f);
            yield return new WaitForSeconds(RandomPauseTime);
        }

        while (transform.position.x >= TargetLeft)
        {
            transform.Translate(Vector3.left * BirdSpeed * Time.deltaTime);
            Debug.Log("pták odjizdi");
            yield return null;

        }
        yield return null;
    }
    
    IEnumerator BirdSings()
    {
        canClickBird = false;
        spriteRenderer.color = Color.red;
        Debug.Log("BIRD SINGS START");
        float timer = 0;



        yield return new WaitForSeconds(3);
    

        

        while (timer < 3)
        {
            BirdStopped = false;
            timer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("ENTER");
                canClickBird = true;

            }
           if (canClickBird == true && Input.GetMouseButtonDown(0))
            {
              
                BirdStopped = true;
                spriteRenderer.color = Color.yellow;
            }

            yield return null;
            spriteRenderer.color = Color.yellow;
            Debug.Log("KONEC BIRD SINGS");
           
        }

        if (!BirdStopped)
        {
            logic.ScoreDecrease(5);
        }
    }

    }