using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour 
{
	public bool isEnabled = false;

	Transform cameraPosition;

	public static PlayerScript _instance;
	public static PlayerScript Instance { get { return _instance; } }

	void Awake()
	{
		if (_instance != null && _instance != this)
			Destroy (this.gameObject);
		else
			_instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		cameraPosition = transform.FindChild ("CameraPosition");
		UpdatePlayer ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdatePlayer()
	{
		if (cameraPosition.childCount > 0)
			isEnabled = true;
		else
			isEnabled = false;
	}
}
