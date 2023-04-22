using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private int _baseValue;
    private readonly List<int> _modifiers = new();

    public int BaseValue { 
        set => _baseValue = value;
        get => _baseValue;
    }

    public int GetModifiedValue()
    {
        int finalValue = _baseValue;
        _modifiers.ForEach(x => finalValue += x);
        return finalValue;
    } 

    public void AddModifier(int modifier)
    {
        if(modifier != 0)
            _modifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
    {
        if(modifier != 0)
            _modifiers.Remove(modifier);
    }
}
