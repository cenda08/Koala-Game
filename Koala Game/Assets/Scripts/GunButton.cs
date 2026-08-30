using UnityEngine;

public class GunButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public CarScript carScript;

    public void TakeGun()
    {
        carScript.TakeGun();
    }

}