using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleOverlay : MonoBehaviour
{
    public Button playGame;
    private void Start()
    {
        playGame = GetComponentInChildren<Button>();
        //playGame.onClick.AddListener(delegate {Loader.LoadeScene((int)SceneIndexes.MAIN_GAME_SCREEN); });
        playGame.onClick.AddListener(delegate { GameManager.instance.LoadGame(); });
    }
}
