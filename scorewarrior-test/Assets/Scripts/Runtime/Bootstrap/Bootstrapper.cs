using System.Collections.Generic;
using UnityEngine;
using Scorewarrior.Runtime.Characters;
using Scorewarrior.Runtime.UI;
using System.Collections;

namespace Scorewarrior.Runtime.Bootstrap
{
	public class Bootstrapper : MonoBehaviour
	{
		[SerializeField] private CharacterPrefab[] _characters;
		[SerializeField] private SpawnPoint[] _spawns;
		[SerializeField] private UserInterfaceHandler _userInterfaceHandler;

		private Battlefield _battlefield;
		private Coroutine _updateCoroutine;
		private bool _isGameplayActive;

		private void Start()
		{
			_userInterfaceHandler.OnContinueClicked += StartGameplay;
			_userInterfaceHandler.OnReplayClicked += HandleReplayClicked;
		}

		private void OnDestroy()
		{
			if (_userInterfaceHandler != null)
			{
				_userInterfaceHandler.OnContinueClicked -= StartGameplay;
				_userInterfaceHandler.OnReplayClicked -= HandleReplayClicked;
			}
			if (_battlefield != null)
			{
				_battlefield.OnTeamDeath -= StartGameplay;
			}
		}

		private void StartGameplay()
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
			}
			_battlefield = new Battlefield(spawnPositionsByTeam);
			_battlefield.OnTeamDeath += StopGameplay;
			_battlefield.Start(_characters);
			_updateCoroutine = StartCoroutine(UpdateCoroutine());
		}

		private IEnumerator UpdateCoroutine()
		{
			_isGameplayActive = true;
			while (_isGameplayActive)
			{
				_battlefield.Update(Time.deltaTime);
				yield return null;
			}
		}

		private void StopGameplay()
		{
			_isGameplayActive = false;
			if (_updateCoroutine != null)
			{
				StopCoroutine(_updateCoroutine);
				_updateCoroutine = null;
			}
			_battlefield.OnTeamDeath -= StopGameplay;
			_userInterfaceHandler.ShowReplay();
		}

		private void HandleReplayClicked()
		{
			_userInterfaceHandler.ShowContinue();
			_battlefield.Clear();
			_battlefield = null;
		}
	}
}