using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {

    public enum GameColor {
        Red = 0,
        Blue,
        Yellow,
        Green
    };

    public enum ColorType
    {
        Attack = 0,
        Defense
    }

    private GameColor[] color = new GameColor[2];

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void SetColor(GameColor newColor, ColorType type)
    {
        color[(int)type] = newColor;
    }

    public GameColor GetColor(ColorType type) {
        return color[(int)type];
    }

    public int GetHitValue(GameColor hittingColor)
    {
        if (hittingColor == color[(int)ColorType.Defense])
            return 0;
        if ((GameColor)(((int)hittingColor + 1) % 4) == color[(int)ColorType.Defense])
            return 15;
        return 5;
    }

    public Color GetRGB(ColorType type)
    {
        switch (color[(int)type])
        {
            case (GameColor.Red):
                return (Color.red);
            case (GameColor.Blue):
                return (Color.blue);
            case (GameColor.Green):
                return (Color.green);
            case (GameColor.Yellow):
                return (Color.yellow);
            default:
                return (Color.black);
        }
    }
}
