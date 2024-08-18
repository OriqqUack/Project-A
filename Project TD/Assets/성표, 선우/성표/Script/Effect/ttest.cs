using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ttest : MonoBehaviour
{
    public GameObject test;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            test.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            test.SetActive(false);
        }
    }
}
