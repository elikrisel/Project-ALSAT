﻿using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// hinput class representing a gamepad.
/// </summary>
public class hGamepad {
	// --------------------
	// ID
	// --------------------

	/// <summary>
	/// Returns the real index of a gamepad in the gamepad list of hinput.
	/// </summary>
	/// <remarks>
	/// If this is anyGamepad, returns -1.
	/// </remarks>
	public readonly int internalIndex;

	/// <summary>
	/// Returns the index of a gamepad in the gamepad list of hinput.
	/// </summary>
	/// <remarks>
	/// If this is anyGamepad, returns the index of the gamepad that is currently being pressed.
	/// </remarks>
	public virtual int index { get { return internalIndex; } }

	/// <summary>
	/// Returns the real name of a gamepad, like "Gamepad1".
	/// </summary>
	/// <remarks>
	/// If this is anyGamepad, returns "AnyGamepad".
	/// </remarks>
	public readonly string internalName;
	
	/// <summary>
	/// Returns the name of a gamepad, like "Gamepad1".
	/// </summary>
	/// <remarks>
	/// If this is anyGamepad, returns the name of the gamepad that is currently being pressed.
	/// </remarks>
	public virtual string name { get { return internalName; } }

	/// <summary>
	/// Returns the real full name of a gamepad, like "Windows_Gamepad4".
	/// </summary>
	/// <remarks>
	/// If this is anyGamepad, returns the full name of anyGamepad, like "Windows_AnyGamepad".
	/// </remarks>
	public readonly string internalFullName;

	/// <summary>
	/// Returns the full name of a gamepad, like "Windows_Gamepad4".
	/// </summary>
	/// <remarks>
	/// If this is anyGamepad, returns the full name of the gamepad that is currently being pressed.
	/// </remarks>
	public virtual string fullName { get { return internalFullName; } }
	
	/// <summary>
	/// Returns the type of a gamepad, like "Xbox One For Windows".
	/// </summary>
	/// <remarks>
	/// If this is anyGamepad, returns the type of the gamepad that is currently being pressed.
	/// </remarks>
	public virtual string type {
		get {
			if (Input.GetJoystickNames().Length <= index) return "";
			return Input.GetJoystickNames()[index];
		}
	}
	
	/// <summary>
	/// Returns true if a gamepad is currently connected. Returns false otherwise.
	/// </summary>
	/// <remarks>
	/// If this is anyGamepad, returns true if a gamepad is connected. Returns false otherwise.
	/// </remarks>
	public virtual bool isConnected { get { return type != ""; } }


	// --------------------
	// CONSTRUCTOR
	// --------------------

	public hGamepad (int index) {
		internalIndex = index;
		vibration = new hVibration (index);

		if (index == -1) internalName = "AnyGamepad";
		else internalName = "Gamepad" + index;
		
		internalFullName = hUtils.os + "_" + internalName;
	}

	
	// --------------------
	// BUILD
	// --------------------

	public void BuildAll () {
		if (A.gamepadIndex == 0
		|| B.gamepadIndex == 0
		|| X.gamepadIndex == 0
		|| Y.gamepadIndex == 0
		|| leftBumper.gamepadIndex == 0
		|| rightBumper.gamepadIndex == 0
		|| back.gamepadIndex == 0
		|| start.gamepadIndex == 0
		|| leftStickClick.gamepadIndex == 0
		|| rightStickClick.gamepadIndex == 0
		|| xBoxButton.gamepadIndex == 0
		|| leftTrigger.gamepadIndex == 0
		|| rightTrigger.gamepadIndex == 0
		|| anyInput.gamepadIndex == 0) {
			// Do nothing, I'm just looking them up so that they are assigned.
		}
		
		leftStick.BuildDirections ();
		rightStick.BuildDirections ();
		dPad.BuildDirections ();
	}

	
	// --------------------
	// UPDATE
	// --------------------

	public void Update () {
		vibration.Update ();

		if (_A != null) _A.Update();
		if (_B != null) _B.Update();
		if (_X != null) _X.Update();
		if (_Y != null) _Y.Update();

		if (_leftBumper != null) _leftBumper.Update();
		if (_rightBumper != null) _rightBumper.Update();

		if (_back != null) _back.Update();
		if (_start != null) _start.Update();

		if (_leftStickClick != null) _leftStickClick.Update();
		if (_rightStickClick != null) _rightStickClick.Update();

		if (_xBoxButton != null) _xBoxButton.Update();


		if (_leftTrigger != null) _leftTrigger.Update();
		if (_rightTrigger != null) _rightTrigger.Update();


		if (_leftStick != null) _leftStick.Update();
		if (_rightStick != null) _rightStick.Update();


		if (_dPad != null) _dPad.Update();
		
		anyInput.Update();
	}

	
	// --------------------
	// PUBLIC PROPERTIES
	// --------------------

	private hButton _A;
	/// <summary>
	/// The A button of a gamepad.
	/// </summary>
	public hButton A { 
		get {
			if (_A == null) _A = new hButton ("A", this, 0);
			return _A; 
		} 
	}

	private hButton _B;
	/// <summary>
	/// The B button of a gamepad.
	/// </summary>
	public hButton B { 
		get {
			if (_B == null) _B = new hButton ("B", this, 1);
			return _B; 
		} 
	}

	private hButton _X;
	/// <summary>
	/// The X button of a gamepad.
	/// </summary>
	public hButton X { 
		get {
			if (_X == null) _X = new hButton ("X", this, 2);
			return _X; 
		} 
	}

	private hButton _Y;
	/// <summary>
	/// The Y button of a gamepad.
	/// </summary>
	public hButton Y { 
		get {
			if (_Y == null) _Y = new hButton ("Y", this, 3);
			return _Y; 
		} 
	}


	private hButton _leftBumper;
	/// <summary>
	/// The left bumper of a gamepad.
	/// </summary>
	public hButton leftBumper { 
		get {
			if (_leftBumper == null) _leftBumper = new hButton ("LeftBumper", this, 4);
			return _leftBumper; 
		} 
	}

	private hButton _rightBumper;
	/// <summary>
	/// The right bumper of a gamepad.
	/// </summary>
	public hButton rightBumper { 
		get {
			if (_rightBumper == null) _rightBumper = new hButton ("RightBumper", this, 5);
			return _rightBumper; 
		} 
	}

	private hButton _back;
	/// <summary>
	/// The back button of a gamepad.
	/// </summary>
	public hButton back { 
		get {
			if (_back == null) _back = new hButton ("Back", this, 8);
			return _back; 
		} 
	}

	private hButton _start;
	/// <summary>
	/// The start button of a gamepad.
	/// </summary>
	public hButton start { 
		get {
			if (_start == null) _start = new hButton ("Start", this, 9);
			return _start; 
		} 
	}

	private hButton _leftStickClick;
	/// <summary>
	/// The left stick click of a gamepad.
	/// </summary>
	public hButton leftStickClick { 
		get {
			if (_leftStickClick == null) _leftStickClick = new hButton ("LeftStickClick", this, 10);
			return _leftStickClick; 
		} 
	}

	private hButton _rightStickClick;
	/// <summary>
	/// The right stick click of a gamepad.
	/// </summary>
	public hButton rightStickClick { 
		get {
			if (_rightStickClick == null) _rightStickClick = new hButton ("RightStickClick", this, 11);
			return _rightStickClick; 
		} 
	}

	private hButton _xBoxButton;
	/// <summary>
	/// The xBox button of a gamepad.<br/>
	/// Windows and Linux drivers can’t detect the value of this button. 
	/// Therefore it will be considered released at all times on these operating systems.
	/// </summary>
	public hButton xBoxButton { 
		get {
			if (_xBoxButton == null) _xBoxButton = new hButton ("XBoxButton", this, 12);
			return _xBoxButton; 
		} 
	}

	private hTrigger _leftTrigger;
	/// <summary>
	/// The left trigger of a gamepad.
	/// </summary>
	public hTrigger leftTrigger { 
		get {
			if (_leftTrigger == null) _leftTrigger = new hTrigger ("LeftTrigger", this, 6);
			return _leftTrigger; 
		} 
	}
	
	private hTrigger _rightTrigger;
	/// <summary>
	/// The right trigger of a gamepad.
	/// </summary>
	public hTrigger rightTrigger { 
		get {
			if (_rightTrigger == null) _rightTrigger = new hTrigger ("RightTrigger", this, 7);
			return _rightTrigger; 
		} 
	}

	protected hStick _leftStick;
	/// <summary>
	/// The left stick of a gamepad.
	/// </summary>
	public virtual hStick leftStick { 
		get {
			if (_leftStick == null) _leftStick = new hStick ("LeftStick", this, 0);
			return _leftStick; 
		} 
	}

	protected hStick _rightStick;
	/// <summary>
	/// The right stick click of a gamepad.
	/// </summary>
	public virtual hStick rightStick { 
		get {
			if (_rightStick == null) _rightStick = new hStick ("RightStick", this, 1);
			return _rightStick; 
		} 
	}
	
	protected hStick _dPad;
	/// <summary>
	/// The D-pad of a gamepad.
	/// </summary>
	public virtual hStick dPad { 
		get {
			if (_dPad == null) _dPad = new hStick ("DPad", this, 2);
			return _dPad; 
		} 
	}

	private List<hStick> _sticks;
	/// <summary>
	/// The list containing a gamepad’s sticks, in the following order : { leftStick, rightStick, dPad }
	/// </summary>
	public List<hStick> sticks { 
		get {
			if (_sticks == null) _sticks = new List<hStick> { leftStick, rightStick, dPad };
			return _sticks;
		}
	}

	private List<hPressable> _buttons;
	/// <summary>
	/// The list containing a gamepad’s buttons, in the following order : { A, B, X, Y, left bumper, right bumper, left
	/// trigger, right trigger, back, start, left stick click, right stick click, XBox button }
	/// </summary>
	public List<hPressable> buttons { 
		get {
			if (_buttons == null) _buttons = new List<hPressable> {
				A, B, X, Y,
				leftBumper, rightBumper, leftTrigger, rightTrigger,
				back, start, leftStickClick, rightStickClick, xBoxButton
			};
			return _buttons;
		}
	}

	private hAnyInput _anyInput;
	/// <summary>
	/// A virtual button that returns every input of a gamepad at once.
	/// It has the name and full name of the input that is currently being pushed (except if you use "internal"
	/// properties).
	/// </summary>
	public hAnyInput anyInput {
		get {
			if (_anyInput == null) _anyInput = new hAnyInput("AnyInput", this);
			return _anyInput;
		}
	}

	// --------------------
	// VIBRATION
	// --------------------
	
	private readonly hVibration vibration;
	
	/// <summary>
	/// Vibrate a gamepad. Default duration and intensity can be tweaked in settings.
	/// </summary>
	public void Vibrate() {
		vibration.Vibrate(
			hSettings.leftVibrationIntensity, 
			hSettings.rightVibrationIntensity, 
			hSettings.vibrationDuration);
	}

	/// <summary>
	/// Vibrate a gamepad for duration seconds. Default intensity can be tweaked in settings. 
	/// </summary>
	public void Vibrate(float duration) {
		vibration.Vibrate(
			hSettings.leftVibrationIntensity, 
			hSettings.rightVibrationIntensity, 
			duration);
	}
	
	/// <summary>
	/// Vibrate a gamepad with an instensity of leftIntensity on the left motor, and an intensity of rightIntensity on
	/// the right motor. Default duration can be tweaked in settings.
	/// </summary>
	public void Vibrate(float leftIntensity, float rightIntensity) {
		vibration.Vibrate(
			leftIntensity, 
			rightIntensity, 
			hSettings.vibrationDuration);
	}
	
	/// <summary>
	/// Vibrate a gamepad for duration seconds with an instensity of leftIntensity on the left motor, and an intensity
	/// of rightIntensity on the right motor.
	/// </summary>
	public void Vibrate(float leftIntensity, float rightIntensity, float duration) {
		vibration.Vibrate(
			leftIntensity, 
			rightIntensity, 
			duration);
	}
	
	/// <summary>
	/// Vibrate a gamepad with an intensity over time based on an animation curve.
	/// </summary>
	public void Vibrate(AnimationCurve curve) {
		vibration.Vibrate(curve, curve);
	}
	
	/// <summary>
	/// Vibrate a gamepad with an intensity over time based on two animation curves, one for the left side and one for
	/// the right side.
	/// </summary>
	public void Vibrate(AnimationCurve leftCurve, AnimationCurve rightCurve) {
		vibration.Vibrate(leftCurve, rightCurve);
	}

	/// <summary>
	/// Vibrate a gamepad with an intensity and a duration based on a vibration preset.
	/// </summary>
	public void Vibrate(hVibrationPreset vibrationPreset) {
		vibration.Vibrate(vibrationPreset, 1, 1, 1);
	}

	/// <summary>
	/// Vibrate a gamepad with an intensity and a duration based on a vibration preset.
	/// The duration of the preset is multiplied by duration.
	/// </summary>
	public void Vibrate(hVibrationPreset vibrationPreset, float duration) {
		vibration.Vibrate(vibrationPreset, 1, 1, duration);
	}

	/// <summary>
	/// Vibrate a gamepad with an intensity and a duration based on a vibration preset.
	/// The left intensity of the preset is multiplied by leftIntensity, and its right intensity is multiplied by
	/// rightIntensity.
	/// </summary>
	public void Vibrate(hVibrationPreset vibrationPreset, float leftIntensity, float rightIntensity) {
		vibration.Vibrate(vibrationPreset, leftIntensity, rightIntensity, 1);
	}

	/// <summary>
	/// Vibrate a gamepad with an intensity and a duration based on a vibration preset.
	/// The left intensity of the preset is multiplied by leftIntensity, its right intensity is multiplied by
	/// rightIntensity, and its duration is multiplied by duration.
	/// </summary>
	public void Vibrate(hVibrationPreset vibrationPreset, float leftIntensity, float rightIntensity, float duration) {
		vibration.Vibrate(vibrationPreset, leftIntensity, rightIntensity, duration);
	}
	
	/// <summary>
	/// Vibrate a gamepad with an instensity of leftIntensity on the left motor, and an intensity of rightIntensity on
	/// the right motor, FOREVER. Don't forget to call StopVibration !
	/// </summary>
	public void VibrateAdvanced(float leftIntensity, float rightIntensity) {
		vibration.VibrateAdvanced(leftIntensity, rightIntensity);
	}

	/// <summary>
	/// Stop all vibrations on a gamepad immediately.
	/// </summary>
	public void StopVibration () {
		vibration.StopVibration(0);
	}

	/// <summary>
	/// Stop all vibrations on a gamepad progressively over duration seconds.
	/// </summary>
	public void StopVibration (float duration) {
		vibration.StopVibration(duration);
	}

	/// <summary>
	/// The intensity at which the left motor of a gamepad is currently vibrating.
	/// </summary>
	public float leftVibration { get { return vibration.currentLeft; } }

	/// <summary>
	/// The intensity at which the right motor of a gamepad is currently vibrating.
	/// </summary>
	public float rightVibration { get { return vibration.currentRight; } }
}