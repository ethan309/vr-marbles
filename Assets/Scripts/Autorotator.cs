using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autorotator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Rotate
        Vector3 incrementalRotation = new Vector3(0, 90, 0);
        transform.Rotate(incrementalRotation * Time.deltaTime, Space.World);
    }
}
