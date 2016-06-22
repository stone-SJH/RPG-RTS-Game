using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StroryController : MonoBehaviour {
    private Image image;
    private float speed = 30;
    private bool isScroll = true;

	// Use this for initialization
	void Start () {
        image = GetComponentInChildren<Image>( );
	
	}
	
	// Update is called once per frame
	void Update () {
        if (image.transform.localPosition.y < 0) {
			image.transform.Translate (new Vector3 (0, Time.deltaTime * speed, 0));
		} else {
			isScroll = false;
		}

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(0)) {
            if (isScroll) {
                isScroll = false;
                image.transform.localPosition = new Vector3(0, 0, 0);
            } else {
				Application.LoadLevel("begin");
            }
        }
	}
}
