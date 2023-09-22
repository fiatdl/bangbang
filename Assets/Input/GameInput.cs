using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }


    private TankInputAction tankInputAction;
    private GameMnagement gameManagementInput;
    public event EventHandler skill1;
    public event EventHandler Drop;
    public event EventHandler InteractAlternal;
    public event EventHandler OnPauseAction;
    public event EventHandler OnBindingRebind;
    public event EventHandler Sub;
    public event EventHandler Until; 
    public event EventHandler Left;
    public event EventHandler Right;
    private void Awake()
    {
        tankInputAction = new TankInputAction();
        gameManagementInput = new GameMnagement();
        tankInputAction.tank1.Enable();
        tankInputAction.tank1.skill1.performed += Skill1_performed;
        tankInputAction.tank1.until.performed += Until_performed;
        tankInputAction.tank1.sub.performed += Q_performed;
        gameManagementInput.handleSelect.Enable();
        gameManagementInput.handleSelect.left.performed += Left_performed;
        gameManagementInput.handleSelect.right.performed += Right_performed;
     
    }

    private void Right_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Right.Invoke(this, EventArgs.Empty);
    }

    private void Left_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
      Left.Invoke(this, EventArgs.Empty);
    }

    private void Q_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
             Sub?.Invoke(this,EventArgs.Empty);

    }

    private void Until_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Until?.Invoke(this, EventArgs.Empty);
    }

    private void Skill1_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        skill1?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = tankInputAction.tank1.move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;
        return inputVector;
    }
}
