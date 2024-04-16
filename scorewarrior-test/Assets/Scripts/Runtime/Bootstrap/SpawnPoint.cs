using UnityEngine;

namespace Scorewarrior.Runtime.Bootstrap
{
	public class SpawnPoint : MonoBehaviour
	{
		[field: SerializeField] public Team Team { get; private set; }
	}
}