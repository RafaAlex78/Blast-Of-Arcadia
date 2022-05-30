using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject _intro;
    [SerializeField] GameObject _menu;
    [SerializeField] GameObject _Settings;
    private bool _introFinish = false;
    private void Start()
    {
        
    }

    private void Update()
    {
        if (_introFinish == false)
        {
            if (Input.anyKeyDown)
            {
                _intro.SetActive(false);
                _introFinish = true;
            }
        }
    }
    public void OpenSettings()
    {
        _Settings.SetActive(true);
    }
}
