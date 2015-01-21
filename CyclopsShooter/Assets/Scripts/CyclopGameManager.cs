using UnityEngine;
using System.Collections;

public class CyclopGameManager : MonoBehaviour
{
	public CyclopsMainPlayer Player;
	public RailSystem Rails;

    void Start()
    {
	    Rails.CompletedSection += () =>
	    {
		    var tmp = Player.GetComponent<CrouchMe>();
		    if (tmp != null)
		    {
			    tmp.enabled = false;
			    tmp.UnCrouch();
		    }
	    };
	    Rails.CompletedAll += () => {
			Debug.Log("You win");
	    };
	    Rails.EnterSection += () =>
	    {
		    var tmp = Player.GetComponent<CrouchMe>() ?? Player.gameObject.AddComponent<CrouchMe>();
		    tmp.enabled = true;
	    };
    }

	public void Continue()
	{
		Rails.Continue();
	}
}
