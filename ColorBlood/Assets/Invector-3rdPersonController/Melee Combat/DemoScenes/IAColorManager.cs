using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAColorManager : MonoBehaviour {

    public Light lt;
    public enum MoveColorEffect
    {
        RED = 0,
        BLUE = 1,
        GREEN = 2,
        YELLOW = 3
    }
    public MoveColorEffect colorAttack;

    // Use this for initialization
    void Start () {
        lt = GetComponent<Light>();
        colorAttack = MoveColorEffect.RED;
    }
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(waiter());
        lt.color = (colorAttack == MoveColorEffect.BLUE) ? Color.blue : (colorAttack == MoveColorEffect.GREEN) ? Color.green : (colorAttack == MoveColorEffect.RED) ? Color.red : Color.yellow;
    }

    IEnumerator waiter()
    {
        System.Array values = System.Enum.GetValues(typeof(MoveColorEffect));
        System.Random random = new System.Random();
        MoveColorEffect randomColor = (MoveColorEffect)values.GetValue(random.Next(values.Length));

        int wait_time = UnityEngine.Random.Range(0, 1000);
        yield return new WaitForSeconds(wait_time);
        colorAttack = randomColor;
    }
}
