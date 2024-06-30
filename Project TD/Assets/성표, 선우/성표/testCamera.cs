using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCamera : MonoBehaviour
{
    public GameObject target;
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = target.transform.position;
    }
}
