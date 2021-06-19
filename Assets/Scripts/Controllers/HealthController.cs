using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class HealthController : MonoBehaviour
{
    [SerializeField] private BlockManager _blockManager;
    [SerializeField] private Player _player;

    [SerializeField] private HealthViewController _healthViewController;

    public int PlayerMaxHealth => _player.maxhealth;

    private void AddLife()
    {
        _player.health++;
    }

    private void DeleteLife()
    {
        _player.health--;
    }

    private void OnEnable()
    {
        _healthViewController.AddLifeEvent += AddLife;
        _healthViewController.DeleteLifeEvent += DeleteLife;
    }

    private void OnDisable()
    {
        _healthViewController.AddLifeEvent -= AddLife;
        _healthViewController.DeleteLifeEvent -= DeleteLife;
    }
}
