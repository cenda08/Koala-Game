using UnityEngine;
public class Snake_Script : MonoBehaviour
{

    public GameObject Snake;
    public float SnakeSpeed;
    private float PreviousSnakeSpeed = 5f;
    public float TargetDown;
    public float TargetUp;
    public float TargetLeft;
    public float TargetRight;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SnakeMovement()
    {
        transform.Translate(Vector3.down * SnakeSpeed * Time.deltaTime);

        if (transform.position.y <= TargetDown) SnakeSpeed = 0;

        RestoreSpeed();
        transform.Translate(Vector3.right * SnakeSpeed * Time.deltaTime);
        if (transform.position.x >= TargetRight) SnakeSpeed = 0;
        RestoreSpeed();

    }

    public void RestoreSpeed()
    {
        SnakeSpeed = PreviousSnakeSpeed; 
    }
}
