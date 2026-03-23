using UnityEngine;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Management;
using TMPro;

public class XRHandsPeaceSignRecognizer : MonoBehaviour
{
    XRHandSubsystem subsystem;

    // Use TextMeshPro (3D) instead of TextMeshProUGUI
    public TextMeshPro gestureText;

    void Start()
    {
        var loader = XRGeneralSettings.Instance.Manager.activeLoader;
        subsystem = loader.GetLoadedSubsystem<XRHandSubsystem>();
    }

    void Update()
    {
        if (subsystem == null) return;

        CheckHand(subsystem.leftHand, "Left");
        CheckHand(subsystem.rightHand, "Right");
    }

    void CheckHand(XRHand hand, string label)
    {
        if (!hand.isTracked) return;

        var index = hand.GetJoint(XRHandJointID.IndexTip);
        var middle = hand.GetJoint(XRHandJointID.MiddleTip);
        var ring = hand.GetJoint(XRHandJointID.RingTip);
        var pinky = hand.GetJoint(XRHandJointID.LittleTip);
        var palm = hand.GetJoint(XRHandJointID.Palm);

        if (!index.TryGetPose(out var indexPose)) return;
        if (!middle.TryGetPose(out var middlePose)) return;
        if (!ring.TryGetPose(out var ringPose)) return;
        if (!pinky.TryGetPose(out var pinkyPose)) return;
        if (!palm.TryGetPose(out var palmPose)) return;

        float indexPalm = Vector3.Distance(indexPose.position, palmPose.position);
        float middlePalm = Vector3.Distance(middlePose.position, palmPose.position);
        float ringPalm = Vector3.Distance(ringPose.position, palmPose.position);
        float pinkyPalm = Vector3.Distance(pinkyPose.position, palmPose.position);

        bool indexExtended = indexPalm > 0.07f;
        bool middleExtended = middlePalm > 0.07f;
        bool ringCurled = ringPalm < 0.05f;
        bool pinkyCurled = pinkyPalm < 0.05f;

        if (indexExtended && middleExtended && ringCurled && pinkyCurled)
        {
            gestureText.text = $"{label} hand peace sign!";
        }
    }
}