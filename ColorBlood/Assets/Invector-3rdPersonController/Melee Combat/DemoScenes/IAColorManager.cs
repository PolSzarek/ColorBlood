using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAColorManager : MonoBehaviour {

    public Light lt;
    public Invector.vItemManager.ColorMove colormanager;

    // Use this for initialization
    void Start () {
        lt = GetComponent<Light>();
        colormanager = GetComponent<Invector.vItemManager.ColorMove>();
    }
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(waiter());
        lt.color = (colormanager.colorAttack == Invector.vItemManager.MoveColorEffect.BLUE) ? Color.blue : (colormanager.colorAttack == Invector.vItemManager.MoveColorEffect.GREEN) ? Color.green : (colormanager.colorAttack == Invector.vItemManager.MoveColorEffect.RED) ? Color.red : Color.yellow;
    }

    IEnumerator waiter()
    {
        System.Array values = System.Enum.GetValues(typeof(Invector.vItemManager.MoveColorEffect));
        System.Random random = new System.Random();
        Invector.vItemManager.MoveColorEffect randomColor = (Invector.vItemManager.MoveColorEffect)values.GetValue(random.Next(values.Length));

        int wait_time = UnityEngine.Random.Range(999, 1000);
        yield return new WaitForSeconds(wait_time);
        colormanager.colorAttack = randomColor;
        colormanager.colorDefense = randomColor;
    }
}
