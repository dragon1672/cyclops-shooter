using System.Collections.Generic;
using TBE_3DCore;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	struct AudObj {
		public GameObject Obj { get; set; }
		public TBE_Source SourceComponent { get; set; }
	}

	private static int _numCreated = 0;
	static readonly LinkedList<AudObj> Pool = new LinkedList<AudObj>();
	private static AudObj GetAudObj()
	{
		if (Pool.Count <= 0)
		{
			var toAdd = new AudObj() {Obj = new GameObject("AudioSource")};
			toAdd.SourceComponent = toAdd.Obj.AddComponent<TBE_Source>();
			toAdd.SourceComponent.loop = false;
			toAdd.SourceComponent.minimumDistance = 4;
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
		var src = GetAudObj();
		src.Obj.transform.parent = transform; // this transform
		src.Obj.transform.localPosition = Vector3.zero;
		src.SourceComponent.clip = toPlay;
		src.SourceComponent.PlayOneShot(toPlay);
		StartCoroutine(Free(src));
	}
	private static IEnumerator Free(AudObj o)
	{
		yield return new WaitForSeconds(o.SourceComponent.clip.length);
		Pool.AddFirst(o);
	}
}
