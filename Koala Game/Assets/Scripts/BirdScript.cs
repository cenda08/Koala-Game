using UnityEngine;
using System.Collections;

public class BirdScript : MonoBehaviour

{
    public GameObject Bird;
    public float BirdSpeed;
    public float TargetRight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator BirdSequence()
    {
        yield return new WaitForSeconds(5);

        StartCoroutine(BirdOn());

    }

    IEnumerator BirdOn()
    {
        // SnakeIsMoving = true; //pozdějc se použije pro game over když se klikne

        while (transform.position.x >= TargetRight)
        {
            transform.Translate(Vector3.right * BirdSpeed * Time.deltaTime);
            Debug.Log("pták doprava");
            yield return null;

        }

        yield return new WaitForSeconds(5);
        StartCoroutine(BirdSings());


        while (transform.position.x >= TargetRight)
        {
            transform.Translate(Vector3.right * BirdSpeed * Time.deltaTime);
            Debug.Log("pták doprava");
            yield return null;

        }
    }


    IEnumerator BirdSings()
        {

        yield return null;
    }

}