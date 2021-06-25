using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueGameButton : MonoBehaviour
{
    [SerializeField] private Button _stopGame;
    public void ContinurGame()
    {
        this.gameObject.SetActive(false);
        _stopGame.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
}
