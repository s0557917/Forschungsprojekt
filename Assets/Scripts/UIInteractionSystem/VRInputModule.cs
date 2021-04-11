using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInputModule : BaseInputModule
{
    public Camera pointerCamera;
    public SteamVR_Input_Sources targetSource;
    public SteamVR_Action_Boolean clickAction;

    private GameObject currentObject;
    private PointerEventData data;

    protected override void Awake()
    {
        base.Awake();

        data = new PointerEventData(eventSystem);
    }

    public override void Process()
    {
        data.Reset();
        data.position = new Vector2(pointerCamera.pixelWidth / 2, pointerCamera.pixelHeight / 2);

        eventSystem.RaycastAll(data, m_RaycastResultCache);
        data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        currentObject = data.pointerCurrentRaycast.gameObject;

        m_RaycastResultCache.Clear();

        HandlePointerExitAndEnter(data, currentObject);

        if (clickAction.GetStateDown(targetSource))
        {
            ProcessPress(data);
        }

        if (clickAction.GetStateUp(targetSource))
        {
            ProcessRelease(data);
        }
    }

    public PointerEventData GetData()
    {
        return data;
    }

    private void ProcessPress(PointerEventData data)
    {

    }

    private void ProcessRelease(PointerEventData data)
    {

    }
}
