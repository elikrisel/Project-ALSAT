using System.Collections;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
	using System.Collections.Generic;
	using XInputDotNetPure;
#endif

public class hVibration {
	// --------------------
	// PRIVATE VARIABLES
	// --------------------

	#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
		private readonly List<PlayerIndex> index;
	#endif
	private readonly bool canVibrate = false;
	public float currentLeft;
	public float currentRight;
	private float prevLeft;
	private float prevRight;


	// --------------------
	// CONSTRUCTOR
	// --------------------

    public hVibration (int index) {
	    if (hUtils.os != "Windows") return;
	    if (index > 3) return;
	    
		
		#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
		    if (index == 0) this.index = new List<PlayerIndex> { PlayerIndex.One };
		    else if (index == 1) this.index = new List<PlayerIndex> { PlayerIndex.Two };
			else if (index == 2) this.index = new List<PlayerIndex> { PlayerIndex.Three };
		    else if (index == 3) this.index = new List<PlayerIndex> { PlayerIndex.Four };
		    else if (index == -1) this.index = new List<PlayerIndex> {
			    PlayerIndex.One, 
			    PlayerIndex.Two, 
			    PlayerIndex.Three, 
			    PlayerIndex.Four
		    };
		    
		    canVibrate = true;
		#endif
	    
    }


	// --------------------
	// UPDATE
	// --------------------

	public void Update () {
		if (currentLeft.IsEqualTo(prevLeft) && currentRight.IsEqualTo(prevRight)) return;
		
		prevLeft = currentLeft;
		prevRight = currentRight;
		
		#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
			try { index.ForEach(playerIndex => GamePad.SetVibration(playerIndex, currentLeft, currentRight)); } 
			catch { /*Ignore errors here*/ }
		#endif
	}


	// --------------------
	// PUBLIC METHODS
	// --------------------

	public void Vibrate (float left, float right, float duration) {
		hUtils.Coroutine(_Vibrate(left, right, duration));
	}

	public void Vibrate(AnimationCurve leftCurve, AnimationCurve rightCurve) {
		hUtils.Coroutine(_Vibrate(leftCurve, rightCurve));
	}

	public void Vibrate(hVibrationPreset vibrationPreset, float left, float right, float duration) {
		PresetCurves presetCurves = GetCurves(vibrationPreset, left, right, duration);
		hUtils.Coroutine(_Vibrate(presetCurves.leftCurve, presetCurves.rightCurve));
	}

	public void VibrateAdvanced (float left, float right) {
		currentLeft += left;
		currentRight += right;
	}

	public void StopVibration(float duration) {
		hUtils.StopRoutines();
		hUtils.Coroutine(_StopVibration(duration));
	}


	// --------------------
	// PRIVATE METHODS
	// --------------------

	private IEnumerator _Vibrate (float left, float right, float duration) {
		if (canVibrate) {
			currentRight += right;
			currentLeft += left;

			yield return new WaitForSecondsRealtime (duration);

			currentRight -= right;
			currentLeft -= left;
		} else {
			if (hUtils.os != "Windows") {
				Debug.LogWarning("hinput warning : vibration is only supported on Windows computers.");
			} else {
				Debug.LogWarning("hinput warning : vibration is only supported on four controllers.");
			}
		}
	}

	private IEnumerator _Vibrate(AnimationCurve leftCurve, AnimationCurve rightCurve) {
		float time = 0;
		bool leftCurveIsOver = (leftCurve.keys.Length == 0);
		bool rightCurveIsOver = (rightCurve.keys.Length == 0);

		while(!leftCurveIsOver || !rightCurveIsOver) {
			float leftCurveValue;
			if (leftCurveIsOver) leftCurveValue = 0;
			else leftCurveValue = leftCurve.Evaluate(time);

			float rightCurveValue;
			if (rightCurveIsOver) rightCurveValue = 0;
			else rightCurveValue = rightCurve.Evaluate(time);

			time += Time.unscaledDeltaTime;
			leftCurveIsOver = (time > leftCurve.keys.Last().time);
			rightCurveIsOver = (time > rightCurve.keys.Last().time);
			
			currentLeft += leftCurveValue;
			currentRight += rightCurveValue;
			yield return new WaitForEndOfFrame();
			currentLeft -= leftCurveValue;
			currentRight -= rightCurveValue;
		}

		yield return null;
	}

	private IEnumerator _StopVibration(float duration) {
		float originLeft = currentLeft;
		float originRight = currentRight;
		
		currentLeft = 0;
		currentRight = 0;

		float timeLeft = duration;
		while (timeLeft > 0) {
			timeLeft -= Time.unscaledDeltaTime;

			currentLeft += timeLeft / duration * originLeft;
			currentRight += timeLeft / duration * originRight;
			yield return new WaitForEndOfFrame();
			currentLeft -= timeLeft / duration * originLeft;
			currentRight -= timeLeft / duration * originRight;
		}
	}


	// --------------------
	// VIBRATION PRESETS
	// --------------------

	private struct PresetCurves {
		public readonly AnimationCurve leftCurve;
		public readonly AnimationCurve rightCurve;

		public PresetCurves(AnimationCurve left, AnimationCurve right) {
			leftCurve = left;
			rightCurve = right;
		}
	}

	private PresetCurves GetCurves(hVibrationPreset vibrationPreset, float left, float right, float duration) {
		AnimationCurve leftCurve = new AnimationCurve();
		AnimationCurve rightCurve = new AnimationCurve();
		if (vibrationPreset == hVibrationPreset.ButtonPress) {
			leftCurve.AddKey(0, 0.5f);
			leftCurve.AddKey(0.1f, 0.5f);
			
			rightCurve.AddKey(0, 0.5f);
			rightCurve.AddKey(0.1f, 0.5f);
		}
		
		if (vibrationPreset == hVibrationPreset.ImpactLight) {
			leftCurve.AddKey(0, 0f);
			leftCurve.AddKey(0.2f, 0f);
			
			rightCurve.AddKey(0, 0.5f);
			rightCurve.AddKey(0.2f, 0.5f);
		}
		
		if (vibrationPreset == hVibrationPreset.Impact) {
			leftCurve.AddKey(0, 0.2f);
			leftCurve.AddKey(0.2f, 0.2f);
			
			rightCurve.AddKey(0, 0.8f);
			rightCurve.AddKey(0.2f, 0.8f);
		}
		
		if (vibrationPreset == hVibrationPreset.ImpactHeavy) {
			leftCurve.AddKey(0, 0.5f);
			leftCurve.AddKey(0.2f, 0.5f);
			
			rightCurve.AddKey(0, 1);
			rightCurve.AddKey(0.2f, 1);
		}
		
		if (vibrationPreset == hVibrationPreset.ExplosionShort) {
			leftCurve.AddKey(0, 0.5f);
			leftCurve.AddKey(0.2f, 0.5f);
			
			rightCurve.AddKey(0, 0.25f);
			rightCurve.AddKey(0.2f, 0.25f);
		}
		
		if (vibrationPreset == hVibrationPreset.Explosion) {
			leftCurve.AddKey(0, 0.8f);
			leftCurve.AddKey(0.5f, 0.8f);
			
			rightCurve.AddKey(0, 0.4f);
			rightCurve.AddKey(0.5f, 0.4f);
		}
		
		if (vibrationPreset == hVibrationPreset.ExplosionLong) {
			leftCurve.AddKey(0, 1f);
			leftCurve.AddKey(1f, 1f);
			leftCurve.AddKey(1.1f, 0f);
			
			rightCurve.AddKey(0, 0.5f);
			rightCurve.AddKey(1f, 0.5f);
			rightCurve.AddKey(1.1f, 0f);
		}
		
		if (vibrationPreset == hVibrationPreset.AmbientSubtle) {
			leftCurve.AddKey(0, 0.1f);
			leftCurve.AddKey(10f, 0.1f);
			
			rightCurve.AddKey(0, 0);
			rightCurve.AddKey(10f, 0);
		}
		
		if (vibrationPreset == hVibrationPreset.Ambient) {
			leftCurve.AddKey(0, 0.3f);
			leftCurve.AddKey(10f, 0.3f);
			
			rightCurve.AddKey(0, 0.1f);
			rightCurve.AddKey(10f, 0.1f);
		}
		
		if (vibrationPreset == hVibrationPreset.AmbientStrong) {
			leftCurve.AddKey(0, 0.6f);
			leftCurve.AddKey(10f, 0.6f);
			
			rightCurve.AddKey(0, 0.3f);
			rightCurve.AddKey(10f, 0.3f);
		}

		leftCurve.keys = leftCurve.keys
			.ToList()
			.Select(key => new Keyframe(key.time * duration, key.value * left))
			.ToArray();
		
		rightCurve.keys = rightCurve.keys
			.ToList()
			.Select(key => new Keyframe(key.time * duration, key.value * right))
			.ToArray();

		return new PresetCurves(leftCurve, rightCurve);
	}
}