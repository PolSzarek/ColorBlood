using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummyAI : MonoBehaviour {

    public Slider HealthBar;
    public float ColorSwitchTime;

    private float countdown;
    private ColorManager colorManager;
    private Renderer newRenderer;
    private int health;

	// Use this for initialization
	void Start () {
        health = 100;
        countdown = ColorSwitchTime;
        Random.InitState((int)System.DateTime.Now.Ticks);
        colorManager = this.GetComponent<ColorManager>();
        newRenderer = this.GetComponent<Renderer>();
        colorManager.SetColor((ColorManager.GameColor)Random.Range(0, 4), ColorManager.ColorType.Defense);
        newRenderer.material.color = colorManager.GetRGB(ColorManager.ColorType.Defense);
    }
	
	// Update is called once per frame
	void Update () {
        countdown -= Time.deltaTime;
        if (countdown <= 0) {
            colorManager.SetColor((ColorManager.GameColor)(((int)(colorManager.GetColor(ColorManager.ColorType.Defense)) + Random.Range(1, 4)) % 4), ColorManager.ColorType.Defense);
            newRenderer.material.color = colorManager.GetRGB(ColorManager.ColorType.Defense);
            countdown = ColorSwitchTime;
        }
	}

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("HIT");
        if (collider.gameObject.CompareTag("Player Weapon"))
        {
            health -= colorManager.GetHitValue(collider.gameObject.GetComponent<ColorManager>().GetColor(ColorManager.ColorType.Attack));
            HealthBar.value = health;
        }
    }
}
