using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    float speed = 10.0f;
    bool TimeRunning;

    public GameObject AddCountText;
    public GameObject MinusCountText;
    public GameObject TotalEnergyText;
    public GameObject EnergyCountText;
    public Animator PlayerAni;
    public int energyCount;

    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        //rb.GetComponent<Rigidbody>(); //Caling Rigidbody for collision checks for private
    }

    // Update is called once per frame
    void Update()
    {
        //Movement Forward
        if (Input.GetKey(KeyCode.W) && TimeRunning == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            PlayerAni.SetBool("RunningBool", true);

        }
        //Movement Backwards
        if (Input.GetKey(KeyCode.S) && TimeRunning == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            PlayerAni.SetBool("RunningBool", true);
        }
        //Movement Left
        if (Input.GetKey(KeyCode.A) && TimeRunning == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, -90, 0);
            PlayerAni.SetBool("RunningBool", true);
        }
        //Movement Right
        if (Input.GetKey(KeyCode.D) && TimeRunning == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, 90, 0);
            PlayerAni.SetBool("RunningBool", true);
        }

        if(Input.GetKeyUp(KeyCode.W)|| Input.GetKeyUp(KeyCode.A)|| Input.GetKeyUp(KeyCode.S)|| Input.GetKeyUp(KeyCode.D) && TimeRunning == true)
        {
            PlayerAni.SetBool("RunningBool", false);
        }


        //Printing text onto the screen to display the following.
        EnergyCountText.GetComponent<Text>().text = "Energy Collected : " + energyCount; //Showing Energy Collected

        AddCountText.GetComponent<Text>().text = "Add Energy Left : " + GameManager.instance.AddCubeSize; //Total Add Cube 

        MinusCountText.GetComponent<Text>().text = "Minus Energy Left : " + GameManager.instance.MinusCubeSize;//Total Minus Cube

        TotalEnergyText.GetComponent<Text>().text = "Total Energy Left : " + GameManager.instance.TotalCubeSize;// Total Cube

        if (energyCount <= -1 || GameManager.instance.levelTime <= 0)
        {
            TimeRunning = false;

            SceneManager.LoadScene("LoseScene");
        }
        else if (energyCount >= 50)
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            TimeRunning = true;
        }
    }

    private void OnCollisionEnter(Collision Player)
    {
        //if Player colide with addcube it adds time, energy collected and spawn more cubes.
        if (Player.gameObject.CompareTag("AddEnergy"))
        {
            energyCount += 5;

            GameManager.instance.AddTime();

            Destroy(Player.gameObject);
        }
        if (Player.gameObject.CompareTag("MinusEnergy"))
        {
            energyCount -= 25;
            Destroy(Player.gameObject);
        }

    }
}
