using UnityEngine;

public class Koala_script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  [SerializeField] public Animator _KoalaAnimation;
    public LogicScript logic;
    public BirdScript bird;
    public PhoneScript phone;
    public CarScript car;
    public GameObject Koala;


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
}
