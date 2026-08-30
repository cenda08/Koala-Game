using UnityEngine;
using System.Collections;

public class Koala_script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  [SerializeField] public Animator _KoalaAnimation;
    public LogicScript logic;
    public BirdScript bird;
    public PhoneScript phone;
    public CarScript car;
    public GameObject Koala;
    public float TargetedCar;
    public float KoalaSpeed;
  

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        bird = GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdScript>();
        phone = GameObject.FindGameObjectWithTag("Phone").GetComponent<PhoneScript>();
        
 
    }
    // Update is called once per frame
    void Update()
    {
        if (logic.SleepScore == 0)
        {
            _KoalaAnimation.SetBool("IsWakingUp", true);
        }
    }

 
public IEnumerator KoalaGetsStolen()
{
        yield return new WaitForSeconds(3.2f);
    while (Koala.transform.position.x < TargetedCar)
    {
        Koala.transform.position = Vector3.MoveTowards(
            Koala.transform.position,
            new Vector3(TargetedCar, Koala.transform.position.y, Koala.transform.position.z),
            KoalaSpeed * Time.deltaTime
        );

        yield return null;
    }

        Koala.SetActive(false);
    }


}