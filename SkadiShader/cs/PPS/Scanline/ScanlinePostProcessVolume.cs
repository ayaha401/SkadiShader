using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System;

[Serializable]
[VolumeComponentMenu("Scanline Effect")]
public class ScanlinePostProcessVolume : VolumeComponent
{
    public ClampedFloatParameter distortPower = new ClampedFloatParameter(0f, 0f, 1f);
    public ClampedFloatParameter brightness = new ClampedFloatParameter(0f, 0f, 1f);
    public IntParameter lineAmount = new IntParameter(0);
    public IntParameter lineMoveSpeed = new IntParameter(0);
    public BoolParameter lineNonBlur = new BoolParameter(false);
    public BoolParameter useVignette = new BoolParameter(false);
    public BoolParameter useGlitch = new BoolParameter(false);

    public bool IsActive() => lineAmount.value > 0f;
}
