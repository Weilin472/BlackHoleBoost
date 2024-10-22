using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMainMenu : MonoBehaviour
{
    [SerializeField] private Button _startBtn;
    [SerializeField] private Button _settingBtn;
    [SerializeField] private Button _instructionBtn;
    [SerializeField] private GameObject _settingMenu;
    [SerializeField] private GameObject _instructionMenu;

    private void OnEnable()
    {
        _startBtn.onClick.AddListener(() => StateMachine.Instance.ChangeState(new ShipSelectState()));
        _settingBtn.onClick.AddListener(() => StateMachine.Instance.ChangeState(new ActiveMenuState(_settingMenu)));
        _instructionBtn.onClick.AddListener(() => StateMachine.Instance.ChangeState(new ActiveMenuState(_instructionMenu)));
    }

    private void OnDisable()
    {
        _startBtn.onClick.RemoveListener(() => StateMachine.Instance.ChangeState(new ShipSelectState()));
        _settingBtn.onClick.RemoveListener(() => StateMachine.Instance.ChangeState(new ActiveMenuState(_settingMenu)));
        _instructionBtn.onClick.RemoveListener(() => StateMachine.Instance.ChangeState(new ActiveMenuState(_instructionMenu)));
    }


}