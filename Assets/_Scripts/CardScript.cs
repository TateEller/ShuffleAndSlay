using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public CardSO info;
    public Image suitSlot;
    public Image numSlot;
    public Color redColor, blackColor;

    internal bool inHand = false;
    public float scaleSize = 1.2f;

    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        suitSlot.sprite = info.suitSprite;
        numSlot.sprite = info.numSprite;

        if (info.cardSuit == CardSO.suitOps.Spades || info.cardSuit == CardSO.suitOps.Clubs)
        {
            suitSlot.color = blackColor;
            numSlot.color = blackColor;
        }
        else
        {
            suitSlot.color = redColor;
            numSlot.color = redColor;
        }
    }

    public void OnMouseEnter()
    {
        if(inHand)
            this.transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
    }

    public void OnMouseExit()
    {
        if(inHand)
            this.transform.localScale = new Vector3(1, 1, 1);
    }

    public void OnPointerClick()
    {
        if (gameManager.turnPlaying) return;

        //size card down
        OnMouseExit();
        //remove from hand
        inHand = false;

        //update other var
        transform.parent.GetComponent<HandManager>().cardsInHand--;
        gameManager.PlayCard(this.gameObject);
        //delete from hand
        Destroy(gameObject);
    }
}
