using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveScript : MonoBehaviour
{

    public Transform posA;
    public Transform posB;
    
    private bool _moveToB = true;
    [SerializeField]
    private float _speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        if(Vector3.Distance(transform.position, posA.position) <= Mathf.Epsilon)
        {
            // we are at a, move to b
            _moveToB = true;
        }

        if(Vector3.Distance(transform.position, posB.position) <= Mathf.Epsilon)
        {
            // we are at b, move to a
            _moveToB = false;
        }

        if (_moveToB)
        {
            transform.position = Vector3.MoveTowards(transform.position, posB.position, _speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, posA.position, _speed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") other.transform.parent = transform;
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player") other.transform.parent = null;
    }

}
