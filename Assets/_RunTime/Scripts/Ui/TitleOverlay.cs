using UnityEngine;
using UnityEngine.UI;

public sealed class TitleOverlay : MonoBehaviour
{
    [SerializeField] private Button _buttonGoToGame;

    public TitleOverlay(Button buttonGoToGame)
    {
        _buttonGoToGame = buttonGoToGame;
    }
    public void ButtonGoToGame()
    {
#if UNITY_EDITOR
        if (GameManager.instance == null)
        {
            Debug.LogWarning($"To load the game there must be a GameManager instance of the  Scene index:{(int)SceneIndexes.MANAGER}");
        }
        else
        {
            GameManager.instance.LoadGame();
        }
#else 
        GameManager.instance.LoadGame();
#endif
    }
}
