

using System;
using UnityEngine;

[Serializable]
public class OldGUIExamplesJS : MonoBehaviour
{
	public Texture2D grumpy;

	public Texture2D beauty;

	private float w;

	private float h;

	private LTRect buttonRect1;

	private LTRect buttonRect2;

	private LTRect buttonRect3;

	private LTRect buttonRect4;

	private LTRect grumpyRect;

	private LTRect beautyTileRect;

	public OldGUIExamplesJS()
	{
		w = Screen.width;
		h = Screen.height;
	}

	public void Start()
	{
		w = Screen.width;
		h = Screen.height;
		buttonRect1 = new LTRect(0.1f * w, 0.8f * h, 0.25f * w, 0.14f * h);
		buttonRect2 = new LTRect(1.2f * w, 0.8f * h, 0.2f * w, 0.14f * h);
		buttonRect3 = new LTRect(0.35f * w, 0f * h, 0.3f * w, 0.2f * h);
		buttonRect4 = new LTRect(0f * w, 0.4f * h, 0.3f * w, 0.2f * h, 1f, 15f);
		grumpyRect = new LTRect(0.5f * w - (float)grumpy.width / 2f, 0.5f * h - (float)grumpy.height / 2f, grumpy.width, grumpy.height);
		beautyTileRect = new LTRect(0f, 0f, 1f, 1f);
		LeanTween.move(buttonRect2, new Vector2(0.55f * w, buttonRect2.rect.y), 0.7f).setEase(LeanTweenType.easeOutQuad);
	}

	public void OnGUI()
	{
		Rect position = new Rect(0f * w, 0f * h, 0.2f * w, 0.14f * h);
		if (GUI.Button(position, "Move Cat") && !LeanTween.isTweening(grumpyRect))
		{
			Vector2 to = new Vector2(grumpyRect.rect.x, grumpyRect.rect.y);
			LeanTween.move(grumpyRect, new Vector2(1f * w - (float)grumpy.width, 0f * h), 1f).setEase(LeanTweenType.easeOutBounce);
			LeanTween.move(grumpyRect, to, 1f).setEase(LeanTweenType.easeOutBounce).setDelay(1f);
		}
		GUI.DrawTexture(grumpyRect.rect, grumpy);
		if (GUI.Button(buttonRect1.rect, "Scale Centered"))
		{
			LeanTween.scale(buttonRect1, new Vector2(buttonRect1.rect.width, buttonRect1.rect.height) * 1.2f, 0.25f).setEase(LeanTweenType.easeOutQuad);
			LeanTween.move(buttonRect1, new Vector2(buttonRect1.rect.x - buttonRect1.rect.width * 0.1f, buttonRect1.rect.y - buttonRect1.rect.height * 0.1f), 0.25f).setEase(LeanTweenType.easeOutQuad);
		}
		if (GUI.Button(buttonRect2.rect, "Scale"))
		{
			LeanTween.scale(buttonRect2, new Vector2(buttonRect2.rect.width, buttonRect2.rect.height) * 1.2f, 0.25f).setEase(LeanTweenType.easeOutBounce);
		}
		position = new Rect(0.76f * w, 0.53f * h, 0.2f * w, 0.14f * h);
		if (GUI.Button(position, "Flip Tile"))
		{
			LeanTween.move(beautyTileRect, new Vector2(0f, beautyTileRect.rect.y + 1f), 1f).setEase(LeanTweenType.easeOutBounce);
		}
		GUI.DrawTextureWithTexCoords(new Rect(0.8f * w, 0.5f * h - (float)beauty.height / 2f, (float)beauty.width * 0.5f, (float)beauty.height * 0.5f), beauty, beautyTileRect.rect);
		if (GUI.Button(buttonRect3.rect, "Alpha"))
		{
			LeanTween.alpha(buttonRect3, 0f, 1f).setEase(LeanTweenType.easeOutQuad);
			LeanTween.alpha(buttonRect3, 1f, 1f).setEase(LeanTweenType.easeOutQuad).setDelay(1f);
		}
		float a = 1f;
		Color color = GUI.color;
		color.a = a;
		Color color3 = GUI.color = color;
		if (GUI.Button(buttonRect4.rect, "Rotate"))
		{
			LeanTween.rotate(buttonRect4, 150f, 1f).setEase(LeanTweenType.easeOutElastic);
			LeanTween.rotate(buttonRect4, 0f, 1f).setEase(LeanTweenType.easeOutElastic).setDelay(1f);
		}
		GUI.matrix = Matrix4x4.identity;
	}

	public void Main()
	{
	}
}
