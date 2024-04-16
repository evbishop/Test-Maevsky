using UnityEngine;

namespace Scorewarrior.Test.Bootstrap
{
	public class SpawnPoint : MonoBehaviour
	{
		[field: SerializeField] public Team Team { get; private set; }
	}
}