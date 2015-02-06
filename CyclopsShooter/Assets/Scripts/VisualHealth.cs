using UnityEngine;
using System.Collections;

public class VisualHealth : MonoBehaviour {
	[SerializeField]
    private CanvasGroup _healthCanvas;
    public float Alpha{
	    get { return _healthCanvas.alpha;  }
	    set { _healthCanvas.alpha = value; }
    }
	// Use this for initialization
	void Start () {
        Alpha = 0.0f;
	}
}