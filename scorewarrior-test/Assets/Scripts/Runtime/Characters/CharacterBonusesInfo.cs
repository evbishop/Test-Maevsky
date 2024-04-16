using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scorewarrior.Runtime.Characters
{
    [CreateAssetMenu(fileName = "CharacterBonusesInfo", menuName = "Scriptable Objects/Create Character Bonuses Info")]
    public class CharacterBonusesInfo : ScriptableObject
    {
        #region Singleton
        private static string s_assetName => nameof(CharacterBonusesInfo);
        private static string s_loadPath => $"Singletons/{s_assetName}";
        private static string s_savePath => $"Assets/Resources/{s_loadPath}.asset";

        private static CharacterBonusesInfo s_instance;

        public static CharacterBonusesInfo Instance
        {
            get
            {
                if (s_instance != null) return s_instance;
                s_instance = Resources.Load<CharacterBonusesInfo>(s_loadPath);
                if (s_instance != null) return s_instance;
                s_instance = CreateInstance<CharacterBonusesInfo>();
#if UNITY_EDITOR
                UnityEditor.AssetDatabase.CreateAsset(s_instance, s_savePath);
#endif
                return s_instance;
            }
        }
        #endregion

        [field: SerializeField] public List<CharacterBonus> Bonuses { get; private set; }

        public CharacterStat GetRandomBonusValue(CharacterStatType statType)
        {
            CharacterBonus bonus = Bonuses.Find(bonus => bonus.Type == statType);
            return new CharacterStat() {Type = statType, Value = Random.Range(bonus.MinModifierValue, bonus.MaxModifierValue) };
        }
    }

    [Serializable]
    public struct CharacterBonus
    {
        public CharacterStatType Type;
        public float MinModifierValue;
        public float MaxModifierValue;
    }
}