using UnityEngine;
using System.Collections;

public class VisualHealth : MonoBehaviour {

    private CanvasGroup _healthCanvas;
    public float Alpha{
	    get { return _healthCanvas.alpha;  }
	    set { _healthCanvas.alpha = value; }
    }
	// Use this for initialization
	void Start () {
        _healthCanvas = gameObject.GetComponent<CanvasGroup>();
        Alpha = 1.0f;
	}
}