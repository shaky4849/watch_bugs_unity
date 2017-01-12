using UnityEngine;
using System.Collections;

public class DoorUnlocker : MonoBehaviour 
{
	public GameObject doorInteractable;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UnlockDoor()
	{
		if (doorInteractable != null) 
		{
			HingeJoint doorJoint = doorInteractable.GetComponent<HingeJoint> ();
			doorJoint.axis = new Vector3 (0, 1, 0);
		}
	}
}
