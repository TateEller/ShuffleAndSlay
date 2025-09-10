using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public GameObject playCardSlot;
    public GameObject cardPrefab;

    internal int cardsInHand = 0;
    const int MAX_CARDS = 6;

    Animator handAni;
    GameManager gameMan;

    private void Start()
    {
        handAni = GetComponent<Animator>();
        gameMan = FindObjectOfType<GameManager>();
    }
    public void OnMouseEnter()
    {
        //enlarge card
        handAni.SetBool("MouseOver", true);
    }

    public void OnMouseExit()
    {
        //shrink card
        handAni.SetBool("MouseOver", false);
    }

    public void AddToHand(CardSO cardInfo)
    {
        if(cardsInHand < MAX_CARDS)
        {
            FindObjectOfType<SfxManager>().PlayDrawSFX();

            //instatiate card in hand
            GameObject tempCard = Instantiate(cardPrefab, this.transform);
            CardScript cs = tempCard.GetComponent<CardScript>();
            //transfer info
            cs.info = cardInfo;
            cs.inHand = true;
            cardsInHand++;
        }
    }

    public void EnemyDrawCard(CardSO cardInfo)
    {
        gameMan.enemyHand.Add(cardInfo);
    }
}
