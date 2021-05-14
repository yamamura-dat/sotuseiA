using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCreateManager : MonoBehaviour
{
    // 生成するCardオブジェクト
    public Card CardPrefab;

    // 「カード」を生成する親オブジェクト
    public RectTransform CardCreateParent;

    void Start()
    {

        Card card = Instantiate<Card>(this.CardPrefab, this.CardCreateParent);
    }
}
