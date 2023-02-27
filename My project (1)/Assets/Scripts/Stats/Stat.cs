using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValue;

    private List<int> additiveModifiers = new List<int>();
    private List<int> multModifiers = new List<int>();

    public int GetValue()
    {
        int finalValue = baseValue;
        int multValue = 0;
        additiveModifiers.ForEach(x => finalValue += x);
        multModifiers.ForEach(x => multValue += x);
        if(multModifiers.Count == 0)
        {
            return finalValue;
        }
        else
        {
            float returnValue = (float)finalValue * (1 + ((float)multValue / 100));
            finalValue = (int)returnValue;
            return finalValue;
        }
    }

    public void AddAdditiveModifier(int modifier)
    {
        if(modifier != 0)
        {
            additiveModifiers.Add(modifier);
        }
    }

    public void RemoveAdditiveModifier(int modifier)
    {
        if(modifier != 0)
        {
            additiveModifiers.Remove(modifier);
        }
    }

    public void AddMultModifier(int modifier)
    {
        if (modifier != 0)
        {
            multModifiers.Add(modifier);
        }
    }

    public void RemoveMultModifier(int modifier)
    {
        if (modifier != 0)
        {
            multModifiers.Remove(modifier);
        }
    }

}
