using UnityEngine;
using UnityEngine.InputSystem;
sealed public class InputManager : MonoBehaviour
{
    //
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    public delegate void SwipeAxis(Vector2 position);
    public event SwipeAxis OnSwipeAxis;
    //
    private InputControls _inputControls;
    [SerializeField]private Camera _mainCamera;

    private void Awake()
    {
        Initialize();
    }
    private void OnEnable()
    {
        _inputControls.Enable();
    }
    private void OnDisable()
    {
        _inputControls.Disable();
    }
    private void Initialize()
    {
        _inputControls = new InputControls();
        _mainCamera = Camera.main;
        _inputControls.PlayerInputs.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        _inputControls.PlayerInputs.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
        _inputControls.PlayerInputs.MoveControls.started += ctx => SwipeAxisVector2(ctx);
    }
    private void StartTouchPrimary(InputAction.CallbackContext contex)
    {
        OnStartTouch?.Invoke(Utils.ScreenToWorld(_mainCamera, _inputControls.PlayerInputs.PrimaryPosition.ReadValue<Vector2>()), (float)contex.startTime);
    }
    private void EndTouchPrimary(InputAction.CallbackContext contex)
    {
        OnEndTouch?.Invoke(Utils.ScreenToWorld(_mainCamera, _inputControls.PlayerInputs.PrimaryPosition.ReadValue<Vector2>()), (float)contex.time);
    }
    private void SwipeAxisVector2(InputAction.CallbackContext contex)
    {
        OnSwipeAxis?.Invoke(contex.ReadValue<Vector2>());  
    }
    // public Vector2 PrimaryPosition() // TRAIL
    // {
    //     return Utils.ScreenToWorld(mainCamera, playerControls.PlayerInputs.PrimaryPosition.ReadValue<Vector2>());
    // }
}
