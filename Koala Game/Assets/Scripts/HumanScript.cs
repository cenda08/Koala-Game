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
    public LogicScript logic;
    private bool CanLoadGun = false;
    private bool CanClickCar = false;
    private bool CarStopped = true;
    public bool Started = false;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }


    // Update is called once per frame
    void Update()
    {
        if (logic.eventCycle.Count > 0 && logic.eventCycle[0] == "Car" && !Started)
            {
                StartCoroutine(CarSequence());
                Started = true;
            }
        //if (se hitne Car collider s koala colliderem)
        //logic.GameOver();

        //if když se Car pohybuje a udělá se prvně scroll button a pak namíření na Car (1 + 1 OnMouseDown jak v BirdScript) tak se skončí coroutine, ale odečte se 15 sleep score 

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
        while (transform.position.y >= TargetDown && CarStopped == false)
        {
            transform.Translate(Vector3.down * CarSpeed * Time.deltaTime);
            transform.Translate(Vector3.right * CarSpeed * Time.deltaTime);
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
                       
                        CanLoadGun = true;
                        Debug.Log("Kliknuto na gun, da se nabit, CanLoadGun:" + CanLoadGun) ;
                    }

                    //První část zrušení auta, musí se kliknout na zbraň co je dole v invu (Gun obrázek) lowkey to moc nechápu trochu sem to zkopčila pak to musím ještě pochopit víc, raycasting

                    if (Input.GetKeyDown(KeyCode.L))
            {
                        Debug.Log("GUN LOADED:" + CanClickCar); 
                CanClickCar = true;
            }
            //Druhá část, gun se musí nabít tím že se klikne na L jako Load
           
        }

        
    }
        }
    }


    void OnMouseDown()
    {
        if (CanLoadGun && CanClickCar)
        {
            Debug.Log("Auto sejmuto");
            CarStopped = true;
            CanLoadGun = false;
            CanClickCar = false;
            logic.eventCycle.RemoveAt(0);
            logic.eventCooldowns.RemoveAt(0);
            logic.CurrentPhase++;
            logic.TimerText.text = logic.CurrentPhase.ToString() + " / " + logic.TotalEvents;
            Started = false;
        }
    }
    //ukončení auta když se hitne gun a loadne se a namíří se na auto
    }