using UnityEngine;
using System.Collections;

public class CyclopGameManager : MonoBehaviour
{
	public CyclopsMainPlayer Player;
	public RailSystem Rails;

	public void Continue()
	{
		Rails.Continue();
	}
}
