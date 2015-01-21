using UnityEngine;
using System.Collections;

public class CyclopGameManager : MonoBehaviour
{
	public CyclopsMainPlayer Player;
	public RailSystem Rails;

    void Start()
    {
        Rails.CompletedSection += () => {
            var tmp = Player.GetComponent<CrouchMe>();
            Debug.Log("Removing Component "+tmp);
            if (tmp != null)
            {
                Destroy(tmp);
                Debug.Log("Removing Component Done");
            }
        };
        Rails.CompletedAll += () => {
            Debug.Log("You win");
        };
        Rails.EnterSection += () => {
            Debug.Log("Adding Component");
            Player.gameObject.AddComponent<CrouchMe>();
        };
    }

	public void Continue()
	{
		Rails.Continue();
	}
}
