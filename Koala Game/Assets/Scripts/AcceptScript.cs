using UnityEngine;

public class AcceptScript : MonoBehaviour
{

    public GameObject Accept;
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
            phone.AcceptCall();
        }
    }
}
