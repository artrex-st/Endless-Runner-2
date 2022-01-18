using UnityEngine;

public class EventManager : MonoBehaviour
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
        startOverlay.OnCalledScoreData += gameMode.OnCalledScoreData;
    }
}
