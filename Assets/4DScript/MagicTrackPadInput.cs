using UnityEngine;
using System.Collections;

public class MagicTrackPadInput : MonoBehaviour {
    public Transform Controller;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        transform.position = Controller.position;
        transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
    }
}
