

using UnityEngine;

public class GeneralEventsListenersCS : MonoBehaviour
{
	public enum MyEvents
	{
		CHANGE_COLOR,
		JUMP,
		LENGTH
	}

	private Vector3 towardsRotation;

	private float turnForLength = 0.5f;

	private float turnForIter;

	private Color fromColor;

	private void Awake()
	{
		LeanTween.LISTENERS_MAX = 100;
		LeanTween.EVENTS_MAX = 2;
		fromColor = GetComponent<Renderer>().material.color;
	}

	private void Start()
	{
		LeanTween.addListener(base.gameObject, 0, changeColor);
		LeanTween.addListener(base.gameObject, 1, jumpUp);
	}

	private void jumpUp(LTEvent e)
	{
		GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 300f);
	}

	private void changeColor(LTEvent e)
	{
		Transform transform = (Transform)e.data;
		float num = Vector3.Distance(transform.position, base.transform.position);
		LeanTween.value(to: new Color(UnityEngine.Random.Range(0f, 1f), 0f, UnityEngine.Random.Range(0f, 1f)), gameObject: base.gameObject, from: fromColor, time: 0.8f).setLoopPingPong(1).setDelay(num * 0.05f)
			.setOnUpdate(delegate(Color col)
			{
				GetComponent<Renderer>().material.color = col;
			});
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer != 2)
		{
			towardsRotation = new Vector3(0f, UnityEngine.Random.Range(-180, 180), 0f);
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.layer != 2)
		{
			turnForIter = 0f;
			turnForLength = UnityEngine.Random.Range(0.5f, 1.5f);
		}
	}

	private void FixedUpdate()
	{
		if (turnForIter < turnForLength)
		{
			GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * Quaternion.Euler(towardsRotation * Time.deltaTime));
			turnForIter += Time.deltaTime;
		}
		GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 4.5f);
	}

	private void OnMouseDown()
	{
		if (UnityEngine.Input.GetKey(KeyCode.J))
		{
			LeanTween.dispatchEvent(1);
		}
		else
		{
			LeanTween.dispatchEvent(0, base.transform);
		}
	}
}
