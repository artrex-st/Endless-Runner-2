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
    private InputControls inputControls;
    [SerializeField]private Camera mainCamera;

    private void Awake()
    {
        Initialize();
    }
    private void OnEnable()
    {
        inputControls.Enable();
    }
    private void OnDisable()
    {
        inputControls.Disable();
    }
    private void Initialize()
    {
        inputControls = new InputControls();
        mainCamera = Camera.main;
        inputControls.PlayerInputs.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        inputControls.PlayerInputs.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
        inputControls.PlayerInputs.MoveControls.started += ctx => SwipeAxisVector2(ctx);
    }
    private void StartTouchPrimary(InputAction.CallbackContext contex)
    {
        OnStartTouch?.Invoke(Utils.ScreenToWorld(mainCamera, inputControls.PlayerInputs.PrimaryPosition.ReadValue<Vector2>()), (float)contex.startTime);
    }
    private void EndTouchPrimary(InputAction.CallbackContext contex)
    {
        OnEndTouch?.Invoke(Utils.ScreenToWorld(mainCamera, inputControls.PlayerInputs.PrimaryPosition.ReadValue<Vector2>()), (float)contex.time);
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
