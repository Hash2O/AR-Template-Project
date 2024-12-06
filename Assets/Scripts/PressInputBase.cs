using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PressInputBase : MonoBehaviour
{
    protected InputAction m_PressAction;

    protected virtual void Awake()
    {
        //Create a new input within the script
        m_PressAction = new InputAction("touch", binding: "<Pointer>/press");

        m_PressAction.started += context =>
        {
            if (context.control.device is Pointer device)
            {
                OnPressBegan(device.position.ReadValue());
            }
        };

        //If touch is being performed, call the OnPress function
        m_PressAction.performed += context =>
        {
            if (context.control.device is Pointer device)
            {
                OnPress(device.position.ReadValue());
            }
        };

        //if the existing touch is stopped or canceled, call the OnPressCancel function
        m_PressAction.canceled += _ => OnPressCancel();

    }

    protected virtual void OnEnable()
    {
         m_PressAction.Enable();
    }

    protected virtual void OnDisable()
    {
        m_PressAction.Disable();
    }

    protected virtual void OnPress(Vector3 position) { }

    protected virtual void OnPressBegan(Vector3 position) { }

    protected virtual void OnPressCancel() { }

}
