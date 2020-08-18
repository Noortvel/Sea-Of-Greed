using System;
using UnityEngine;

public class SpringArm : MonoBehaviour {

	[SerializeField] private Transform target = null;
	[SerializeField] private float slerpFactor = 1;

	[SerializeField, Min(1)]
	private float mouseXSence = 1;
	[SerializeField, Min(1)]
	private float mouseYSence = 1;
	[SerializeField, Range(-60, 60)]
	private float minPitchRot = -60, maxPichRot = 60;

	private Vector3 offset;
	void Start () {
		target = transform.parent;
		offset = transform.localPosition;
		transform.parent = null;
		offset.y = 0;
	}
	void Update()
	{
		var y = Input.GetAxis("Mouse X");
		var x = Input.GetAxis("Mouse Y");
		var rot = transform.eulerAngles;
		rot.x += x * mouseXSence * Time.deltaTime;
		rot.y += y * mouseYSence * Time.deltaTime;
		rot.x = MMath.AngleTo180Norm(rot.x);
		rot.x = Mathf.Clamp(rot.x, minPitchRot, maxPichRot);
		transform.rotation = Quaternion.Euler(rot);
		var targetPosition = target.position;
		targetPosition.y = Mathf.Lerp(transform.position.y, targetPosition.y, 0.1f);
		transform.position = Vector3.Slerp(transform.position,
			targetPosition + offset, slerpFactor);

	}
}
