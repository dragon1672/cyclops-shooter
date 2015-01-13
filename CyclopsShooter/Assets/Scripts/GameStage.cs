using System.Linq;
using TBE_3DCore;
using UnityEngine;
using System.Collections;

public class GameStage : MonoBehaviour
{
	public CyclopsEnemy[] Players;
	public Collider MyTrigger;
	public RailSystem Rails;
	private bool activeInGame = false;
	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponentInChildren<TBE_GlobalListener>() == null) return;

		activeInGame = true;
		foreach (var cyclopsEnemy in Players)
		{
			cyclopsEnemy.GameStarted();
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (!activeInGame) return;
		if (Players.All(n => n.IsDead))
		{
			activeInGame = false;
			Rails.Continue();
		}

	}
}
