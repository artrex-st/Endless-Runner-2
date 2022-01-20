using UnityEngine;

public sealed class EventManager : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
    [SerializeField] private SaveGame saveGame;
    [SerializeField] private PlayerCollision playerCollision;
    [SerializeField] private MainHUD mainHUD;
    [SerializeField] private MainOverlay mainOverlay;
    [SerializeField] private StartOverlay startOverlay;
    [SerializeField] private PlayerAnimationController playerAnimationController;
    [SerializeField] private PlayerControl playerControl;

    
    private void OnEnable()
    {
        playerCollision.OnPlayerColliderHited += gameMode.OnColision;
        startOverlay.OnCalledScoreData += saveGame.OnCalledScoreData;
        
        playerControl.OnStartedTriggerAnimation += playerAnimationController.OnStartedTriggerAnimation;
        playerControl.OnStartedBoolAnimation += playerAnimationController.OnStartedBoolAnimation;

        mainHUD.OnPauseGame += gameMode.OnPauseGame;
        mainHUD.OnResumeGame += gameMode.OnResumeGame;
        mainHUD.OnClickedQuitGame += gameMode.OnClickedQuitGame;
        
        mainOverlay.OnStartedRun += gameMode.OnStartedRun;
        mainOverlay.OnOverlayStatusCalled += gameMode.OnOverlayStatusCalled;
    }
    private void OnDisable()
    {
        playerCollision.OnPlayerColliderHited -= gameMode.OnColision;
        startOverlay.OnCalledScoreData -= saveGame.OnCalledScoreData;
        
        playerControl.OnStartedTriggerAnimation += playerAnimationController.OnStartedTriggerAnimation;
        playerControl.OnStartedBoolAnimation += playerAnimationController.OnStartedBoolAnimation;

        mainHUD.OnPauseGame -= gameMode.OnPauseGame;
        mainHUD.OnResumeGame -= gameMode.OnResumeGame;
        mainHUD.OnClickedQuitGame -= gameMode.OnClickedQuitGame;
        
        mainOverlay.OnStartedRun += gameMode.OnStartedRun;
        mainOverlay.OnOverlayStatusCalled -= gameMode.OnOverlayStatusCalled;
    }
}
