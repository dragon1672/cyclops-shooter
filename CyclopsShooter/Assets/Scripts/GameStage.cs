using System.Linq;
using UnityEngine;
using System.Collections;

public class GameStage : MonoBehaviour
{
	public RailPointManager PreceedingRailPointManager;

	private CyclopGameManager _myManager;
	private bool _activeInGame = false;
	private CyclopsEnemy[] _players;
	// Use this for initialization
	void Start ()
	{
		_players = GetComponentsInChildren<CyclopsEnemy>();
		_myManager = GetComponentInParent<CyclopGameManager>();
		PreceedingRailPointManager.CompletedEvent += () =>
		{
			_activeInGame = true;
			foreach (var cyclopsEnemy in _players)
			{
				cyclopsEnemy.GameStarted();
			}
		};
	}

	// Update is called once per frame
	void Update ()
	{
		if (!_activeInGame) return;
		if (_players.All(n => n.IsDead))
		{
			Debug.Log("All Dead");
			_activeInGame = false;
			_myManager.Continue();
		}

	}
}
