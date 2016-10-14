using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{
	public Transform cameraObject;

	public int raycastDistance;
	public int horizontalSensivity;
	public int verticalSensivity;

	public LayerMask raycastLayer;

	public bool isEnabled = true;

	RaycastHit hitInfo;

	GameObject raycastObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isEnabled) 
		{
			cameraObject.Rotate (Input.GetAxis ("Mouse Y") * Time.deltaTime * horizontalSensivity, 0, 0);
			cameraObject.Rotate (0, Input.GetAxis ("Mouse X") * Time.deltaTime * verticalSensivity * -1f, 0, Space.World);

			if (Physics.Raycast (cameraObject.position, cameraObject.forward, out hitInfo, raycastDistance, raycastLayer)) 
			{
				UIAcessMessage.Instance.ShowMessage (hitInfo.transform.position, "Hack");

				if (Input.GetKeyDown (KeyCode.E)) 
				{
					raycastObject = hitInfo.transform.gameObject;
					isEnabled = false;
					UIAcessMessage.Instance.CleanMessage ();
					Transform cameraNewPosition = hitInfo.transform.FindChild("CameraPosition");
					Camera.main.transform.parent = cameraNewPosition;
					LeanTween.move (Camera.main.gameObject, cameraNewPosition.position, .8f).setOnComplete(OnCompleteAnim);
					LeanTween.rotate (Camera.main.gameObject, cameraNewPosition.rotation.eulerAngles, .8f);
				}
			}
			else
				UIAcessMessage.Instance.CleanMessage ();
		}
	}

	void OnDrawGizmos()
	{

	}

	void OnCompleteAnim()
	{
		hitInfo.transform.GetComponent<SecurityCamera> ().isEnabled = true;
		//hitInfo.transform.GetComponent<SphereCollider> ().radius = 0;
	}
}
