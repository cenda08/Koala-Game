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
    private bool BirdIsSinging = false;
    public bool Started = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer.color = Color.yellow;
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
    if (logic.eventCycle.Count > 0 && logic.eventCycle[0] == "Bird" && !Started)
        {
            StartCoroutine(BirdSequence());
            Started = true;
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
        while (transform.position.x <= TargetRight)
        {
            transform.Translate(Vector3.right * BirdSpeed * Time.deltaTime);
            Debug.Log("pták doprava");
            yield return null;
        }

        yield return new WaitForSeconds(5);
        StartCoroutine(BirdSingsSequence());
    }
    
    // Začátek/Konec zpěvu Birda
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
            Debug.Log("pták odjíždí");
            yield return null;
        }
        Started = false;
        logic.eventCycle.RemoveAt(0);
        logic.eventCooldowns.RemoveAt(0);
        logic.CurrentPhase++;
        logic.TimerText.text = logic.CurrentPhase.ToString() + " / " + logic.TotalEvents;
        yield return null;
    }
    
    // Správa eventu clicknutí na Birda (nutnost collideru, jinak nefunguje)
    void OnMouseDown()
        {
            if (CanClickBird)
            {
                Debug.Log("Left click stisknut!");
                BirdStopped = true;
                CanClickBird = false;
                spriteRenderer.color = Color.yellow;
            }
        }

    // Samotný zpěv Birda    
    IEnumerator BirdSings()
    {
        Debug.Log("BirdIsSinging TRUE");
        CanClickBird = false;
        BirdIsSinging = true;
        BirdStopped = false;
        spriteRenderer.color = Color.red;
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
        spriteRenderer.color = Color.yellow;
        Debug.Log("KONEC BIRD SINGS");
        CanClickBird = false;   
        BirdIsSinging = false;
    }
}