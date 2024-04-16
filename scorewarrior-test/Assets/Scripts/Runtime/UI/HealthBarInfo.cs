using UnityEngine;

namespace Scorewarrior.Runtime.UI
{
    [CreateAssetMenu(fileName = "Health Bar Info", menuName = "Scriptable Objects/Create Health Bar Info")]
    public class HealthBarInfo : ScriptableObject
    {
        [field: SerializeField] public Color GreenTeamFG { get; private set; }
		[field: SerializeField] public Color GreenTeamBG { get; private set; }
        [field: SerializeField] public Color RedTeamFG { get; private set; }
		[field: SerializeField] public Color RedTeamBG { get; private set; }
    }
}