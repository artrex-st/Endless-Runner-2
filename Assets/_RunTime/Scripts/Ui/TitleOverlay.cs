using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleOverlay : MonoBehaviour
{
    [SerializeField] private Button buttonGoToGame;
    public void ButtonGoToGame()
    {
        GameManager.instance.LoadGame();
    }
}
