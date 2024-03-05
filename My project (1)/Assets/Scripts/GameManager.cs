using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public DifficultyData dif1;
    public DifficultyData dif2;
    public DifficultyData dif3;
    public DifficultyData dif4;
    public DifficultyData dif5;

    public static GameManager instance;
    int area1Difficulty;
    int area2Difficulty;
    int area3Difficulty;
    int area4Difficulty;
    int area5Difficulty;
    int area6Difficulty;

    public int currentArea;

    [SerializeField]
    List<int> areaDifficulties = new List<int>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;            //singleton that manages the equipment that is currently equipped
        }
        DontDestroyOnLoad(instance);
    }

    private void Start()
    {
        area1Difficulty = 1;
        area2Difficulty = 1; 
        area3Difficulty = 1;
        area4Difficulty = 1;
        area4Difficulty = 1;
        area5Difficulty = 1;
        area6Difficulty = 1;

        areaDifficulties.Add(area1Difficulty);
        areaDifficulties.Add(area2Difficulty);
        areaDifficulties.Add(area3Difficulty);
        areaDifficulties.Add(area4Difficulty);
        areaDifficulties.Add(area5Difficulty);
        areaDifficulties.Add(area6Difficulty);

        for (int i = 0; i < 3; i++)
        {
            int random = Random.Range(0, areaDifficulties.Count);
            if (areaDifficulties[random] < 5)
            {
                areaDifficulties[random] = areaDifficulties[random] + 1;
            }
        }

        /*area1Difficulty = areaDifficulties[0];
        area2Difficulty = areaDifficulties[1];
        area3Difficulty = areaDifficulties[2];
        area4Difficulty = areaDifficulties[3];
        area5Difficulty = areaDifficulties[4];
        area6Difficulty = areaDifficulties[5];*/
    }

    public DifficultyData DetermineDifficulty()
    {
        int currentDifficulty = areaDifficulties[currentArea - 1];
        DifficultyData currentDifData = null;

        switch(currentDifficulty)
        {
            case 1:
                currentDifData = dif1;
                break;
            case 2:
                currentDifData = dif2;
                break;
            case 3:
                currentDifData = dif3;
                break;
            case 4:
                currentDifData = dif4;
                break;
            case 5:
                currentDifData = dif5;
                break;
        }
        return currentDifData;
    }
}
