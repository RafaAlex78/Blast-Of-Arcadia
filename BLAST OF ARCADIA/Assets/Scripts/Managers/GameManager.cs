using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private UIManager _ui;
    [SerializeField] private Inventory inventory;
    private bool _isPaused= false;
    private int _numberOfEnemies=2;
    public bool IsPaused { get => _isPaused; set => _isPaused = value; }
    public UIManager Ui { get => _ui; set => _ui = value; }
    public int NumberOfEnemies { get => _numberOfEnemies; set => _numberOfEnemies = value; }
    public Inventory Inventory { get => inventory; set => inventory = value; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            instance = this;
        }
    }
    private void Update()
    {
        if(CheckIsPaused()== true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public bool CheckIsPaused()
    {
        if(IsPaused == false)
        {
            return false;
        }
        return true;
        
    }
    //public void Checkalldead()
    //{
    //    if(NumberOfEnemies<=0)
    //    {
    //        Ui.ReloadScene();
    //    }
    //}
}
