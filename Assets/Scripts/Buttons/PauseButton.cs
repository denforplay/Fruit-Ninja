using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private Button _continueGame;
    public void PauseGame()
    {
        this.gameObject.SetActive(false);
        _continueGame.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
