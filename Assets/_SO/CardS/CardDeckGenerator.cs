#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;

public class CardDeckGenerator : MonoBehaviour
{
    [MenuItem("Tools/Generate Default Card Deck")]
    public static void GenerateDeck()
    {
        string folderPath = "Assets/Resources/CardDeck/";
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string numSpriteSheet = "Assets/_SO/CardS/Sprites/Numbers+.png";
        string suitSpriteSheet = "Assets/_SO/CardS/Sprites/Suits+.png";

        Sprite[] numberSprites = AssetDatabase.LoadAllAssetsAtPath(numSpriteSheet).OfType<Sprite>().ToArray();
        Sprite[] suitSprites = AssetDatabase.LoadAllAssetsAtPath(suitSpriteSheet).OfType<Sprite>().ToArray();

        foreach (CardSO.suitOps suit in System.Enum.GetValues(typeof(CardSO.suitOps)))
        {
            foreach (CardSO.numOps num in System.Enum.GetValues(typeof(CardSO.numOps)))
            {
                CardSO newCard = ScriptableObject.CreateInstance<CardSO>();
                newCard.cardSuit = suit;
                newCard.cardNum = num;

                int numIn = GetSpriteIndex(num);
                int suitIn = GetSuitIndex(suit);
                newCard.numSprite = numberSprites[numIn];
                newCard.suitSprite = suitSprites[suitIn];

                newCard.cardBuff = CardSO.buffOps.None;

                string cardName = $"{num}_of_{suit}.asset";
                string assetPath = Path.Combine(folderPath, cardName);

                AssetDatabase.CreateAsset(newCard, assetPath);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("All 52-card deck generated!");
    }

    static int GetSpriteIndex(CardSO.numOps num)
    {
        switch (num)
        {
            case CardSO.numOps.Ace:
                return 0;
            case CardSO.numOps.Two:
                return 1;
            case CardSO.numOps.Three:
                return 2;
            case CardSO.numOps.Four:
                return 3;
            case CardSO.numOps.Five:
                return 4;
            case CardSO.numOps.Six:
                return 5;
            case CardSO.numOps.Seven:
                return 6;
            case CardSO.numOps.Eight:
                return 7;
            case CardSO.numOps.Nine:
                return 8;
            case CardSO.numOps.Ten:
                return 9;
            case CardSO.numOps.Jack:
                return 10;
            case CardSO.numOps.Queen:
                return 11;
            case CardSO.numOps.King:
                return 12;
            default:
                return 0; //fallback to Ace if something breaks
        }
    }

    static int GetSuitIndex(CardSO.suitOps suit)
    {
        switch (suit)
        {
            case CardSO.suitOps.Spades:
                return 0;
            case CardSO.suitOps.Clubs:
                return 1;
            case CardSO.suitOps.Diamonds:
                return 2;
            case CardSO.suitOps.Hearts:
                return 3;
            default:
                return 0; //fallback to spade if somthing breaks
        }
    }
}
#endif
