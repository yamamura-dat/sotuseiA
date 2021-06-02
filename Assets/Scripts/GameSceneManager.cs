using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameSceneManager : MonoBehaviour
{
    // 一致したカードリストID
    private List<int> mContainCardIdList = new List<int>();

    // カード生成マネージャクラス
    public CardCreateManager CardCreate;

    // 時間管理クラス
    public TimerManager timerManager;

    //得点管理クラス
    public ScoreManager scoreManager;

    // スタートステートクラス
    public StartStateManager startStateManager;

    //リザルトステートクラス
    public ResultStateManager resultStateManager;

    public GameObject panel;

    // ゲームステート管理
    private EGameState mEGameState;

    // 経過時間
    private float mElapsedTime;

    //得点
    private float mScore;

    private void mSetStartState()
    {
        // テキストの拡大縮小アニメーション
        this.startStateManager.EnlarAnimation();
    }

    /// <summary>
    /// Readyステートに遷移する
    /// </summary>
    public void OnGameStart()
    {
        // ゲームステートを初期化
        this.mEGameState = EGameState.READY;

        this.panel.SetActive(true);

        // スタートエリアを表示
        this.startStateManager.gameObject.SetActive(false);

        // ゲームのステート管理
        this.mSetGameState();
    }

    void Start()
    {

        // ゲームステートを初期化
        this.mEGameState = EGameState.START;

        // ResultAreaを非表示にする
        this.resultStateManager.gameObject.SetActive(false);

        this.scoreManager.gameObject.SetActive(false);
        this.timerManager.gameObject.SetActive(false);

        this.panel.SetActive(false);

        // ゲームのステート管理
        this.mSetStartState();
    }

    /// <summary>
    /// ゲームステートで処理を変更する
    /// </summary>
    private void mSetGameState()
    {

        switch (this.mEGameState)
        {
            // スタート画面
            case EGameState.START:
                // スタートエリアを表示
                this.startStateManager.gameObject.SetActive(true);
                
                // ゲームスタートの開始
                this.mSetStartState();
                break;
            // ゲーム準備期間
            case EGameState.READY:
                // ゲームの準備ステートを開始する
                this.mSetGameReady();
                break;
            // ゲーム中
            case EGameState.GAME:
                break;
            // 結果画面
            case EGameState.RESULT:
                this.resultStateManager.gameObject.SetActive(true);
                this.mSetResultState();
                break;
        }
    }

    /// <summary>
    /// リザルトステートの設定処理
    /// </summary>
    private void mSetResultState()
    {

        this.resultStateManager.SetTimerText((int)this.mElapsedTime);

        this.resultStateManager.SetScoresText((int)this.mScore);
    }

    /// <summary>
    /// スタート画面に遷移する
    /// </summary>
    public void OnBackStartState()
    {

        // ResultAreaを非表示にする
        this.resultStateManager.gameObject.SetActive(false);

        // ゲームステートをStartに変更
        this.mEGameState = EGameState.READY;

        this.panel.SetActive(true);

        // ゲームのステート管理
        this.mSetGameState();
    }

    /// <summary>
    /// ゲームの準備ステートを開始する
    /// </summary>
    private void mSetGameReady()
    {

        // カード配布アニメーションが終了した後のコールバック処理を実装する
        this.CardCreate.OnCardAnimeComp = null;
        this.CardCreate.OnCardAnimeComp = () => {

            // ゲームステートをGAME状態に変更する
            this.mEGameState = EGameState.GAME;
            this.mSetGameState();
        };

        // 一致したカードIDリストを初期化
        this.mContainCardIdList.Clear();

        // カードリストを生成する
        this.CardCreate.CreateCard();

        // 時間を初期化
        this.mElapsedTime = 60f;

        //得点を初期化
        this.mScore = 0f;

        // ゲームステートを初期化
        //this.mEGameState = EGameState.START;
    }

    // Update is called once per frame
    void Update()
    {

        // GameState が GAME状態なら
        if (this.mEGameState == EGameState.GAME)
        {
            this.panel.SetActive(false);

            this.scoreManager.gameObject.SetActive(true);
            this.timerManager.gameObject.SetActive(true);

            this.timerManager.SetText((int)this.mElapsedTime);

            this.scoreManager.SetText((int)this.mScore);

            this.mElapsedTime -= Time.deltaTime;

            // 選択したカードが２枚以上になったら
            if (GameStateController.Instance.SelectedCardIdList.Count >= 2)
            {

                // 最初に選択したCardIDを取得する
                int selectedId = GameStateController.Instance.SelectedCardIdList[0];

                // 2枚目にあったカードと一緒だったら
                if (selectedId == GameStateController.Instance.SelectedCardIdList[1])
                {
                    //得点処理
                    if(selectedId==0)
                    {
                        this.mScore += 1000;
                    }
                    else if(selectedId==1)
                    {
                        this.mScore += 700;
                    }
                    else if (selectedId == 2)
                    {
                        this.mScore += 500;
                    }
                    else
                    {
                        this.mScore += 100;
                    }

                    Debug.Log($"Contains! {selectedId}");
                    // 一致したカードIDを保存する
                    this.mContainCardIdList.Add(selectedId);

                    //this.mScore += 100;
                }

                // カードの表示切り替えを行う
                this.CardCreate.HideCardList(this.mContainCardIdList);

                // 選択したカードリストを初期化する
                GameStateController.Instance.SelectedCardIdList.Clear();
            }
            // 配置した全種類のカードを獲得したら
            if (this.mContainCardIdList.Count >= 6)
            {
                
                // ゲームをリザルトステートに遷移する
                this.mEGameState = EGameState.RESULT;
                this.mSetGameState();

                this.scoreManager.gameObject.SetActive(false);
                this.timerManager.gameObject.SetActive(false);
            }
            if(mElapsedTime <= 0)
            {
                // ゲームをリザルトステートに遷移する
                this.mEGameState = EGameState.RESULT;
                this.mSetGameState();

                this.scoreManager.gameObject.SetActive(false);
                this.timerManager.gameObject.SetActive(false);
            }
        }

    }
}
