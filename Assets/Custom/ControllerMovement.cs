using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMovement : MonoBehaviour {

	private float m_horizontalInput;
	private float m_verticalInput;

	// Update is called once per frame
	void Update () {
		Move();
	}

	private void Move() {
		
		m_horizontalInput = Input.GetAxis(InputManager.Instance.Get(InputManager.EInput.hLeft));
		m_verticalInput = Input.GetAxis(InputManager.Instance.Get(InputManager.EInput.vLeft));


		this.transform.Translate(m_horizontalInput, 0, m_verticalInput);

		if(InputManager.Instance.AButtonDown())
		{				
			this.transform.Translate(this.transform.forward*5);
		}
	}
}
