using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scorewarrior.Runtime.Characters
{
    [CreateAssetMenu(fileName = "Character Info", menuName = "Scriptable Objects/Create Character Info")]
    public class CharacterInfo : ScriptableObject
    {
        [field: SerializeField] public List<CharacterStat> Stats { get; private set; }

        public float GetValue(CharacterStatType statType) => Stats.Find(stat => stat.Type == statType).Value;
    }

    [Serializable]
    public struct CharacterStat
    {
        public CharacterStatType Type;
        public float Value;
    }
}