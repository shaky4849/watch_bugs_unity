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
			if (Input.GetAxis ("Horizontal") == 0 && Input.GetAxis ("Vertical") == 0) {
				transform.RotateAround (rotationObject.position, rotationObject.up, Input.GetAxis ("Mouse X") * Time.deltaTime * verticalSensivity);
				transform.RotateAround (rotationObject.position, rotationObject.right, Input.GetAxis ("Mouse Y") * Time.deltaTime * horizontalSensivity * -1f);
				transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
				//transform.Rotate (Input.GetAxis ("Mouse Y") * Time.deltaTime * horizontalSensivity, 0, 0);
				//transform.Rotate (0, Input.GetAxis ("Mouse X") * Time.deltaTime * verticalSensivity * -1f, 0, Space.World);
			} else {
				transform.localPosition = new Vector3 (0, 0, -2f);
				transform.localEulerAngles = Vector3.zero;
			}

			if (Input.GetKeyDown (KeyCode.Escape) && transform.parent.gameObject.tag != "Player") 
			{
				DoAction(GameObject.Find("Player"));
			}

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
		//PlayerScript.Instance.UpdatePlayer ();
		isEnabled = true;
		//hitInfo.transform.GetComponent<SphereCollider> ().radius = 0;
	}

	void DoAction(GameObject gameObject)
	{
		Transform cameraNewPosition;

		switch (gameObject.tag) 
		{
			case "SecCamera":
				cameraNewPosition = gameObject.transform.FindChild("CameraPosition");
				Camera.main.transform.parent = cameraNewPosition;
				LeanTween.moveLocal (Camera.main.gameObject, Vector3.zero, .8f).setOnComplete(OnCompleteAnim);
				LeanTween.rotateLocal (Camera.main.gameObject, Vector3.zero, .8f);
				break;
			case "Player":
				cameraNewPosition = gameObject.transform.FindChild ("CameraPosition");
				Camera.main.transform.parent = cameraNewPosition;
				LeanTween.moveLocal (Camera.main.gameObject, new Vector3 (0, 0, -2f), .8f).setOnComplete (OnCompleteAnim);
				LeanTween.rotateLocal (Camera.main.gameObject, Vector3.zero, .8f);
				break;
		}
	}
}
