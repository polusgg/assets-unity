using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class AnimationRipper {
    
    /// Awful animation ripping code
    /// I wrote this only to make this work
    /// This is entirely test code
    /// Rewrite and improve it if you want, but know that there is literally no reason to do so at the moment
    /// -Sussus Amongus (Sanae)
    
    [MenuItem("Rip/Rip Animation")]
    public static void RipAnimation() {
        AnimationClip[] clips = Selection.GetFiltered<AnimationClip>(SelectionMode.Assets);
        if (clips.Length != 1)
            throw new Exception("ONLY SELECT ONE ANIMATION PLEASE AND THAnk you");
        AnimationClip clip = clips[0];
        EditorCurveBinding[] cub = AnimationUtility.GetCurveBindings(clip);
        AnimationWoozy woozy = new AnimationWoozy();
        woozy.Curves = cub
            .Select(binding => new {binding, curve = AnimationUtility.GetEditorCurve(clip, binding)})
            .Select(t => new CurveWoozy {
                CurveName = t.binding.propertyName,
                Keyframes = t.curve.keys.Select((frame, index) => KeyframeWoozy.From(t.curve, index, frame)).ToArray()
            }).ToArray();
        woozy.Events = clip.events;
        woozy.Name = clip.name;
        string path = Path.Combine(Application.dataPath, $"{clip.name}.woozy");
        File.WriteAllText(path, JsonConvert.SerializeObject(woozy));
        Debug.Log($"Wrote {clip.name}'s curves to {path}");
    }

    [MenuItem("Rip/Unrip Animation")]
    public static void UnripAnimation() {
        AnimationClip[] clips = Selection.GetFiltered<AnimationClip>(SelectionMode.Assets);
        if (clips.Length != 1)
            throw new Exception("ONLY SELECT ONE ANIMATION PLEASE AND THAnk you");
        AnimationClip clip = clips[0];
        EditorCurveBinding[] cub = AnimationUtility.GetCurveBindings(clip);
        AnimationCurve[] curves = cub.Select(curb => AnimationUtility.GetEditorCurve(clip, curb)).ToArray();
        string path = Path.Combine(Application.dataPath, $"{clip.name}.woozy");
        string input = File.ReadAllText(path);
        AnimationWoozy woozy = JsonConvert.DeserializeObject<AnimationWoozy>(input);
        if (curves.Length != woozy.Curves.Length)
            throw new Exception("Incorrect amount of curves for the deserialized animation");
        Debug.Log($"Applying curve animations from {clip.name}.woozy to {clip.name}");
        clip.events = woozy.Events;
        for (int i = 0; i < woozy.Curves.Length; i++) {
            CurveWoozy curveWoozy = woozy.Curves[i];
            AnimationCurve curve = curves[i];
            curve.keys = curveWoozy.Keyframes.Select(kf => (Keyframe) kf).ToArray();
            for (int i1 = 0; i1 < curveWoozy.Keyframes.Length; i1++) {
                Debug.Log($"{curveWoozy.CurveName} {cub[i].propertyName} {curve.keys[i1]} {curveWoozy.Keyframes[i1]}");
                AnimationUtility.SetKeyLeftTangentMode(curve, i1,
                    (AnimationUtility.TangentMode) curveWoozy.Keyframes[i1].LeftTangentMode);
                AnimationUtility.SetKeyRightTangentMode(curve, i1,
                    (AnimationUtility.TangentMode) curveWoozy.Keyframes[i1].RightTangentMode);
            }

            AnimationUtility.SetEditorCurve(clip, cub[i], curve);
        }
    }

    class AnimationWoozy {
        public string Name;
        public AnimationEvent[] Events;
        public CurveWoozy[] Curves;
    }

    public class CurveWoozy {
        public string CurveName;
        public KeyframeWoozy[] Keyframes;
    }

    public class KeyframeWoozy {
        public float Time;
        public float Value;
        public float InTangent;
        public float OutTangent;
        public int LeftTangentMode;
        public int RightTangentMode;
        public int WeightedMode;
        public float InWeight;
        public float OutWeight;

        public static KeyframeWoozy From(AnimationCurve curve, int index, Keyframe frame) {
            return new KeyframeWoozy {
                Time = frame.time,
                InTangent = frame.inTangent,
                InWeight = frame.inWeight,
                OutTangent = frame.outTangent,
                OutWeight = frame.outWeight,
                LeftTangentMode = (int) AnimationUtility.GetKeyLeftTangentMode(curve, index),
                RightTangentMode = (int) AnimationUtility.GetKeyRightTangentMode(curve, index),
                Value = frame.value,
                WeightedMode = (int) frame.weightedMode,
            };
        }

        public static explicit operator Keyframe(KeyframeWoozy woozy) {
            return new Keyframe {
                time = woozy.Time,
                inTangent = woozy.InTangent,
                inWeight = woozy.InWeight,
                outTangent = woozy.OutTangent,
                outWeight = woozy.OutWeight,
                value = Math.Abs(woozy.Value - (-5.5f)) < 0.0001f ? woozy.Value : woozy.Value - 5,
                weightedMode = (WeightedMode) woozy.WeightedMode
            };
        }
    }
}