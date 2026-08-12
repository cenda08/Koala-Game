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
    private bool canClickBird = false;
    private bool BirdStopped = true;
    public float TargetLeft;
    private bool BirdIsSinging = false;
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
    
    // Začátek Bird Života
    IEnumerator BirdSequence()
    {
        yield return new WaitForSeconds(15);

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
        yield return null;
    }
    
    // Správa eventu clicknutí na Birda (nutnost collideru, jinak nefunguje)
    void OnMouseDown()
        {
            if (canClickBird)
            {
                Debug.Log("Left click stisknut!");
                BirdStopped = true;
                canClickBird = false;
                spriteRenderer.color = Color.yellow;
            }
        }

    // Samotný zpěv Birda    
    IEnumerator BirdSings()
    {
        Debug.Log("BirdIsSinging TRUE");
        canClickBird = false;
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
                canClickBird = true;
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
        canClickBird = false;   
        BirdIsSinging = false;
    }
}