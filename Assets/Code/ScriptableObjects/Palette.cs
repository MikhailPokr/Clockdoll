using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


[CreateAssetMenu(fileName = "PrefabPalette", menuName = "Game/PrefabPalette")]
internal class Palette : ScriptableObject, IService
{
    [Header("Префабы")]
    public Doll[] DollsData;
    public Light2D Light;
    public CardModel CardPrefab;
    public Dices DicePrefabs;
    public NoteModel NotePrefab;
    public DollCard DollCardPrefab;
    [Header("Спрайты")]
    [SerializeField] private Sprite[] _diceNumbers;
    public Sprite[] DiceNumbers => _diceNumbers;
    public Suit SuitsSprites;
    public Markers MarkerSprites;


    [Serializable]
    public struct Doll
    {
        public int Index;
        public Sprite Symbol;
        public DollModel Prefab;
    }

    [Serializable]
    public struct Markers
    {
        public Sprite Cross;
        public Sprite Circle;
        public Sprite Triangle;
    }


    [Serializable]
    public struct Suit
    {
        public Sprite Diamonds;
        public Sprite Spades;
        public Sprite Hearts;
        public Sprite Crosses;

        public readonly Sprite GetSuit(int index)
        {
            return index switch
            {
                1 => Diamonds,
                2 => Spades,
                3 => Hearts,
                4 => Crosses,
                _ => null
            };
        }
    }


    [Serializable]
    public struct Dices
    {
        public DiceModel D4;
        public DiceModel D6;
        public DiceModel D8;
        public DiceModel D10;
        public DiceModel D12;
        public DiceModel D16;
        public DiceModel D20;
    }
}

