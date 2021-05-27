using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartStateManager : MonoBehaviour
{
    // ゲームの開始テキストの座標
    public RectTransform GameStartTextRt;

    /// <summary>
    /// テキストの拡大アニメーション
    /// </summary>
    public void EnlarAnimation()
    {

        this.GameStartTextRt.DOScale(Vector3.one * 1.2f, 2f)
            .OnComplete(() => {
                // テキストの縮小アニメーション
                this.mShrinkAnimation();
            });
    }

    /// <summary>
    /// テキストの縮小アニメーション
    /// </summary>
    private void mShrinkAnimation()
    {

        this.GameStartTextRt.DOScale(Vector3.one * 0.8f, 2f)
            .OnComplete(() => {
                // テキストの縮小アニメーション
                this.EnlarAnimation();
            });
    }
}
