using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class ShipSelectControl : MonoBehaviour
{
    [SerializeField] private GameObject[] _verticalButtonList;
    [SerializeField] private Transform _headParentTran;
    [SerializeField] private Transform _bodyParentTran;
    private int _verticalBtnIndex;
    private int _headBtnIndex;
    private int _bodyBtnIndex;

    private bool isReady;

    public TMP_Text InstructionText;

    private void Start()
    {
        _verticalBtnIndex = 0;
        SetBtnColor(_verticalButtonList[0], Color.green);
    }

    private void SetSelectedBtn(int theLastIndex, int currentIndex)
    {
        SetBtnColor(_verticalButtonList[theLastIndex], Color.white);
        SetBtnColor(_verticalButtonList[currentIndex], Color.green);
    }

    private void SetBtnColor(GameObject buttonParent,Color c)
    {
        Image[] images = buttonParent.transform.GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = c;
        }
    }

    public void ButtonGoDown(InputAction.CallbackContext input)
    {
        if (input.phase==InputActionPhase.Performed&&!isReady)
        {
            int thelastindex = _verticalBtnIndex;
            _verticalBtnIndex++;
            if (_verticalBtnIndex>=_verticalButtonList.Length)
            {
                _verticalBtnIndex = 0;
            }
            SetSelectedBtn(thelastindex, _verticalBtnIndex);
        }
       
    }

    public void ButtonGoUp(InputAction.CallbackContext input)
    {
        if (input.phase==InputActionPhase.Performed&&!isReady)
        {
            int thelastIndex = _verticalBtnIndex;
            _verticalBtnIndex--;
            if (_verticalBtnIndex<0)
            {
                _verticalBtnIndex = 2;
            }
            SetSelectedBtn(thelastIndex, _verticalBtnIndex);
        }
       
    }

    private void SetHorizontalBtn(Transform tran,ref int index)
    {
        if (index>=tran.childCount)
        {
            index = 0;
        }
        else if (index<0)
        {
            index = tran.childCount - 1;
        }
        foreach (Transform child in tran)
        {
            child.gameObject.SetActive(false);
        }
        tran.GetChild(index).gameObject.SetActive(true);
    }

    public void CurrentButtonGoRight(InputAction.CallbackContext input)
    {
       
        if (input.phase==InputActionPhase.Performed)
        {
            if (_verticalBtnIndex==1)
            {
                _headBtnIndex++;
                SetHorizontalBtn(_headParentTran,ref _headBtnIndex);
            }
            else if (_verticalBtnIndex==2)
            {
                _bodyBtnIndex++;
                SetHorizontalBtn(_bodyParentTran,ref _bodyBtnIndex);
            }
            
        }
    }

    public void CurrentButtonGoLeft(InputAction.CallbackContext input)
    {
        if (input.phase == InputActionPhase.Performed)
        {
            if (_verticalBtnIndex == 1)
            {
                _headBtnIndex--;
                SetHorizontalBtn(_headParentTran, ref _headBtnIndex);
            }
            else if (_verticalBtnIndex == 2)
            {
                _bodyBtnIndex--;
                SetHorizontalBtn(_bodyParentTran, ref _bodyBtnIndex);
            }

        }
    }

    public void ConfirmReady(InputAction.CallbackContext input)
    {
        if (input.phase==InputActionPhase.Performed&&_verticalBtnIndex==0&&!isReady)
        {
            isReady = true;
            SetBtnColor(_verticalButtonList[0], Color.red);
            
            ShipSelectManager.Instance.PlayerGetReady(_headParentTran.GetChild(_headBtnIndex).GetComponent<Image>().color,_bodyParentTran.GetChild(_bodyBtnIndex).GetComponent<Image>().color);
        }
    }


}
