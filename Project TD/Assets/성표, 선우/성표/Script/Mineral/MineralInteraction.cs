using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class MineralInteraction : MonoBehaviour
{
    [SerializeField] float _interactionDistance = 3f;
    [SerializeField] float _interactionTimer = 5f;

    public Action DestroyEvent;

    GameObject _player;
    float _timer = 0f;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        StartCollectionInteraction();
    }

    bool IsPlayerInInteractionRange()
    {
        float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
        if (distanceToPlayer <= _interactionDistance)
        {
            return true;
        }
        return false;
    }

    void StartCollectionInteraction()
    {
        if (Input.GetKey(KeyCode.F) && IsPlayerInInteractionRange())
        {
            _timer += Time.deltaTime;
            Debug.Log(_timer);
            if(_timer >= _interactionTimer)
            {
                CollectionInterction();
            }
        }
        else
        {
            _timer = 0;
        }
    }

    void CollectionInterction()
    {
        //�κ��丮 �߰�
        Debug.Log("ä������");
        if (DestroyEvent != null)
        {
           DestroyEvent(); //������Ʈ �߰��ʿ�
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _interactionDistance);
    }
}
