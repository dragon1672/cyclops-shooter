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
		if(Input.GetKeyDown(KeyCode.J))
		{
			foreach (var cyclopsEnemy in _players)
			{
				cyclopsEnemy.DoDamage(cyclopsEnemy.Health * 2); // DIE
			}
		}
		if (_players.All(n => n.IsDead))
		{
			_activeInGame = false;
			_myManager.Continue();
		}

	}
}
