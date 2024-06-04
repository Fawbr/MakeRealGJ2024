using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject, CustomInputs.IPlayerActions
{

    #region OnEnable

    public CustomInputs _customInputs;


    private void OnEnable()
    {
        if (_customInputs == null)
        {
            _customInputs = new CustomInputs();

            _customInputs.Player.SetCallbacks(this);

            ToggleActionMaps(_customInputs.Player);
        }
    }

    #endregion

    #region PlayerActions


    public event Action<Vector2> MoveEvent;

    public event Action<Vector2> AimingEvent;

    public event Action PunchEvent;

    public event Action ActionTwoEvent;

    public event Action ActionThreeEvent;

    public event Action JumpEvent;

    public event Action PauseEvent;


    public void OnMovement(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(obj: context.ReadValue<Vector2>());
    }

    public void OnAiming(InputAction.CallbackContext context)
    {
        AimingEvent?.Invoke(obj: context.ReadValue<Vector2>());
    }

    public void OnPunching(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            PunchEvent?.Invoke();
        }
        
    }

    public void OnActionTwo(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ActionTwoEvent?.Invoke();
        }
    }

  

    public void OnActionThree(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ActionThreeEvent?.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            JumpEvent?.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            PauseEvent?.Invoke();
        }
    }

    #endregion

    

    #region ToggleActionMaps

    public event Action<InputActionMap> actionMapChange;

    public void ToggleActionMaps(InputActionMap actionMap)
    {
        if (actionMap.enabled)
            return;

        _customInputs.Disable();
        actionMapChange?.Invoke(actionMap);
        actionMap.Enable();
    }



    #endregion
}
