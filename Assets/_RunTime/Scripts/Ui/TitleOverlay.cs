using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleOverlay : MonoBehaviour
{
    [SerializeField] private Button playGame;
    private void _GoToGame()
    {
        GameManager.instance.LoadGame();
    }
}
