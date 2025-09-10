using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPile : MonoBehaviour
{
    HandManager handMan;
    public GameObject cardPrefab;

    CardSO[] allCards;
    public List<CardSO> drawPile;

    private void Start()
    {
        handMan = FindObjectOfType<HandManager>();

        allCards = Resources.LoadAll<CardSO>("CardDeck");
        ShuffleDeck();
    }
    public void OnMouseDown()
    {
        if(handMan == null)
        {
            handMan = FindObjectOfType<HandManager>();
            if(handMan == null)
            {
                Debug.LogError("Still no HandManager");
                return;
            }
        }
        //when user clicks the draw pile
        if (drawPile.Count > 0)
        {
            if (handMan.cardsInHand < 6)
            {
                StartCoroutine(AutoFillHand());
            }
            //else hand is full
        }        
    }

    public CardSO DrawCard()
    {
        int ran = Random.Range(0, drawPile.Count);
        CardSO drawn = drawPile[ran];
        drawPile.RemoveAt(ran);
        return drawn;
    }

    IEnumerator AutoFillHand()
    {
        do
        {
            CardSO playerCard = DrawCard();
            CardSO enemyCard = DrawCard();

            handMan.AddToHand(playerCard);
            handMan.EnemyDrawCard(enemyCard);

            yield return new WaitForSeconds(0.3f);
        }
        while (handMan.cardsInHand < 6 && drawPile.Count > 0);

        if (drawPile.Count <= 0)
        {
            //if deck is empty
            gameObject.SetActive(false);
        }
    }

    public void ShuffleDeck()
    {
        drawPile = new List<CardSO>(allCards);
    }
}
