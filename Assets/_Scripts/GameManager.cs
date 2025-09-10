using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject playerCardSlot, compCardSlot;
    public GameObject playerEffectSlot;
    public GameObject winEffect, loseEffect;
    AudioSource source;

    [SerializeField] AudioClip winSound, loseSound, bigWinSound, bigLoseSound;

    GameObject playerEffect, compEffect;

    DrawPile deck;

    public List<CardSO> enemyHand = new List<CardSO>();
    List<CardSO> discardPile = new List<CardSO>();

    GameObject playerCard, compCard;

    public TextMeshProUGUI scoreText, roundText;
    int playerWins = 0, compWins = 0;
    int pRoundWins = 0, cRoundWins = 0;

    internal bool turnPlaying = false;

    public float timeOnTable = 1f;

    private void Start()
    {
        deck = FindObjectOfType<DrawPile>();
        source = GetComponent<AudioSource>();
        scoreText.text = ($"Player: {playerWins}\nComputer: {compWins}");
    }

    public void PlayCard(GameObject card)
    {
        FindObjectOfType<SfxManager>().PlayPlaySFX();

        turnPlaying = true;

        //play player card
        playerCard = Instantiate(card, playerCardSlot.transform);
        playerCard.transform.position = playerCardSlot.transform.position;


        //show(draw) computer card
        EnemyTurn();

        //score game
        if (PlayerWin())
        {
            playerWins++;
            Instantiate(winEffect, playerEffectSlot.transform);
            PlayAudio(winSound);
        }
        else
        {
            compWins++;
            Instantiate(loseEffect, playerEffectSlot.transform);
            PlayAudio(loseSound);
        }

        //update score
        scoreText.text = ($"Player: {playerWins}\nComputer: {compWins}");


        //clear board
        //and check win in ClearBoard()
        StartCoroutine(ClearBoard());
    }

    void EnemyTurn()
    {
        //get card from enemy hand
        int tempCompCard = 0;

        //choose random move: highest number, best suit, or just random
        int move = Random.Range(0, 3);
        switch (move)
        {
            case (1):
            case (3):
                //choose highest number
                Debug.Log("Enemy play highest card");
                CardSO.numOps highestNum = 0;

                for (int i = 0; i < enemyHand.Count; i++)
                {
                    if (enemyHand[i].cardNum > highestNum)
                    {
                        highestNum = enemyHand[i].cardNum;
                        tempCompCard = i;
                    }
                }
                break;
            case (2):
            case (4):
                //choose best suit
                Debug.Log("Enemy play best suit");
                CardSO.suitOps bestSuit = 0;

                for (int i = 0; i < enemyHand.Count; i++)
                {
                    if (enemyHand[i].cardSuit > bestSuit)
                    {
                        highestNum = enemyHand[i].cardNum;
                        tempCompCard = i;
                    }
                }
                break;
            case (0):
            default:
                //choose random card
                Debug.Log("Enemy play random");
                tempCompCard = Random.Range(0, enemyHand.Count);
                break;

        }

        CardSO cardInfo = enemyHand[tempCompCard];

        //instantiate card on table
        compCard = Instantiate(deck.cardPrefab, compCardSlot.transform);
        compCard.GetComponent<CardScript>().info = cardInfo;
        compCard.GetComponent<CardScript>().inHand = false;

        //remove card from enemy hand
        enemyHand.RemoveAt(tempCompCard);
    }

    bool PlayerWin()
    {

        //compare cards
        int playerNum = ((int)playerCard.GetComponent<CardScript>().info.cardNum);
        int computerNum = ((int)compCard.GetComponent<CardScript>().info.cardNum);

        //check card buffs

        //2 beats Ace
        if (playerNum == 14 && computerNum == 2)
            return false;
        if (playerNum == 2 && computerNum == 14)
            return true;

        //normal comapring
        if (playerNum > computerNum)
            return true;
        else if (playerNum < computerNum)
            return false;

        //check suits
        //Spades > Hearts > Diamonds > Clubs
        int playerSuit = ((int)playerCard.GetComponent<CardScript>().info.cardSuit);
        int computerSuit = ((int)compCard.GetComponent<CardScript>().info.cardSuit);

        if (playerSuit > computerSuit)
            return true;

        return false; //default to compWin
        //in theory its impossible for same suit (maybe)
    }

    IEnumerator ClearBoard()
    {
        yield return new WaitForSeconds(timeOnTable);

        //add to discard pile
        discardPile.Add(playerCard.GetComponent<CardScript>().info);
        discardPile.Add(compCard.GetComponent<CardScript>().info);

        //remove from table
        Destroy(playerCard);
        Destroy(compCard);
        Destroy(playerEffect);
        Destroy(compEffect);

        //check if all cards have been played
        if (CheckGameReset())
        {
            ResetGame();
        }

        turnPlaying = false;
    }

    bool CheckGameReset()
    {
        //check if draw pile is empty
        if (deck.drawPile.Count <= 0)
        {
            //check if both player and computer hands are empty
            if(enemyHand.Count <= 0)
            {
                if (FindObjectOfType<HandManager>().cardsInHand <= 0)
                {
                    //round over
                    return true;
                }
                //else player still has cards
            }
            //else enemy still has cards
        }
        //else cards still in the draw pile
        return false;
    }

    void ResetGame()
    {
        Debug.Log("***Reset Game");

        //generate new enemy
        FindObjectOfType<RandomSlime>().RandomFeatures();

        //reshuffle cards
        deck.gameObject.SetActive(true);
        deck.ShuffleDeck();
        FindObjectOfType<SfxManager>().PlayShuffleSFX();

        //calculate score
        if (playerWins > compWins)
        {
            pRoundWins++;
            PlayAudio(bigWinSound);
        }
        else if (playerWins < compWins)
        {
            cRoundWins++;
            PlayAudio(bigLoseSound);
        }
        //else it's a tie

        //reset score
        playerWins = 0;
        compWins = 0;

        //update UI
        scoreText.text = ($"Player: {playerWins}\nComputer: {compWins}");
        roundText.text = ($"Player: {pRoundWins}\nComputer: {cRoundWins}");

    }

    public void PlayAudio(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
}
