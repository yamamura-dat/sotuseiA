using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; //このusing を新規に記入しておいてください。
using System; //同じく

public class CardCreateManager : MonoBehaviour
{
    // 生成するCardオブジェクト
    public Card CardPrefab;

    // 「カード」を生成する親オブジェクト
    public RectTransform CardCreateParent;

    void Start()
    {
        //Card card = Instantiate<Card>(this.CardPrefab, this.CardCreateParent);

        // カード情報リスト
        List<CardData> cardDataList = new List<CardData>();
        // 表示するカード画像情報のリスト
        List<Sprite> imgList = new List<Sprite>();
        // Resources/Imageフォルダ内にある画像を取得する
        imgList.Add(Resources.Load<Sprite>("Image/buki_morningstar_flail"));
        imgList.Add(Resources.Load<Sprite>("Image/fantasy_dwarf"));
        imgList.Add(Resources.Load<Sprite>("Image/fantasy_game_character_slime"));

        // forを回す回数を取得する
        int loopCnt = imgList.Count;

        for(int i =0; i<loopCnt; i++)
        {
            // カード情報を生成する
            CardData cardata = new CardData(i, imgList[i]);
            cardDataList.Add(cardata);
        }
            // 生成したカードリスト２つ分のリストを生成する
            List<CardData> SumCardDataList = new List<CardData>();
            SumCardDataList.AddRange(cardDataList);
            SumCardDataList.AddRange(cardDataList);

            // リストの中身をランダムに再配置する
            List<CardData> randomCardDataList = SumCardDataList.OrderBy(a => Guid.NewGuid()).ToList();

            // カードオブジェクトを生成する
            foreach (var _cardData in randomCardDataList)
            {

                // Instantiate で Cardオブジェクトを生成
                Card card = Instantiate<Card>(this.CardPrefab, this.CardCreateParent);
                // データを設定する
                card.Set(_cardData);
            }
    }
}
