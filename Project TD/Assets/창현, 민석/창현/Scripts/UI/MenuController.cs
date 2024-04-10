using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    private Vector3 offset = new Vector3(1.7f, 2.7f, 6.6f);
    private RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (player != null)
        {
            rect.position = player.position + offset;

            transform.LookAt(player);
        }
    }
}
