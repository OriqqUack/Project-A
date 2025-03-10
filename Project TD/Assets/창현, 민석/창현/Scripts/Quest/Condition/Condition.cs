using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : ScriptableObject
{
    [SerializeField]
    private string description;

    public string Description => description;

    public abstract bool IsPass(Quest quest);
}
