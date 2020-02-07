using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacer : MonoBehaviour
{
    private float speed = 5.0F;
    private Vector3 center = new Vector3(0F, 0F, 0F);

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(center, Vector3.up, 30 * Time.deltaTime);
    }
}
