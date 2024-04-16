using System.Collections.Generic;
using UnityEngine;
using Scorewarrior.Test.Characters;

namespace Scorewarrior.Test.Bootstrap
{
	public class Bootstrapper : MonoBehaviour
	{
		[SerializeField]
		private CharacterPrefab[] _characters;
		[SerializeField]
		private SpawnPoint[] _spawns;

		private Battlefield _battlefield;

		public void Start()
		{
			Dictionary<Team, List<Vector3>> spawnPositionsByTeam = new();
			for (int i = 0; i < _spawns.Length; i++)
			{
				SpawnPoint spawn = _spawns[i];
				if (spawnPositionsByTeam.TryGetValue(spawn.Team, out List<Vector3> spawnPoints))
				{
					spawnPoints.Add(spawn.transform.position);
				}
				else
				{
					spawnPositionsByTeam.Add(spawn.Team, new List<Vector3> { spawn.transform.position });
				}
				Destroy(spawn.gameObject);
			}
			_battlefield = new Battlefield(spawnPositionsByTeam);
			_battlefield.Start(_characters);
		}

		public void Update()
		{
			_battlefield.Update(Time.deltaTime);
		}
	}
}