using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject Weapon;
    public float Speed;

    private Vector3 moveVelocity;

    // Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        moveVelocity = new Vector3(Input.GetAxisRaw("LeftHorizontal"), 0, Input.GetAxisRaw("LeftVertical")) * Speed;

        Turn();
        Attack();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Turn()
    {
        Transform selectedTarget = GetComponent<TargetEnemy>().selectedTarget;

        if (!selectedTarget)
        {
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("RightHorizontal") - Vector3.forward * Input.GetAxisRaw("RightVertical");
            if (playerDirection.sqrMagnitude > 0.01f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }
        }
        else
        {
            transform.LookAt(selectedTarget);
        }
    }

    private void Move()
    {
        // Move the player to it's current position plus the movement.
        GetComponent<Rigidbody>().velocity = moveVelocity;
    }

    private bool GetInputColor(ref ColorManager.GameColor result)
    {
        if (Input.GetAxis("L1") != 0)
        {
            result = ColorManager.GameColor.Blue;
            return true;
        }
        if (Input.GetAxis("R1") != 0)
        {
            result = ColorManager.GameColor.Red;
            return true;
        }
        if (Input.GetAxis("BumperAxis") > 0.8)
        {
            result = ColorManager.GameColor.Yellow;
            return true;
        }
        if (Input.GetAxis("BumperAxis") < -0.8)
        {
            result = ColorManager.GameColor.Green;
            return true;
        }
        return false;
    }

    private void Attack()
    {
        ColorManager.GameColor inputColor = ColorManager.GameColor.Blue;
        bool hasInput = GetInputColor(ref inputColor);
        if (hasInput)
        {
            if (Input.GetAxis("L3") != 0)
            {
                ColorManager colorManager = GetComponent<ColorManager>();
                colorManager.SetColor(inputColor, ColorManager.ColorType.Defense);
                GetComponent<Renderer>().material.color = colorManager.GetRGB(ColorManager.ColorType.Defense);
            } else
            {
                ColorManager weaponColor = Weapon.GetComponent<ColorManager>();
                weaponColor.SetColor(inputColor, ColorManager.ColorType.Attack);
                Weapon.GetComponent<Renderer>().material.color = weaponColor.GetRGB(ColorManager.ColorType.Attack);
                GetComponent<Animator>().SetTrigger("Hit");
            }
        }
    }
}
