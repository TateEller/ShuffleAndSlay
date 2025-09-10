using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card", order = 1)]
public class CardSO : ScriptableObject
{
    public enum numOps {Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace = 14};
    public enum suitOps { Spades = 3, Clubs = 0, Diamonds = 1, Hearts = 2};
    public enum buffOps { None, TimesTwo, PlusFive, LowerBetter};


    public numOps cardNum;
    public Sprite numSprite;
    public suitOps cardSuit;
    public Sprite suitSprite;
    public buffOps cardBuff;
}
