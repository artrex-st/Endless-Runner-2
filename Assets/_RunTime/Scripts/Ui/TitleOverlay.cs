using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleOverlay : MonoBehaviour
{
    public Button playGame;
    private void Start()
    {
        _Initialize();
    }
    private void _Initialize()
    {
        playGame = GetComponentInChildren<Button>();
        playGame.onClick.AddListener(delegate { GameManager.instance.LoadGame(); });
    }
}
