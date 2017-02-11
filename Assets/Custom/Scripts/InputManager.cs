using UnityEngine;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;

class InputManagerEditor : EditorWindow 
{[MenuItem ("InputManager/CreateMapping")]
		
	public static void  ShowWindow () 
	{
		SetUnityInputManager();
		Debug.Log("Creating InputMappings");
	}

	public class InputAxis
	{
		public string name;
		public string descriptiveName;
		public string descriptiveNegativeName;
		public string negativeButton;
		public string positiveButton;
		public string altNegativeButton;
		public string altPositiveButton;
		
		public float gravity;
		public float dead;
		public float sensitivity;
		
		public bool snap = false;
		public bool invert = false;
		
		public AxisType type;
		
		public int axis;
		public int joyNum;
	}

	public enum AxisType
	{
		KeyOrMouseButton = 0,
		MouseMovement = 1,
		JoystickAxis = 2
	};

	private static SerializedProperty GetChildProperty(SerializedProperty parent, string name)
	{
		SerializedProperty child = parent.Copy();
		child.Next(true);
		do
		{
			if (child.name == name) return child;
		}
		while (child.Next(false));
		return null;
	}
	
	private static void AddAxis(InputAxis axis)
	{
		SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
		SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");
		
		axesProperty.arraySize++;
		serializedObject.ApplyModifiedProperties();
		
		SerializedProperty axisProperty = axesProperty.GetArrayElementAtIndex(axesProperty.arraySize - 1);
		
		GetChildProperty(axisProperty, "m_Name").stringValue = axis.name;
		GetChildProperty(axisProperty, "descriptiveName").stringValue = axis.descriptiveName;
		GetChildProperty(axisProperty, "descriptiveNegativeName").stringValue = axis.descriptiveNegativeName;
		GetChildProperty(axisProperty, "negativeButton").stringValue = axis.negativeButton;
		GetChildProperty(axisProperty, "positiveButton").stringValue = axis.positiveButton;
		GetChildProperty(axisProperty, "altNegativeButton").stringValue = axis.altNegativeButton;
		GetChildProperty(axisProperty, "altPositiveButton").stringValue = axis.altPositiveButton;
		GetChildProperty(axisProperty, "gravity").floatValue = axis.gravity;
		GetChildProperty(axisProperty, "dead").floatValue = axis.dead;
		GetChildProperty(axisProperty, "sensitivity").floatValue = axis.sensitivity;
		GetChildProperty(axisProperty, "snap").boolValue = axis.snap;
		GetChildProperty(axisProperty, "invert").boolValue = axis.invert;
		GetChildProperty(axisProperty, "type").intValue = (int)axis.type;
		GetChildProperty(axisProperty, "axis").intValue = axis.axis - 1;
		GetChildProperty(axisProperty, "joyNum").intValue = axis.joyNum;
		
		serializedObject.ApplyModifiedProperties();
	}
	
	private static void SetUnityInputManager()
	{
		SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
		SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");
		axesProperty.ClearArray();
		serializedObject.ApplyModifiedProperties();

		AddAxis(new InputAxis() { name = "Mouse X",  gravity = 0.5f, dead = 0.4f, sensitivity = 1f, type = AxisType.MouseMovement, axis = 1,});

		//Right
		AddAxis(new InputAxis() { name = "VerticalStickRightPC",  gravity = 0.5f, dead = 0.4f, sensitivity = 1f, type = AxisType.JoystickAxis, axis = 5,});
		AddAxis(new InputAxis() { name = "HorizontalStickRightPC", gravity = 0.5f, dead = 0.4f, sensitivity = 1f, type = AxisType.JoystickAxis, axis = 4,});
		//Left
		AddAxis(new InputAxis() { name = "VerticalStickLeftPC", gravity = 0.5f,  invert = true, dead = 0.4f, sensitivity = 1f, type = AxisType.JoystickAxis, axis = 2,});
		AddAxis(new InputAxis() { name = "HorizontalStickLeftPC", gravity = 0.5f, dead = 0.4f, sensitivity = 1f, type = AxisType.JoystickAxis, axis = 1,});
		
		//shoulder trigger
		AddAxis(new InputAxis() { name = "TriggerLeftPC", gravity = 0.5f, dead = 0.4f, sensitivity = 1f, type = AxisType.JoystickAxis, axis = 3,});
		AddAxis(new InputAxis() { name = "TriggerRightPC", gravity = 0.5f, dead = 0.4f, sensitivity = 1f,invert = true, type = AxisType.JoystickAxis, axis = 6,});
		
		//jump
		AddAxis(new InputAxis() { name = "A",descriptiveName = "A", positiveButton = "joystick 1 button 0",  type = AxisType.KeyOrMouseButton, axis = 1,});
		
		//Change Modes
		AddAxis(new InputAxis() { name = "B",descriptiveName = "B", positiveButton = "joystick 1 button 1",  type = AxisType.KeyOrMouseButton, axis = 1,});
		
		AddAxis(new InputAxis() { name = "LeftBumperPC",descriptiveName = "LeftBumperPC", positiveButton = "joystick 1 button 4", type = AxisType.KeyOrMouseButton, axis = 1,});
		AddAxis(new InputAxis() { name = "RightBumperPC",descriptiveName = "RightBumperPC", positiveButton = "joystick 1 button 5", type = AxisType.KeyOrMouseButton, axis = 1,});
	}
}
#endif



public class InputManager : MonoBehaviour
{
	public int number;

	private static InputManager m_instance;

	public string HLeft
	{
		get {return InputMapping [EInput.hLeft];}
	}
	public string VLeft
	{
		get {return InputMapping [EInput.vLeft];}
	}
	public string HRight
	{
		get {return InputMapping [EInput.hRight];}
	}
	public string VRight
	{
		get {return InputMapping [EInput.vRight];}
	}
	public string TriggerLeft
	{
		get {return InputMapping [EInput.lTrigger];}
	}
	public string TriggerRight
	{
		get {return InputMapping [EInput.rTrigger];}
	}
	public bool AButtonDown()
	{
		return Input.GetButtonDown(InputMapping [EInput.aButton]);
	}

	public bool BButtonPressed()
	{
		return Input.GetButton(InputMapping [EInput.bButton]);
	}

	public bool LeftBumper
	{
		get{ return Input.GetButton (InputMapping [EInput.lBumper]);}
	}
	public bool RightBumper
	{
		get{ return Input.GetButton (InputMapping [EInput.rBumper]);}
	}

	public bool BButtonUp()
	{
		return Input.GetButtonUp(InputMapping [EInput.bButton]);
	}
	public bool BButtonDown
	{
		get{return Input.GetButtonDown(InputMapping [EInput.bButton]);}
	}

	public enum EInput
	{
		hLeft,
		vLeft,

		hRight,
		vRight,

		aButton,
		bButton,

		rTrigger,
		lTrigger,

		lBumper,
		rBumper,
	}

	private Dictionary<EInput,string> InputMapping = new Dictionary<EInput,string>();
	 
	public static InputManager Instance
	{
		get 
		{ 
			if(m_instance == null)
			{
				m_instance = new GameObject("Singleton").AddComponent<InputManager>(); 
				Instance.Initialize();
			}
			return m_instance;
		}
	}
	private void Initialize()
	{
		if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer) 
		{
			InputMapping.Add (EInput.hLeft, "HorizontalStickLeftPC");
			InputMapping.Add (EInput.vLeft, "VerticalStickLeftPC");
			InputMapping.Add (EInput.hRight, "HorizontalStickRightPC");
			InputMapping.Add (EInput.vRight, "VerticalStickRightPC");
			InputMapping.Add (EInput.aButton, "A");
			InputMapping.Add (EInput.bButton, "B");
			InputMapping.Add (EInput.rTrigger, "TriggerRightPC");
			InputMapping.Add (EInput.lTrigger, "TriggerLeftPC");
			InputMapping.Add (EInput.lBumper, "LeftBumperPC");
			InputMapping.Add (EInput.rBumper, "RightBumperPC");
		}
		if (Application.platform == RuntimePlatform.Android)
		{
			InputMapping.Add (EInput.hLeft, "HorizontalStickLeftPC");
			InputMapping.Add (EInput.vLeft, "VerticalStickLeftPC");
			InputMapping.Add (EInput.hRight, "HorizontalStickRightPC");
			InputMapping.Add (EInput.vRight, "VerticalStickRightPC");
			InputMapping.Add (EInput.aButton, "A");
			InputMapping.Add (EInput.bButton, "B");
			InputMapping.Add (EInput.rTrigger, "TriggerRightPC");
			InputMapping.Add (EInput.lTrigger, "TriggerLeftPC"); 
		}
	}

	public string Get(EInput _enum)
	{
		return InputMapping [_enum];
	}
}









