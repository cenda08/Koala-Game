  using UnityEngine;
using System.Collections;

public class CarScript : MonoBehaviour

{

    //Scale na konci:X 1,17 Y 2,79
    //Box collider kterej je trigger: jede zeshora a zvětšuje se, když se dotknou Carcollider a koalacollider (trigger)
    //Target x position je 6,89 (Tam furt asi bude ve stejný linii) a y je 2,79

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject Car;
    public float CarSpeed;
    public float CarSpeed2;
    public float TargetDown;
    public float TargetUp;
    private Vector3 StartPosition = new Vector3(6.56f, -0.85f, -0.95f);
    private Vector3 StartScale = new Vector3(0.4f, 0.4f, 0.2f);
    public LogicScript logic;
    private bool CanLoadGun = false;
    private bool CanClickCar = false;
    private bool CarStopped = true;
    public bool Started = false;
    public int Random;
    public bool CarIsGoingDown = false;
    public bool CarIsStealingKoala = false;
    public Transform CarPosition;
    [SerializeField] public Animator _KoalaAnimation;
    [SerializeField] public Animator CarAnimator;
    [SerializeField] public Animator GunAnimator;
    public GameObject Gun;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        Random = UnityEngine.Random.Range(0,2);
        Car.transform.position = StartPosition;
        Car.transform.localScale = StartScale;
    }


    // Update is called once per frame
    void Update()
    {
        if (logic.eventCycle.Count > 0 && logic.eventCycle[0] == "Car" && !Started)
        {
            StartCoroutine(CarSequence());
            Started = true;
        }


        if(logic.eventCycle.Count > 1 && !Started 
            && logic.eventCycle[0] != "Snake"
            && logic.eventCycle[1] == "Phone")
        {
            if(Random == 1)
                {
                    StartCoroutine(CarSequence());
                    Started = true;
                }
        }

    
        if (Input.GetKeyDown(KeyCode.R) && CanLoadGun)
        {
            Debug.Log("GUN LOADED:" + CanClickCar);
            GunAnimator.SetTrigger("Reload");
            CanClickCar = true;
        }
    }

    IEnumerator CarSequence()
    {
        Debug.Log("Starting CarScript, cooldown:" + logic.eventCooldowns[0]);
        yield return new WaitForSeconds(logic.eventCooldowns[0]);
        StartCoroutine(CarMovement());
    }

    

    IEnumerator CarMovement()
    {
        Car.transform.position = StartPosition ;
        Car.transform.localScale = StartScale;
        CarIsGoingDown = true;
        CarStopped = false;
        Car.SetActive(true);
        while (Car.transform.position.y >= TargetDown && CarStopped == false)
        {
            Car.transform.Translate(Vector3.down * CarSpeed * Time.deltaTime);
            Car.transform.Translate(Vector3.right * CarSpeed * Time.deltaTime);
            Car.transform.localScale += Vector3.one * 0.1f * Time.deltaTime;
                // Debug.Log("pohyb auta diagonálně");
                yield return null;
        }
   
        //auto se sestřelí => skončí sekvence
        if (CarStopped)
        {
            yield break;
        }

        //po tom co dojede dolů už nejde sestřelit
        CanLoadGun = false;
        CanClickCar = false;
        CarIsGoingDown = false;

        //jede back nahoru
        Debug.Log ("Auto jede zpatky nahoru");
        while (Car.transform.position.y <= TargetUp && CarStopped == false)
        {
            Car.transform.Translate(Vector3.up * CarSpeed2 * Time.deltaTime);
            Car.transform.Translate(Vector3.left * CarSpeed2 * Time.deltaTime);
            Car.transform.localScale -= Vector3.one * 0.1f * Time.deltaTime; 
            yield return null;
        }


        //dojede doprostřed
        CarIsStealingKoala = true;
        //zahrání animace CarSteals
        CarAnimator.SetBool("StealingKoala", true);
        _KoalaAnimation.SetTrigger("Frightens");

        yield return new WaitForSeconds(5f);
        logic.GameOver();


       
        //ukončení auta když se hitne gun a loadne se a namíří se na auto
    }
    IEnumerator CarExplodes()
    {
if (CarPosition.position.y <= -3.5)
        {
           GunAnimator.SetTrigger("ShootUp");
        }
else
        {
            GunAnimator.SetTrigger("ShootDown");
        }

        CarAnimator.SetTrigger("CarExplodes");



        CarStopped = true;
        yield return new WaitForSeconds(1f);
        GunAnimator.SetTrigger("PutGunDown");
   
        Car.SetActive(false);
        yield return new WaitForSeconds(2f);
        Gun.SetActive(false);
        Started = false;
    }
    





    public void TakeGun()
    {
        if (CarIsGoingDown == false)
            return;

        Gun.SetActive(true);
        CanLoadGun = true;
        GunAnimator.SetTrigger("TakeGunOut");

        Debug.Log("GUN TAKEN OUT, dá se nabít");
    }

    public void ShootCar()
    {
    
            if (CanLoadGun && CanClickCar)
            {
            CarStopped = true;
            CarIsGoingDown = false;
            StartCoroutine(CarExplodes());
           
                Debug.Log("Auto sejmuto");

                CanLoadGun = false;
                CanClickCar = false;

                logic.eventCycle.RemoveAt(0);
                logic.eventCooldowns.RemoveAt(0);
                // logic.eventCooldowns.RemoveAt(0);
                Random = UnityEngine.Random.Range(0, 2);
                logic.CurrentPhase++;
                logic.TimerText.text = logic.CurrentPhase.ToString() + " / " + logic.TotalEvents;
              

            }
        }
  
}