/*
using UnityEngine;
using System.Collections;

public class MoveCharacterCharacterController : MonoBehaviour {
	
	[SerializeField] private CharacterController m_characterController;
	[SerializeField] private Transform m_camera;
	[SerializeField] private MoveCamera m_cameraScript;
	
	[SerializeField] private GameObject m_playerMesh;
	
	[SerializeField] private float m_speed = 8;
	[SerializeField] private float m_gravity = 2;
	[SerializeField] private float m_jumpSpeed = 20;
	
	[SerializeField] private Animator m_animator;
	[SerializeField] private IKHandling m_ik;
	
	private bool m_moving;
	
	private float m_horizontalInput;
	private float m_verticalInput;
	
	private float m_smoothedHorizontalInput;
	private float m_smoothedVerticalInput;
	
	private float m_groundHitAngle;
	
	private Vector3 m_lookDirection = Vector3.zero;
	
	private RaycastHit m_groundHit;
	private RaycastHit m_frontHit;
	private RaycastHit m_ledgeHit;
	
	private bool m_jumping;
	
	private bool m_isGrounded;
	private bool m_isGroundedRaycast;
	
	private Vector3 m_moveDirection;
	
	private bool m_inAir;
	
	private bool m_playerControlling;
	
	private float m_combinedSpeed;
	
	public bool PlayerControlling
	{
		get
		{
			return m_playerControlling;
		}
		set
		{
			m_playerControlling = value;
		}
	}

	void Update () 
	{
		GetInput();
		CheckEnvironment();
		ExecuteMovement();
		ExecuteAnimations();
	}

	private float SmoothInputHelper(float _from, float _to)
	{
		float returnValue;
		returnValue = Mathf.Lerp(_from, _to, Time.deltaTime * 5);
		if(Mathf.Abs (returnValue) < 0.05f)
		{
			return returnValue;
		}
		return returnValue;
	}
	
	private void GetInput()
	{
		m_horizontalInput = Input.GetAxis(InputManager.Instance.Get(InputManager.EInput.hLeft)); //Mathf.Lerp(m_horizontalInput, Input.GetAxis ("HorizontalStickLeft"), Time.deltaTime *7);
		m_verticalInput = Input.GetAxis(InputManager.Instance.Get(InputManager.EInput.vLeft));//Mathf.Lerp(m_verticalInput, Input.GetAxis ("VerticalStickLeft"), Time.deltaTime *7);
				
		if( Mathf.Abs (m_horizontalInput) > 0 || Mathf.Abs(m_verticalInput) > 0)
			m_moving = true;
		else
			m_moving = false;	
		
		m_smoothedHorizontalInput = SmoothInputHelper(m_smoothedHorizontalInput, m_horizontalInput);// Mathf.Lerp(m_smoothedHorizontalInput, m_horizontalInput, Time.deltaTime * 5);
		m_smoothedVerticalInput = SmoothInputHelper(m_smoothedVerticalInput, m_verticalInput);//Mathf.Lerp(m_smoothedVerticalInput, m_verticalInput, Time.deltaTime * 5);
		
		m_combinedSpeed = Mathf.Abs(m_horizontalInput) + Mathf.Abs(m_verticalInput);
		
		m_combinedSpeed = Mathf.Clamp(m_combinedSpeed, 0,1);
	}
	private void CheckEnvironment()
	{
		RaycastHit hit;
		
		Ray downRay = new Ray(transform.position, -Vector3.up);
		Ray walkDirectionRay = new Ray(transform.position, transform.forward);
		
		Debug.DrawRay(downRay.origin, downRay.direction, Color.magenta, m_characterController.height/2 +1f);
		
		// CHECKINGDOWN
		if (Physics.Raycast(downRay.origin, downRay.direction, out hit, m_characterController.height/2 +1f)) 
		{  
			m_groundHitAngle = Vector3.Angle(m_groundHit.normal, Vector3.up);
			m_groundHit = hit;
			m_isGroundedRaycast = true;
			m_inAir = false;
		}else{
			m_isGroundedRaycast = false;
			m_inAir = true;
		}
		//CHECKING FRONT
		if (Physics.Raycast(walkDirectionRay.origin, walkDirectionRay.direction, out hit, m_characterController.height/2 + 2))
		{		
			m_frontHit = hit;
		}
	}
	private void ExecuteMovement()
	{
		m_isGrounded = m_characterController.isGrounded;
		
		if(m_moving)
			this.transform.eulerAngles = new Vector3(transform.eulerAngles.x, m_camera.transform.eulerAngles.y, transform.eulerAngles.z);

		if(m_isGroundedRaycast)
		{
			if(m_jumping)m_jumping = false;
			
			m_moveDirection = new Vector3(m_smoothedHorizontalInput, m_moveDirection.y , m_smoothedVerticalInput);
			m_moveDirection = Vector3.ClampMagnitude(m_moveDirection, 1);
			
			if(Mathf.Abs(m_horizontalInput) == 1f && Mathf.Abs(m_verticalInput) == 1f)
			{	
				m_moveDirection.Normalize();
			}
			m_moveDirection = transform.TransformDirection(m_moveDirection);
			m_moveDirection *= m_speed;
			
			if(InputManager.Instance.AButtonDown())
			{				
				m_moveDirection.y = m_jumpSpeed;
				m_jumping = true;
			}
		}
		else
		{
			m_moveDirection = new Vector3(m_horizontalInput, m_moveDirection.y , m_verticalInput);
			
			m_moveDirection = transform.TransformDirection(m_moveDirection);
			m_moveDirection.x *= m_speed;
			m_moveDirection.z *= m_speed;
		}
		
		if(m_isGroundedRaycast && m_groundHitAngle > 20 && !m_jumping)
		{
			m_moveDirection[1] = -20.0f; 
		}
		
		m_moveDirection.y -= m_gravity * Time.deltaTime; 
		m_characterController.Move(m_moveDirection * Time.deltaTime);
		m_cameraScript.ExecuteMovement();
		m_lookDirection = m_moveDirection;
		
		if(m_moving)
		{			
			transform.rotation = Quaternion.LookRotation(new Vector3(m_lookDirection.x,0,m_lookDirection.z)*Time.deltaTime, Vector3.up);
		}
	}
	
	private void ExecuteAnimations()
	{
		m_animator.SetFloat("Speed",m_combinedSpeed);
		m_animator.SetFloat("CharacterSpeed", ((m_speed/10)*m_combinedSpeed)*0.8f);
		m_ik.ikWeight = m_inAir ? 0 : 1; 
		m_animator.SetBool("InAir", m_inAir);
	}
}
*/


























