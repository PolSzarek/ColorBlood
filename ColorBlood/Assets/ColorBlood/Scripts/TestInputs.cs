using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInputs : MonoBehaviour {

    private string lastInput;

	// Use this for initialization
	void Start () {
        Debug.Log("Starting");
        lastInput = "";
	}
	
	// Update is called once per frame
	void Update () {
        float input;
        if ((input = Input.GetAxis("ArrowHorizontal")) != 0 && lastInput != "ArrowHorizontal")
        {
            Debug.Log("ArrowHorizontal : " + input);
            lastInput = "ArrowHorizontal";
        }
        else if ((input = Input.GetAxis("ArrowVertical")) != 0 && lastInput != "ArrowVertical")
        {
            Debug.Log("ArrowVertical : " + input);
            lastInput = "ArrowVertical";
        }
        else if ((input = Input.GetAxis("LeftHorizontal")) != 0 && lastInput != "LeftHorizontal")
        {
            Debug.Log("LeftHorizontal : " + input);
            lastInput = "LeftHorizontal";
        }
        else if ((input = Input.GetAxis("LeftVertical")) != 0 && lastInput != "LeftVertical")
        {
            Debug.Log("LeftVertical : " + input);
            lastInput = "LeftVertical";
        }
        else if ((input = Input.GetAxis("RightHorizontal")) != 0 && lastInput != "RightHorizontal")
        {
            Debug.Log("RightHorizontal : " + input);
            lastInput = "RightHorizontal";
        }
        else if ((input = Input.GetAxis("RightVertical")) != 0 && lastInput != "RightVertical")
        {
            Debug.Log("RightVertical : " + input);
            lastInput = "RightVertical";
        }
        else if ((input = Input.GetAxis("BumperAxis")) > 0.99 || (input = Input.GetAxis("BumperAxis")) < -0.99)
        {
            Debug.Log("BumperAxis : " + input);
            lastInput = "BumperAxis";
        }
        else if ((input = Input.GetAxis("ButtonA")) != 0 && lastInput != "ButtonA")
        {
            Debug.Log("ButtonA : " + input);
            lastInput = "ButtonA";
        }
        else if ((input = Input.GetAxis("ButtonB")) != 0 && lastInput != "ButtonB")
        {
            Debug.Log("ButtonB : " + input);
            lastInput = "ButtonB";
        }
        else if ((input = Input.GetAxis("ButtonX")) != 0 && lastInput != "ButtonX")
        {
            Debug.Log("ButtonX : " + input);
            lastInput = "ButtonX";
        }
        else if ((input = Input.GetAxis("ButtonY")) != 0 && lastInput != "ButtonY")
        {
            Debug.Log("ButtonY : " + input);
            lastInput = "ButtonY";
        }
        else if ((input = Input.GetAxis("Select")) != 0 && lastInput != "Select")
        {
            Debug.Log("Select : " + input);
            lastInput = "Select";
        }
        else if ((input = Input.GetAxis("Start")) != 0 && lastInput != "Start")
        {
            Debug.Log("Start : " + input);
            lastInput = "Start";
        }
        else if ((input = Input.GetAxis("L1")) != 0 && lastInput != "L1")
        {
            Debug.Log("L1 : " + input);
            lastInput = "L1";
        }
        else if ((input = Input.GetAxis("R1")) != 0 && lastInput != "R1")
        {
            Debug.Log("R1 : " + input);
            lastInput = "R1";
        }
        else if ((input = Input.GetAxis("L3")) != 0 && lastInput != "L3")
        {
            Debug.Log("L3 : " + input);
            lastInput = "L3";
        }
        else if ((input = Input.GetAxis("R3")) != 0 && lastInput != "R3")
        {
            Debug.Log("R3 : " + input);
            lastInput = "R3";
        }
    }
}
