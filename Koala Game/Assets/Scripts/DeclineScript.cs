using UnityEngine;

public class DeclineScript : MonoBehaviour
{
    public GameObject Decline;
    public PhoneScript phone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (phone.CanClickHint == false)
        {
            phone.DeclineCall();
        }
    }
}
