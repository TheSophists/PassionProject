using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    /*public static TurnManager turnInstance;
    public bool inCombat = false;
    private List<CharacterStats> currentRoundList;
    private List<CharacterStats> nextRoundList;

    [SerializeField]
    private Dictionary<CharacterStats, int> combatOrderDict;
    CharacterStats currentCharacter;
    public int indexer;

    PlayerManager playerManager;
    PlayerStats playerStats;

    public bool newRound;


    private void Awake()
    {
        turnInstance = this;
    }

    public void Start()
    {
        playerManager = PlayerManager.instance;
        playerStats = playerManager.player.GetComponent<PlayerStats>();
    }

    public void StartCombat()
    {
        playerStats.myTurn = false;
        inCombat = true;
        newRound = true;
        Debug.Log("combat started");
        foreach (CharacterStats stat in currentRoundList)
        {
            stat.myTurn = false;
        }
        StartTurn();
    }

    public void EndCombat()
    {
        inCombat = false;
        indexer = 0;
        ClearLists();
        Debug.Log("combat ended");
    }

    public void SortLists(List<CharacterStats> combatList, Dictionary<CharacterStats, int> combatDictionary)
    {
        combatList.Sort((a, b) => b.initiative.GetValue().CompareTo(a.initiative.GetValue()));

        int i = 0;
        currentRoundList = new List<CharacterStats>(combatList.Count);
        foreach (CharacterStats stat in combatList)
        {
            currentRoundList.Add(combatList[i]);
            i++;
        }
        combatOrderDict = combatDictionary;
        GetNextRound();
    }

    public List<CharacterStats> SortLists(List<CharacterStats> combatList)
    {
        List<CharacterStats> tempOrder = new List<CharacterStats>(combatList.Count);
        combatList.Sort((a, b) => b.initiative.GetValue().CompareTo(a.initiative.GetValue()));
        int i = 0;
        foreach(CharacterStats stat in combatList)
        {
            Debug.Log(i);
            tempOrder[i] = combatList[i];
            i++;
        }
        GetNextRound();
        return tempOrder;
    }

    public void ClearLists()
    {
        combatOrderDict.Clear();
        currentRoundList.Clear();
    }

    public void RemoveFromList(CharacterStats charStats)
    {
        combatOrderDict.Remove(charStats);
        currentRoundList.Remove(charStats);

        if (currentRoundList.Count <= 1)
        {
            EndCombat();
        }
    }

    public void StartTurn()     //picks and starts the next turn
    {
        
        if (indexer < currentRoundList.Count)
        {
            currentCharacter = currentRoundList[indexer];
            indexer++;
        }
        else
        {
            EndRound();
        }

        currentCharacter.myTurn = true;
        Debug.Log(currentCharacter.name + "'s Turn");


        //Debug.Log("Character: " + currentCharacter + "\nInitiative: " + combatOrderDict[currentCharacter]);
    }

    public void EndTurn()
    {
        currentCharacter.myTurn = false;
    }

    public void EndRound()
    {
        Debug.Log("Ends Round");
        indexer = 0;
        int i = 0;
        foreach(CharacterStats stat in nextRoundList)
        {
            currentRoundList[i] = nextRoundList[i];
            i++;
        }
        
        currentCharacter = currentRoundList[indexer];
        indexer++;
        GetNextRound();
    }

    public void UpdateLists()
    {
        combatOrderDict[playerStats] = playerStats.initiative.GetValue();
        
        List<CharacterStats> placeHolderList =  SortLists(nextRoundList);
        int i = 0;
        foreach(CharacterStats stat in placeHolderList)
        {
            nextRoundList[i] = placeHolderList[i];
        }

        Debug.Log("Next Round: " + nextRoundList[0] + "    " + nextRoundList[1] + "    " + nextRoundList[2]);
    }

    public void GetNextRound()
    {
        int i = 0;
        nextRoundList = new List<CharacterStats>(currentRoundList.Count);
        foreach(CharacterStats stat in currentRoundList)
        {
            nextRoundList.Add(currentRoundList[i]);
            i++;
        }

        Debug.Log("This Round: " + currentRoundList[0] + "    " + currentRoundList[1] + "    " + currentRoundList[2]);
    }*/
}
