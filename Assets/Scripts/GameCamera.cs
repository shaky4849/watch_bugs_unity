using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour 
{
	public int raycastDistance;
	public int horizontalSensivity;
	public int verticalSensivity;

	public LayerMask raycastLayer;

	public bool isEnabled = true;
	public bool doAction = false;

	public float buttonCooldown = 1.0f;
	float buttonTotalCooldown = 0;

	RaycastHit hitInfo;

	GameObject raycastObject;

	Transform rotationObject;

	// Use this for initialization
	void Start () 
	{
		GetObjectRotation ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isEnabled) 
		{
			transform.RotateAround (rotationObject.position, rotationObject.up, Input.GetAxis ("Mouse X") * Time.deltaTime * verticalSensivity);
			transform.RotateAround (rotationObject.position, rotationObject.right, Input.GetAxis ("Mouse Y") * Time.deltaTime * horizontalSensivity * -1f);
			transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
			//transform.Rotate (Input.GetAxis ("Mouse Y") * Time.deltaTime * horizontalSensivity, 0, 0);
			//transform.Rotate (0, Input.GetAxis ("Mouse X") * Time.deltaTime * verticalSensivity * -1f, 0, Space.World);

			if (Physics.Raycast (transform.position, transform.forward, out hitInfo, raycastDistance, raycastLayer)) 
			{
				UIAcessMessage.Instance.ShowMessage (hitInfo.transform.position);

				if (Input.GetKey (KeyCode.E)) 
				{
					UIAcessMessage.Instance.SetBackgroundScale (.90f);
					buttonTotalCooldown += Time.deltaTime;
				} 
				else 
				{
					doAction = false;
					UIAcessMessage.Instance.ResetBackgroundScale ();
					buttonTotalCooldown = 0;
				}

				if (buttonTotalCooldown >= buttonCooldown)
					doAction = true;

				if (doAction) 
				{
					isEnabled = false;
					doAction = false;
					UIAcessMessage.Instance.CleanMessage ();
					UIAcessMessage.Instance.ResetBackgroundScale ();
					DoAction (hitInfo.transform.gameObject);
				}
			}
			else
				UIAcessMessage.Instance.CleanMessage ();
		}
	}

	void GetObjectRotation()
	{
		if (transform.parent.gameObject.tag == "Player")
			rotationObject = transform.parent;
		else
			rotationObject = transform;
	}

	void OnCompleteAnim()
	{		
		GetObjectRotation ();
		isEnabled = true;
		//hitInfo.transform.GetComponent<SphereCollider> ().radius = 0;
	}

	void DoAction(GameObject gameObject)
	{
		string tag = gameObject.GetComponent<HackableObject> ().myTag;

		switch (tag) 
		{
			case "SecCamera":
				Transform cameraNewPosition = hitInfo.transform.FindChild("CameraPosition");
				Camera.main.transform.parent = cameraNewPosition;
				LeanTween.move (Camera.main.gameObject, cameraNewPosition.position, .8f).setOnComplete(OnCompleteAnim);
				LeanTween.rotate (Camera.main.gameObject, cameraNewPosition.rotation.eulerAngles, .8f);
				break;
		}
	}
}
