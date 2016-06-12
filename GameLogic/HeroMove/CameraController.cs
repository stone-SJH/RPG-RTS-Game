using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    //视野控制参数
    public float minFov = 15.0f;
    public float maxFov = 150.0f;
    public float sensitivity = 10.0f;
    public float angleRotate = 2.0f;
    public float distMove = 5.0f;
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        bool leftPress = Input.GetMouseButton(0);
        bool rightPress = Input.GetMouseButton(1);
        bool scrollPress = Input.GetMouseButton(2);
        bool altPress = Input.GetKey(KeyCode.LeftAlt);
        bool ctrlPress = Input.GetKey(KeyCode.LeftControl);

        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        if (rightPress)
            transform.Rotate(my * angleRotate, mx * angleRotate, 0);
        if (leftPress & altPress)
            transform.Translate(-mx * distMove, 0, -my * distMove);
        if (rightPress & altPress)
            transform.Translate(0, -my * distMove, 0);

        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
	}

}
