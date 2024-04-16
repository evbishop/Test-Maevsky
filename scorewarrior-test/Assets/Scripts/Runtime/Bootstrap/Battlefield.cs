﻿using System.Collections.Generic;
using UnityEngine;
using Scorewarrior.Runtime.Characters;
using Scorewarrior.Runtime.Weapons;

namespace Scorewarrior.Runtime.Bootstrap
{
	public class Battlefield
	{
		private readonly Dictionary<Team, List<Vector3>> _spawnPositionsByTeam;
		private readonly Dictionary<Team, List<Character>> _charactersByTeam;

		private bool _isPaused;

		public Battlefield(Dictionary<Team, List<Vector3>> spawnPositionsByTeam)
		{
			_spawnPositionsByTeam = spawnPositionsByTeam;
			_charactersByTeam = new();
		}

		public void Start(CharacterPrefab[] prefabs)
		{
			_isPaused = false;
			_charactersByTeam.Clear();

			List<CharacterPrefab> availablePrefabs = new List<CharacterPrefab>(prefabs);
			foreach (var (team, positions) in _spawnPositionsByTeam)
			{
				List<Character> characters = new();
				_charactersByTeam.Add(team, characters);
				int i = 0;
				while (i < positions.Count && availablePrefabs.Count > 0)
				{
					int index = Random.Range(0, availablePrefabs.Count);
					characters.Add(CreateCharacterAt(availablePrefabs[index], this, positions[i]));
					availablePrefabs.RemoveAt(index);
					i++;
				}
			}
		}

		public bool TryGetNearestAliveEnemy(Character character, out Character target)
		{
			if (TryGetTeam(character, out Team team))
			{
				Character nearestEnemy = null;
				float nearestDistance = float.MaxValue;
				List<Character> enemies = team == Team.Green ? _charactersByTeam[Team.Red] : _charactersByTeam[Team.Green];
				foreach (Character enemy in enemies)
				{
					if (enemy.IsAlive)
					{
						float distance = Vector3.Distance(character.Position, enemy.Position);
						if (distance < nearestDistance)
						{
							nearestDistance = distance;
							nearestEnemy = enemy;
						}
					}
				}
				target = nearestEnemy;
				return target != null;
			}
			target = default;
			return false;
		}

		public bool TryGetTeam(Character target, out Team team)
		{
			foreach (var charactersPair in _charactersByTeam)
			{
				List<Character> characters = charactersPair.Value;
				foreach (Character character in characters)
				{
					if (character == target)
					{
						team = charactersPair.Key;
						return true;
					}
				}
			}
			team = default;
			return false;
		}

		public void Update(float deltaTime)
		{
			if (!_isPaused)
			{
				foreach (var charactersPair in _charactersByTeam)
				{
					List<Character> characters = charactersPair.Value;
					foreach (Character character in characters)
					{
						character.Update(deltaTime);
					}
				}
			}
		}

		private static Character CreateCharacterAt(CharacterPrefab prefab, Battlefield battlefield, Vector3 position)
		{
			CharacterPrefab character = Object.Instantiate(prefab);
			character.transform.position = position;
			return new Character(character, new Weapon(character.Weapon), battlefield, character.Info);
		}
	}
}