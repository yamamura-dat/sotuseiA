using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public Text LifeText;

    public void SetText(int score)
    {
        this.LifeText.text = "お手つき " + score + "/3";
    }
}
