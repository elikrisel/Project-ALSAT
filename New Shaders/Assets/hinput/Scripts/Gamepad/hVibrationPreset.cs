﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//hinput enum listing some vibration patterns that can be played on a gamepad.
public enum hVibrationPreset {
    /// <summary>
    /// A short vibration, suitable for feedback after the player pressed a button.
    /// Similar to Vibrate(0.5f, 0.5f, 0.1f).
    /// </summary>
    ButtonPress,
    
    /// <summary>
    /// A short and intense vibration, suitable for a light impact.
    /// Similar to Vibrate(0f, 0.5f, 0.2f).
    /// </summary>
    ImpactLight,
    
    /// <summary>
    /// A short and intense vibration, suitable for an impact.
    /// Similar to Vibrate(0.2f, 0.8f, 0.2f).
    /// </summary>
    Impact,
    
    /// <summary>
    /// A short and intense vibration, suitable for a heavy impact.
    /// Similar to Vibrate(0.5f, 1f, 0.2f).
    /// </summary>
    ImpactHeavy,
    
    /// <summary>
    /// A low and powerful vibration, suitable for a short or distant explosion.
    /// Similar to Vibrate(0.5f, 0.25f, 0.2f).
    /// </summary>
    ExplosionShort,
    
    /// <summary>
    /// A low and powerful vibration, suitable for an explosion.
    /// Similar to Vibrate(0.8f, 0.4f, 0.5f).
    /// </summary>
    Explosion,
    
    /// <summary>
    /// A low and powerful vibration, suitable for a long or nearby explosion.
    /// Similar to Vibrate(1f, 0.5f, 1f).
    /// </summary>
    ExplosionLong,
    
    /// <summary>
    /// A 10-second constant, low and subtle vibration, suitable for an ongoing event.
    /// Similar to Vibrate(0.1f, 0f, 10f).
    /// </summary>
    AmbientSubtle,
    
    /// <summary>
    /// A 10-second constant, low vibration, suitable for an ongoing event.
    /// Similar to Vibrate(0.3f, 0.1f, 10f).
    /// </summary>
    Ambient,
    
    /// <summary>
    /// A 10-second constant, low and strong vibration, suitable for an ongoing event.
    /// Similar to Vibrate(0.6f, 0.3f, 10f).
    /// </summary>
    AmbientStrong
}
