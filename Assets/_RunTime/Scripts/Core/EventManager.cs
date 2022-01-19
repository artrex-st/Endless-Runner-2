using UnityEngine;

public sealed class EventManager : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
    [SerializeField] private SaveGame saveGame;
    [SerializeField] private PlayerCollision playerCollision;
    [SerializeField] private MainHUD mainHUD;
    [SerializeField] private StartOverlay startOverlay;
    private void Awake()
    {
        _Initialize();
    }
    public void _Initialize()
    {
        playerCollision.OnPlayerColliderHited += gameMode.OnColision;
        
        mainHUD.OnPauseGame += gameMode.OnPauseGame;
        mainHUD.OnResumeGame += gameMode.OnResumeGame;
        mainHUD.OnClickedQuitGame += gameMode.OnClickedQuitGame;
        startOverlay.OnCalledScoreData += gameMode.OnCalledScoreData;
    }
}
