using UnityEngine;
using System.Collections;

public class HoverCarControls : MonoBehaviour {

	[SerializeField] private Transform m_carMesh;

	public float Speed  = 90f;
	public float m_hoverheight = 1;
	public float m_hoverForce = 65;
	public float m_rotationAngle = 15;


	private float m_verticalInput;
	private float m_horizontalInput;

	private RaycastHit m_groundHitPoint;
	private RaycastHit m_tempHitPoint;

	private Rigidbody m_rigidBody;

	//public MoveCameraHoverCar m_camera;

	public RaycastHit GroundHitPoint
	{
		get{return m_groundHitPoint;}
	}

	// Use this for initialization
	void Awake () {
	
		m_rigidBody = gameObject.GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {

		m_horizontalInput = Input.GetAxis (InputManager.Instance.HLeft);
		m_verticalInput = Input.GetAxis (InputManager.Instance.VLeft);
	
	}

	private void FixedUpdate()
	{
		Ray ray = new Ray (transform.position, -Vector3.up);
		RaycastHit hit;
		
		if (Physics.Raycast (ray, out hit, m_hoverheight)) {
			
			float proportionalHeight = (m_hoverheight - hit.distance) / m_hoverheight;
			Vector3 appliedHoverForce = Vector3.up * proportionalHeight * m_hoverForce;
			m_rigidBody.AddForce (appliedHoverForce, ForceMode.Acceleration);
			m_tempHitPoint = hit;
		} 

		m_rigidBody.AddForce (new Vector3 (0,-45,0), ForceMode.Acceleration);
		
		m_groundHitPoint = m_tempHitPoint;
		
		Debug.DrawRay (ray.origin, ray.direction, Color.yellow);

		m_rigidBody.AddRelativeForce(0, 0f, m_verticalInput * Speed );
		transform.Rotate(new Vector3(0,m_horizontalInput*3,0));

		//m_camera.ExecuteMovement(); 

		//RotateCar(hit);
	}

	private void RotateCar(RaycastHit _hit)
	{
		float yRotation;

		if (Mathf.Abs (m_horizontalInput) > 0.1f || Mathf.Abs (m_verticalInput) > 0.1f) {
						//yRotation = m_camera.transform.eulerAngles.y;
				} else {
						yRotation = this.transform.eulerAngles.y; 
				}
		m_carMesh.localEulerAngles = Vector3.Lerp(new Vector3 (-m_verticalInput, 0, -m_horizontalInput), 

		                                          new Vector3 (-m_verticalInput * m_rotationAngle, 0, -m_horizontalInput * m_rotationAngle), Time.deltaTime*15);

		//m_carMesh.localEulerAngles = new Vector3 (-m_verticalInput  * m_rotationAngle, 0, -m_horizontalInput * m_rotationAngle);

		//this.transform.eulerAngles = Vector3.Lerp(this.transform.eulerAngles, new Vector3 (this.transform.eulerAngles.x, yRotation, this.transform.eulerAngles.z), Time.deltaTime*6);


		//this.transform.rotation = Quaternion.LookRotation (this.transform.forward, _hit.normal);


	}
}



























