using UnityEngine;

public sealed class EventManager : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
    [SerializeField] private SaveGame saveGame;
    [SerializeField] private PlayerCollision playerCollision;
    [SerializeField] private MainHUD mainHUD;
    [SerializeField] private MainOverlay mainOverlay;
    [SerializeField] private StartOverlay startOverlay;

    
    private void OnEnable()
    {
        playerCollision.OnPlayerColliderHited += gameMode.OnColision;
        
        mainHUD.OnPauseGame += gameMode.OnPauseGame;
        mainHUD.OnResumeGame += gameMode.OnResumeGame;
        mainHUD.OnClickedQuitGame += gameMode.OnClickedQuitGame;
        startOverlay.OnCalledScoreData += gameMode.OnCalledScoreData;
        mainOverlay.OnStartedRun += gameMode.OnStartedRun;
        mainOverlay.OnOverlayStatusCalled += gameMode.OnOverlayStatusCalled;
    }
    private void OnDisable()
    {
        playerCollision.OnPlayerColliderHited -= gameMode.OnColision;
        
        mainHUD.OnPauseGame -= gameMode.OnPauseGame;
        mainHUD.OnResumeGame -= gameMode.OnResumeGame;
        mainHUD.OnClickedQuitGame -= gameMode.OnClickedQuitGame;
        startOverlay.OnCalledScoreData -= gameMode.OnCalledScoreData;
        mainOverlay.OnStartedRun += gameMode.OnStartedRun;
        mainOverlay.OnOverlayStatusCalled -= gameMode.OnOverlayStatusCalled;
        
    }
}
