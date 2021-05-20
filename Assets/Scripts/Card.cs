using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    //カードのID
    public int Id;

    // 表示するカードの画像
    public Image CardImage;

    // 透過処理用
    public CanvasGroup CanGroup;

    // 選択されているか判定
    private bool mIsSelected = false;

    // カード情報
    private CardData mData;
    // カードの設定
    public void Set(CardData data)
    {
        // カード情報を設定
        this.mData = data;

        // IDを設定する
        this.Id = data.Id;

        // 表示する画像を設定する
        // 初回は全て裏面表示とする
        this.CardImage.sprite = Resources.Load<Sprite>("Image/samplecard");

        // 選択判定フラグを初期化する
        this.mIsSelected = false;

        // アルファ値を1に設定
        this.CanGroup.alpha = 1;
    }
    ///  <summary>
    /// カードを背面表記にする
    /// </summary>
    public void SetHide()
    {

        // 選択判定フラグを初期化する
        this.mIsSelected = false;

        // カードを背面表示にする
        this.CardImage.sprite = Resources.Load<Sprite>("Image/samplecard");
    }

    /// <summary>
    /// カードを非表示にする
    /// </summary>
    public void SetInvisible()
    {

        // 選択済設定にする
        this.mIsSelected = true;

        // アルファ値を0に設定 (非表示)
        this.CanGroup.alpha = 0;

    }
    /// <summary>
    /// 選択された時の処理
    /// </summary>
    public void OnClick()
    {
        // カードが表面になっていた場合は無効
        if(this.mIsSelected)
        {
            return;
        }

        Debug.Log("OnClick");

        // 選択判定フラグを有効にする
        this.mIsSelected = true;

        // カードを表面にする
        this.CardImage.sprite = this.mData.ImgSprite;

        // 選択したCardIdを保存しよう！
        GameStateController.Instance.SelectedCardIdList.Add(this.mData.Id);
    }
}
/// <summary>
/// カードの情報クラス
/// </summary>
public class CardData
{
    // カードID
    public int Id { get; private set; }

    // 画像
    public Sprite ImgSprite { get; private set; }

    public CardData(int _id, Sprite _sprite)
    {
        this.Id = _id;
        this.ImgSprite = _sprite;
    }
}