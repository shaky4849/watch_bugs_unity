using UnityEngine;
using System.Collections;

public class HackableObject : MonoBehaviour
{
	public bool isEnabled = false;
	public LayerMask raycastLayer;

	string objectTag;

	void Start()
	{
		objectTag = tag;
	}

	public string myTag
	{
		get{ return objectTag; }
	}
}
