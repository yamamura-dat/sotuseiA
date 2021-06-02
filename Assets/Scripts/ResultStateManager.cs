using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultStateManager : MonoBehaviour
{
    // 時間を表示するテキスト
    public Text TimerText;

    public Text ScoresText;
    /// <summary>
    /// ゲームで経過した時間を表示する
    /// </summary>
    public void SetTimerText(int timer)
    {
        this.TimerText.text = timer.ToString();
    }

    public void SetScoresText(int scores)
    {
        this.ScoresText.text = scores.ToString();
    }
}
