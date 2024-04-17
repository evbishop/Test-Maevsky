using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scorewarrior.Runtime.Weapons
{
    [CreateAssetMenu(fileName = "WeaponBonusesInfo", menuName = "Scriptable Objects/Create Weapon Bonuses Info")]
    public class WeaponBonusesInfo : ScriptableObject
    {
        #region Singleton
        private static string s_assetName => nameof(WeaponBonusesInfo);
        private static string s_loadPath => $"Singletons/{s_assetName}";
        private static string s_savePath => $"Assets/Resources/{s_loadPath}.asset";

        private static WeaponBonusesInfo s_instance;

        public static WeaponBonusesInfo Instance
        {
            get
            {
                if (s_instance != null) return s_instance;
                s_instance = Resources.Load<WeaponBonusesInfo>(s_loadPath);
                if (s_instance != null) return s_instance;
                s_instance = CreateInstance<WeaponBonusesInfo>();
#if UNITY_EDITOR
                UnityEditor.AssetDatabase.CreateAsset(s_instance, s_savePath);
#endif
                return s_instance;
            }
        }
        #endregion

        [field: SerializeField] public int RollsQuantity { get; private set; } = 2;
        [field: SerializeField] public List<WeaponBonus> Bonuses { get; private set; }

        public WeaponStat GetRandomBonusValue(WeaponStatType statType)
        {
            WeaponBonus bonus = Bonuses.Find(bonus => bonus.Type == statType);
            return new WeaponStat() {Type = statType, Value = Random.Range(bonus.MinModifierValue, bonus.MaxModifierValue) };
        }
    }

    [Serializable]
    public struct WeaponBonus
    {
        public WeaponStatType Type;
        public float MinModifierValue;
        public float MaxModifierValue;
    }
}