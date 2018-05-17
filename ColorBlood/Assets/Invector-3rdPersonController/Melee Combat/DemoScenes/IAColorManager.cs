using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vItemManager;

public class IAColorManager : MonoBehaviour {

    public Light lt;
    public float TimeBeforeChange;
    private ColorMove colormanager;
    private bool changeColor;
    private System.Random random;

    // Use this for initialization
    void Start () {
        lt = GetComponent<Light>();
        colormanager = GetComponent<ColorMove>();
        lt.color = (colormanager.colorAttack == MoveColorEffect.BLUE) ? Color.blue : (colormanager.colorAttack == MoveColorEffect.GREEN) ? Color.green : (colormanager.colorAttack == MoveColorEffect.RED) ? Color.red : Color.yellow;
        changeColor = false;
        random = new System.Random();
        Random.InitState(unchecked((int)(System.DateTime.Now.Ticks % int.MaxValue)));
        StartCoroutine(waiter());
    }
	
	// Update is called once per frame
	void Update () {
        if (changeColor)
        {
            changeColor = false;
            StartCoroutine(waiter());
        }
    }

    public void OnDeath()
    {
        lt.enabled = false;
        this.tag = "Untagged";
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(TimeBeforeChange);
        System.Array values = System.Enum.GetValues(typeof(MoveColorEffect));
        MoveColorEffect randomColor = (MoveColorEffect)values.GetValue(random.Next(values.Length));
        colormanager.colorAttack = randomColor;
        colormanager.colorDefense = randomColor;
        lt.color = (colormanager.colorAttack == MoveColorEffect.BLUE) ? Color.blue : (colormanager.colorAttack == MoveColorEffect.GREEN) ? Color.green : (colormanager.colorAttack == MoveColorEffect.RED) ? Color.red : Color.yellow;
        changeColor = true;
    }
}
