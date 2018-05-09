using System.Collections;
using System.Collections.Generic;
using Invector.vCharacterController;
using Invector.vItemManager;
using UnityEngine;


public class PlayerInput : MonoBehaviour {	
	// Update is called once per frame
	void Update () {
        vMeleeCombatInput combatInput = GetComponent<vMeleeCombatInput>();
        ColorMove colorScript = GetComponent<ColorMove>();

        if (combatInput && combatInput.isBlocking)
        {
            if (Input.GetAxis("LT") > 0.8)
                colorScript.colorDefense = MoveColorEffect.BLUE;
            else if (Input.GetAxis("LB") > 0.8)
                colorScript.colorDefense = MoveColorEffect.RED;
            else if (Input.GetAxis("RB") > 0.8)
                colorScript.colorDefense = MoveColorEffect.YELLOW;
            else if (Input.GetAxis("RT") > 0.8)
                colorScript.colorDefense = MoveColorEffect.GREEN;
        }
        else
        {
            if (Input.GetAxis("LT") > 0.8)
                colorScript.colorAttack = MoveColorEffect.BLUE;
            else if (Input.GetAxis("LB") > 0.8)
                colorScript.colorAttack = MoveColorEffect.RED;
            else if (Input.GetAxis("RB") > 0.8)
                colorScript.colorAttack = MoveColorEffect.YELLOW;
            else if (Input.GetAxis("RT") > 0.8)
                colorScript.colorAttack = MoveColorEffect.GREEN;
        }
    }
}
