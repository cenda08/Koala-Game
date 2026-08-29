using UnityEngine;

public class CarClick : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public CarScript carScript;

    void OnMouseDown()
    {
        carScript.ShootCar();
    }

   
}
