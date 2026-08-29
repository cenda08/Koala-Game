  using UnityEngine;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class CarScript : MonoBehaviour

{

    //Scale na konci:X 1,17 Y 2,79
    //Box collider kterej je trigger: jede zeshora a zvětšuje se, když se dotknou Carcollider a koalacollider (trigger)
    //Target x position je 6,89 (Tam furt asi bude ve stejný linii) a y je 2,79

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject Car;
    public float CarSpeed;
    public float TargetDown;
    public float TargetUp;
    public LogicScript logic;
    private bool CanLoadGun = false;
    private bool CanClickCar = false;
    private bool CarStopped = true;
    public bool Started = false;
    public int Random;
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
    }


    // Update is called once per frame
    void Update()
    {
        if (logic.eventCycle.Count > 0 && logic.eventCycle[0] == "Car" && !Started)
        {
            StartCoroutine(CarSequence());
            Started = true;
        }
        if(logic.eventCycle.Count > 1 && !Started && logic.eventCycle[0] != "Snake" && logic.eventCycle[1] == "Phone")
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
        CarStopped = false;
        Car.SetActive(true);
        while (transform.position.y >= TargetDown && CarStopped == false)
        {
            transform.Translate(Vector3.down * CarSpeed * Time.deltaTime);
            transform.Translate(Vector3.right * CarSpeed * Time.deltaTime);
            transform.localScale += Vector3.one * 0.1f * Time.deltaTime;
            // Debug.Log("pohyb auta diagonálně");
            yield return null;

        

            if (Input.GetMouseButtonDown(0))
            {
          //Vytvoření paprsku z pozice myši přes kameru do hry
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.collider.CompareTag("Gun")) 
                    {
                        Gun.SetActive(true);
                        CanLoadGun = true;
                        GunAnimator.SetTrigger("TakeGunOut");
                       
                        Debug.Log("Kliknuto na gun, da se nabit, CanLoadGun:" + CanLoadGun) ;
                    }

                    //První část zrušení auta, musí se kliknout na zbraň co je dole v invu (Gun obrázek) lowkey to moc nechápu trochu sem to zkopčila pak to musím ještě pochopit víc, raycasting
            }

        
        }
    }
        CanLoadGun = false;
        CanClickCar = false;

        while (transform.position.y <= TargetUp && CarStopped == false)
        {
            transform.Translate(Vector3.up * CarSpeed * Time.deltaTime);
            transform.Translate(Vector3.left * CarSpeed * Time.deltaTime);
            transform.localScale -= Vector3.one * 0.1f * Time.deltaTime; 
            yield return null;
        }

        CarIsStealingKoala = true;
        yield return new WaitForSeconds(5f);
        //zahrání animace CarSteals
        CarAnimator.SetBool("StealingKoala", true);
        _KoalaAnimation.SetTrigger("Frightens");
        logic.GameOver();


       
        //ukončení auta když se hitne gun a loadne se a namíří se na auto
    }
    IEnumerator CarExplodes()
    {
if (CarPosition.position.x <= -3.5)
        {
           GunAnimator.SetTrigger("ShootUp");
        }
else
        {
            GunAnimator.SetTrigger("ShootDown");
        }

        CarAnimator.SetTrigger("CarExplodes");


        GunAnimator.SetTrigger("PutGunDown");
        yield return null;
    }


    void OnMouseDown()
    {
        if (CanLoadGun && CanClickCar)
        {
            StartCoroutine(CarExplodes());
            Debug.Log("Auto sejmuto");

            CarStopped = true;
            CanLoadGun = false;
            CanClickCar = false;

            logic.eventCycle.RemoveAt(0);
            logic.eventCooldowns.RemoveAt(0);
            logic.eventCooldowns.RemoveAt(0);
            Random = UnityEngine.Random.Range(0, 1);
            logic.CurrentPhase++;
            logic.TimerText.text = logic.CurrentPhase.ToString() + " / " + logic.TotalEvents;
            Started = false;

        }
    }
}