using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //得点表示
    public Text ScoreText;

    //得点のテキストの設定
    public void SetText(int score)
    {
        this.ScoreText.text = "得点 : " + score;
    }
}
