using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandingMarbleController : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject centerpoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        centerpoint = GameObject.FindGameObjectsWithTag("Centerpoint")[0];
    }

    private void FixedUpdate()
    {
        float separation = Vector3.Distance(centerpoint.transform.position, transform.position);
        if(separation > 6.1)
        {
            transform.gameObject.SetActive(false);
        }
    }
}
