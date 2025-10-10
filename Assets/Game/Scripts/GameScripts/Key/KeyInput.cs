using System;
using UnityEngine;

public class KeyInput : MonoBehaviour
{
    public static KeyInput Instance;

    private GameInputAction gameInputAction;

    public event EventHandler<NoteEventArgs> OnNotePerform;
    public event EventHandler<NoteEventArgs> OnNoteCancel;

    public class NoteEventArgs : EventArgs
    {
        public KeyManager.KeyNote key;
    }


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameInputAction = new GameInputAction();
        gameInputAction.Enable();
        gameInputAction.GameKey.KKey.started += KKey_started;
        gameInputAction.GameKey.KKey.canceled += KKey_canceled;
        gameInputAction.GameKey.JKey.started += JKey_started;
        gameInputAction.GameKey.JKey.canceled += JKey_canceled;
        gameInputAction.GameKey.FKey.started += FKey_started;
        gameInputAction.GameKey.FKey.canceled += FKey_canceled;
        gameInputAction.GameKey.DKey.started += DKey_started;
        gameInputAction.GameKey.DKey.canceled += DKey_canceled;
    }

    private void DKey_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnNoteCancel?.Invoke(this, new NoteEventArgs { key = KeyManager.KeyNote.D });
    }

    private void DKey_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnNotePerform?.Invoke(this, new NoteEventArgs { key = KeyManager.KeyNote.D });
    }

    private void FKey_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnNoteCancel?.Invoke(this, new NoteEventArgs { key = KeyManager.KeyNote.F });
    }

    private void FKey_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnNotePerform?.Invoke(this, new NoteEventArgs { key = KeyManager.KeyNote.F });
    }

    private void JKey_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnNoteCancel?.Invoke(this, new NoteEventArgs { key = KeyManager.KeyNote.J });
    }

    private void JKey_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnNotePerform?.Invoke(this, new NoteEventArgs { key = KeyManager.KeyNote.J });
    }

    private void KKey_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnNoteCancel?.Invoke(this, new NoteEventArgs { key = KeyManager.KeyNote.K });
    }

    private void KKey_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnNotePerform?.Invoke(this, new NoteEventArgs { key = KeyManager.KeyNote.K });
    }
}
