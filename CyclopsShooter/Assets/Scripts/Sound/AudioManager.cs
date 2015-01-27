using System.Collections.Generic;
using TBE_3DCore;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	struct AudObj {
		public GameObject Obj { get; set; }
		public AudioSource SourceComponent { get; set; }
	}

	private static int _numCreated = 0;
	static readonly LinkedList<AudObj> Pool = new LinkedList<AudObj>();
	ICollection<AudObj> objInUse = new HashSet<AudObj>();

	private static AudObj GetAudObj()
	{
		if (Pool.Count <= 0)
		{
			var toAdd = new AudObj() { Obj = new GameObject("AudioSource" + _numCreated) };
			toAdd.SourceComponent = toAdd.Obj.AddComponent<AudioSource>();
			toAdd.SourceComponent.loop = false;
			/* using 3d
			toAdd.SourceComponent.minimumDistance = 4;
			/*/
			//toAdd.SourceComponent.minDistance = 4;
			//*/
			Pool.AddLast(toAdd);
			_numCreated++;
			Debug.Log("Creating new Audio Source (total: " + _numCreated + ")");
		}
		var ret = Pool.Last.Value;
		Pool.RemoveLast();
		return ret;
	}
	public void PlayClip(AudioClip toPlay)
	{
		if (!gameObject.activeInHierarchy)
		{
			OnDisable();
			return;
		}
		var src = GetAudObj();
		src.Obj.transform.parent = transform; // this transform
		src.Obj.transform.localPosition = Vector3.zero;
		src.SourceComponent.clip = toPlay;
		src.SourceComponent.PlayOneShot(toPlay);
		if (!gameObject.activeInHierarchy)
		{
			Pool.AddFirst(src);
			return;
		}
		StartCoroutine(Free(src));
	}
	private IEnumerator Free(AudObj o)
	{
		objInUse.Add(o);
		yield return new WaitForSeconds(o.SourceComponent.clip.length);
		Pool.AddFirst(o);
		objInUse.Remove(o);
	}
	void OnDisable()
	{
		foreach (var aud in objInUse)
		{
			Pool.AddFirst(aud);
		}
		objInUse.Clear();
	}
}
