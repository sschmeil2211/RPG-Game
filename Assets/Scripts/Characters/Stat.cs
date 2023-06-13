using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private int _baseValue;
    private readonly List<int> _modifiers = new(); 

    public int GetBaseValue()
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
