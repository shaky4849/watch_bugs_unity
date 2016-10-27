using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour 
{
	public bool isEnabled = false;

	public Animator playerAnimator;

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
		playerAnimator = gameObject.GetComponent<Animator> ();
		UpdatePlayer ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isEnabled) 
		{
			
				//Quaternion m_relativeRotation = Quaternion.LookRotation(Camera.main.transform.forward - transform.position);
				//transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, m_relativeRotation.eulerAngles.y, 0), Time.deltaTime * 100);//We use only Y ro rotate the enemy
			

			if (Input.GetAxis ("Vertical") > 0) 
			{
				transform.Translate (transform.forward * .1f, Space.Self);
				if (Input.GetKey (KeyCode.LeftShift)) 
				{
					playerAnimator.SetBool ("isWalking", false);
					playerAnimator.SetBool ("isRunning", true);
				} 
				else 
				{
					playerAnimator.SetBool ("isWalking", true);
					playerAnimator.SetBool ("isRunning", false);
				}
			} 
			else 
			{
				playerAnimator.SetBool ("isWalking", false);
				playerAnimator.SetBool ("isRunning", false);
			}
		}
	}

	public void UpdatePlayer()
	{
		if (cameraPosition.childCount > 0) 
		{
			isEnabled = true;
			cameraPosition = transform.FindChild ("CameraPosition");
		}
		else
			isEnabled = false;
	}
}
