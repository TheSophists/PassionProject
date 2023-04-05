using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValue;      //base value for a stat to be set in the inspector

    private List<int> additiveModifiers = new List<int>();      //A list that contains all modifiers that will be additive. (5 base damage and a modifier of 2 is 7 damage)
    private List<int> multModifiers = new List<int>();

    public int GetValue()
    {
        int finalValue = baseValue;     //final value starts with the base value.
        int multValue = 0;              //mult modifiers start at 0. The calculation below will add 1 to this value after dividing the mult value by 100.
                                        //(a mult modifier of 20 and base value of 5 should result in 1.2 * 5 = 6 damage total

        additiveModifiers.ForEach(x => finalValue += x);    //add all additive modifiers in the list together for a final additive value
        multModifiers.ForEach(x => multValue += x);         //add all mult modifiers in the list together for a final mult value.


        if(multModifiers.Count == 0)        //if we have no mult modifiers, simply return the final value of the additive modifiers.
        {
            return finalValue;
        }
        else                                //else, return the final value multiplied by the mult values/100 + 1. (the +1 is to ensure that all values can be written as a x/100 + 1, so all values will increase the return value.
        {
            float returnValue = (float)finalValue * (1 + ((float)multValue / 100));

            finalValue = (int)returnValue;  //cast that value to an int (necessary because mult values can produce fractions, and we dont want fractions in our health counters.)
            return finalValue;
        }
    }

    public void AddAdditiveModifier(int modifier)
    {
        if(modifier != 0)                               //adds modifiers to the list
        {
            additiveModifiers.Add(modifier);
        }
    }

    public void RemoveAdditiveModifier(int modifier)    //removes modifiers to the list
    {
        if(modifier != 0)
        {
            additiveModifiers.Remove(modifier);
        }
    }

    public void AddMultModifier(int modifier)           //adds modifiers to the list
    {
        if (modifier != 0)
        {
            multModifiers.Add(modifier);
        }
    }

    public void RemoveMultModifier(int modifier)        //removes modifiers to the list
    {
        if (modifier != 0)
        {
            multModifiers.Remove(modifier);
        }
    }

}
