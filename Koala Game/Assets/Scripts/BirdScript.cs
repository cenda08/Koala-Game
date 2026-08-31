using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class BirdScript : MonoBehaviour

{
    public GameObject Bird;
    public float BirdSpeed;
    public float TargetRight;
    public LogicScript logic;
    public SpriteRenderer spriteRenderer;
   // public Sprite BirdSingsSprite;
    //public Sprite CalmBirdSprite;// DO BUDOUCNA NA ZMĚNU SPRITU A ANIMACE
    private bool CanClickBird = false;
    private bool BirdStopped = true;
    public float TargetLeft;
    public float BirdPause;
    public bool BirdIsSinging = false;
    public bool Started = false;
    public int Random;
    [SerializeField] public Animator BirdAnimator;
    [SerializeField] public Animator StoneAnimator;
    [SerializeField] public Animator _KoalaAnimation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //spriteRenderer.color = Color.yellow;
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        //Random = UnityEngine.Random.Range(0,1);
        Random = 1;
    }

    // Update is called once per frame
    void Update()
    {
    if (logic.eventCycle.Count > 0 && logic.eventCycle[0] == "Bird" && !Started)
        {
            StartCoroutine(BirdSequence());
            Started = true;
        }
    if(logic.eventCycle.Count > 1 && !Started && logic.eventCycle[0] != "Snake" && logic.eventCycle[1] == "Bird")
        {
            if(Random == 1)
            {
                Debug.Log("Double event! Bird");
                StartCoroutine(BirdSequence());
                Started = true;
            }
        }  

    }
    
    // Začátek Bird Života
    IEnumerator BirdSequence()
    {
        Debug.Log("Starting BirdScript, cooldown:" + logic.eventCooldowns[0]);
        yield return new WaitForSeconds(logic.eventCooldowns[0]);
        StartCoroutine(BirdOn());
    }

    // Let Birda
    IEnumerator BirdOn()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        BirdAnimator.SetBool("IsLeaving", false);
        BirdAnimator.SetBool("IsLeaving", false);
        while (transform.position.x <= TargetRight)
        {
            transform.Translate(Vector3.right * BirdSpeed * Time.deltaTime);
            Debug.Log("pták doprava");
            yield return null;
        }
        BirdAnimator.SetBool("IsComing", false);
        yield return new WaitForSeconds(5);
        StartCoroutine(BirdSingsSequence());
    }
    
    // Začátek/Konec zpěvu Birda
    IEnumerator BirdSingsSequence()
    {
        BirdAnimator.SetBool("IsLeaving", false);
        BirdAnimator.SetBool("IsComing", false);
        BirdIsSinging = true;
        for (int i = 0; i<3; i++)
        {
            yield return StartCoroutine(BirdSings());
            float RandomPauseTime = UnityEngine.Random.Range(5f, 10f);
            yield return new WaitForSeconds(RandomPauseTime);
        }

        BirdAnimator.SetBool("IsLeaving", true);
        transform.localScale = new Vector3(-1f, 1f, 1f);
        while (transform.position.x >= TargetLeft)
        {
      
            transform.Translate(Vector3.left * BirdSpeed * Time.deltaTime);
            Debug.Log("pták odjíždí");
            yield return null;
        }
        Started = false;
        logic.eventCycle.RemoveAt(0);
        logic.eventCooldowns.RemoveAt(0);
        Random = UnityEngine.Random.Range(0,1);
        logic.CurrentPhase++;
        logic.TimerText.text = logic.CurrentPhase.ToString() + " / " + logic.TotalEvents;
        yield return null;
    }
    
    // Správa eventu clicknutí na Birda (nutnost collideru, jinak nefunguje)
    void OnMouseDown()
        {
            if (CanClickBird)
            {
            StoneAnimator.SetTrigger("RockThrown");
            BirdAnimator.SetTrigger("Hit");
            Debug.Log("Left click stisknut! Bird hitnut");
                BirdStopped = true;
                CanClickBird = false;
                //spriteRenderer.color = Color.yellow;
            }
        }

    // Samotný zpěv Birda    
    IEnumerator BirdSings()
    {
        BirdAnimator.SetTrigger("Sing");
        _KoalaAnimation.SetTrigger("Frightens");
        Debug.Log("BirdIsSinging TRUE");
        CanClickBird = false;
        BirdIsSinging = true;
        BirdStopped = false;
        //spriteRenderer.color = Color.red;
        Debug.Log("BIRD SINGS START");
        float timer = 0;
        

        // 3s timer + kontrola kliknutí space
        while (timer < 3 && BirdIsSinging)
        {
            timer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("SPACE STISKNUT");
                Debug.Log("BirdIsSinging: " + BirdIsSinging);
                CanClickBird = true;
            }
            yield return null;

        }
        // Logic systém ztráty bodů
        if (!BirdStopped)
        {
            logic.ScoreDecrease(5);
        }
        //spriteRenderer.color = Color.yellow;
        Debug.Log("KONEC BIRD SINGS");
        CanClickBird = false;   
        BirdIsSinging = false;
    }
}