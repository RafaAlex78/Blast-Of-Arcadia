using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private UIManager _ui;
    private bool _isPaused= false;

    public bool IsPaused { get => _isPaused; set => _isPaused = value; }
    public UIManager Ui { get => _ui; set => _ui = value; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
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
}
