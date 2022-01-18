using UnityEngine;
using UnityEngine.UI;

public class TitleOverlay : MonoBehaviour
{
    [SerializeField] private Button _buttonGoToGame;
    public void ButtonGoToGame()
    {
#if UNITY_EDITOR
        if (GameManager.instance == null)
        {
            Debug.LogWarning($"To load the game there must be a GameManager instance of the {SceneIndexes.MANAGER}");
        }
#else 
        GameManager.instance.LoadGame();
#endif
    }
    public void Initialize(Button buttonGoToGame)
    {
        _buttonGoToGame = buttonGoToGame;
    }
}
