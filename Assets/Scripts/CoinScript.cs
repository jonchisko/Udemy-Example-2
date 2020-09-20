using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField]
    private int _amount = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerScript ps = other.gameObject.GetComponent<PlayerScript>();
            ps.IncreaseCoinAmount(_amount);
            Destroy(this.gameObject);
        }
    }
}
