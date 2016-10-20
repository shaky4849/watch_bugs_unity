using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIAcessMessage : MonoBehaviour
{
	Text textMessage;
	RectTransform textRectTransform;
	RectTransform imageRectTransform;

	public Image backgroundImage;

	public static UIAcessMessage _instance;
	public static UIAcessMessage Instance { get { return _instance; } }

	void Awake()
	{
		if (_instance != null && _instance != this)
			Destroy (this.gameObject);
		else
			_instance = this;

		//textMessage = gameObject.GetComponent<Text> ();
		imageRectTransform = backgroundImage.GetComponent<RectTransform>();
		textRectTransform = gameObject.GetComponent<RectTransform> ();
	}

	public void ShowMessage(Vector3 messagePosition)
	{
		if (!gameObject.activeSelf)
			gameObject.SetActive (true);
		Vector2 uiNewPosition = GetCanvasPosition (messagePosition);
		//uiNewPosition.x += 65f;
		textRectTransform.anchoredPosition = uiNewPosition;
		//textMessage.text = message;
	}

	public void SetBackgroundScale(float value)
	{
		imageRectTransform.sizeDelta = new Vector2 (imageRectTransform.sizeDelta.x + value, imageRectTransform.sizeDelta.y);
	}

	public void ResetBackgroundScale()
	{
		imageRectTransform.sizeDelta = new Vector2 (0, imageRectTransform.sizeDelta.y);
	}

	public void CleanMessage()
	{
		if (gameObject.activeSelf)
			gameObject.SetActive (false);
		//textMessage.text = "";
	}

	Vector2 GetCanvasPosition(Vector3 worldObjectPoint)
	{
		//first you need the RectTransform component of your canvas
		RectTransform CanvasRect = transform.parent.GetComponent<RectTransform> ();

		//then you calculate the position of the UI element
		//0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.
		Vector2 ViewportPosition = Camera.main.WorldToViewportPoint (worldObjectPoint);
		Vector2 WorldObject_ScreenPosition = new Vector2 (
			                                    ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
			                                    ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

		//now you can set the position of the ui element
		return WorldObject_ScreenPosition;
	}
}
