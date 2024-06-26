using UnityEngine;

namespace Scorewarrior.Runtime.Weapons
{
    [CreateAssetMenu(fileName = "Bullet Info", menuName = "Scriptable Objects/Create Bullet Info")]
    public class BulletInfo : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; } = 30f;
    }
}