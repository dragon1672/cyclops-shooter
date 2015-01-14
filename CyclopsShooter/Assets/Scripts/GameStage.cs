using System.Linq;
using UnityEngine;
using System.Collections;

public class GameStage : MonoBehaviour
{
	public ColliderNotify MyTrigger;
	private CyclopGameManager _myManager;
	private bool _activeInGame = false;
	private CyclopsEnemy[] _players;
	// Use this for initialization
	void Start ()
	{
		_players = GetComponentsInChildren<CyclopsEnemy>();
		_myManager = GetComponentInParent<CyclopGameManager>();
		MyTrigger.OnCollidionAction += OnCollisionEnter;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponentInChildren<CyclopsMainPlayer>() == null) return;

		_activeInGame = true;
		foreach (var cyclopsEnemy in _players)
		{
			cyclopsEnemy.GameStarted();
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (!_activeInGame) return;
		if (_players.All(n => n.IsDead))
		{
			_activeInGame = false;
			_myManager.Continue();
		}

	}
}
