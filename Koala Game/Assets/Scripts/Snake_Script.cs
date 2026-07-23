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
    //private bool SnakeIsMoving = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SnakeSequence());
    }

    // Update is called once per frame
    void Update()
    {
        //if (SnakeIsMoving == true) GameOver();
    }

    IEnumerator SnakeSequence()
    {
        yield return new WaitForSeconds(5);

        StartCoroutine(SnakeMovement());

    }


        IEnumerator SnakeMovement()
    {
       // SnakeIsMoving = true; //pozdějc se použije pro game over když se klikne

       while (transform.position.y >= TargetDown)
        {
            transform.Translate(Vector3.down * SnakeSpeed * Time.deltaTime);
            Debug.Log ("pohyb dolů");
            yield return null;

        }


        while (transform.position.x <= TargetRight)
        {
            transform.Translate(Vector3.right * SnakeSpeed * Time.deltaTime);
            Debug.Log("pohyb doprava");
            yield return null;
        }

        while (transform.position.y <= TargetUp)
        {
            transform.Translate(Vector3.up * SnakeSpeed * Time.deltaTime);
            Debug.Log("pohybnahoru");
            yield return null;
        }


        while (transform.position.x <= TargetRight2)
        {
            transform.Translate(Vector3.right * SnakeSpeed * Time.deltaTime * 1/2);
            Debug.Log("pohyb doprava");
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
            Debug.Log("pohyb doleva");
            yield return null;

        }
        while (transform.position.y <= TargetUp2)
        {
            transform.Translate(Vector3.up* SnakeSpeed * Time.deltaTime);
            Debug.Log("pohybnahoru");
            yield return null;
        }
    }
    }


   

