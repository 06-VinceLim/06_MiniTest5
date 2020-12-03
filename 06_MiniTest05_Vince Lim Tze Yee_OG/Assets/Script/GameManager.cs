using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float levelTime;

    public GameObject TimeText;
    public GameObject addEnergyPrefab;
    public GameObject minusEnergyPrefab;
    public int numberOfSpawn;
    public int TotalCubeSize;
    public int AddCubeSize;
    public int MinusCubeSize;
    public int AddCube;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        //This is used to spawn cubes on random spots
        for (int i = 0; i < numberOfSpawn; i++)
        {
            //Spawn location for X Y Z
            Vector3 randomPos = new Vector3(Random.Range(-15, 15),
                                            0,
                                            Random.Range(-15, 15));
            //Randomly spawning between add or minus energy
            if(Random.Range(0,2) < 1)
            {
                Instantiate(addEnergyPrefab, randomPos, Quaternion.identity);
            }
            else
            {
                Instantiate(minusEnergyPrefab, randomPos, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //This is to count the total number of Cubes respectively
        AddCubeSize = GameObject.FindGameObjectsWithTag("AddEnergy").Length; //Counting Add Cubes
        MinusCubeSize = GameObject.FindGameObjectsWithTag("MinusEnergy").Length; //Counting Minus Cube

        TotalCubeSize = AddCubeSize + MinusCubeSize; //Counting Total Cube

        if (levelTime > 0)
        {
            levelTime -= Time.deltaTime;
            //print("levelTime : " + levelTime);
            //print("levelTime : " + FormatTime(levelTime));
            TimeText.GetComponent<Text>().text = "Time Left : " + levelTime;
        }
        else
        {
            levelTime = 0;
            //print("Times Up!");
            TimeText.GetComponent<Text>().text = "Times up!";
        }

    }
    //Method to add time and spawn cubes
    public void AddTime()
    {

        for (int i = 0; i < AddCube; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-15, 15),
                                            0,
                                            Random.Range(-15, 15));
            if (Random.Range(0, 2) < 1)
            {
                Instantiate(addEnergyPrefab, randomPos, Quaternion.identity);
            }
            else
            {
                Instantiate(minusEnergyPrefab, randomPos, Quaternion.identity);
            }
        }

        levelTime += 5;
    }
    //Formatting the time to count 00(Mins):00(Seconds):00(MiliSec) 
    public string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        int milliseconds = (int)(1000 *(time - minutes * 60 - seconds));
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}

