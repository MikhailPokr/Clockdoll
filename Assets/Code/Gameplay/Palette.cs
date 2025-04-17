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
    public CardView CardPrefab;
    public Dices DicePrefabs;
    public NoteView NotePrefab;
    public DollCardView DollCardPrefab;
    [Header("Спрайты")]
    [SerializeField] private Sprite[] _diceNumbers;
    public Sprite[] DiceNumbers => _diceNumbers;
    public Suit SuitsSprites;
    public Markers MarkerSprites;
    public Sprite[] Numbers;

    [Serializable]
    public struct Doll
    {
        public int Index;
        public Sprite Symbol;
        public DollView Prefab;
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
        public DiceView D4;
        public DiceView D6;
        public DiceView D8;
        public DiceView D10;
        public DiceView D12;
        public DiceView D16;
        public DiceView D20;
    }
}

