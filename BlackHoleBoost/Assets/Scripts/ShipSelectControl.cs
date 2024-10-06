using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ShipSelectControl : MonoBehaviour
{
    [SerializeField] private GameObject[] _verticalButtonList;
    [SerializeField] private Transform _headParentTran;
    [SerializeField] private Transform _bodyParentTran;
    private int _verticalBtnIndex;
    private int _headBtnIndex;
    private int _bodyBtnIndex;


    private void Start()
    {
        _verticalBtnIndex = 0;
        Image[] images = _verticalButtonList[_verticalBtnIndex].transform.GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = Color.green;
        }
    }

    private void SetSelectedBtn(int theLastIndex, int currentIndex)
    {
        Image[] lastImages = _verticalButtonList[theLastIndex].transform.GetComponentsInChildren<Image>();
        for (int i = 0; i < lastImages.Length; i++)
        {
            lastImages[i].color = Color.white;
        }
        Image[] images = _verticalButtonList[currentIndex].transform.GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = Color.green;
        }
    }

    public void ButtonGoDown(InputAction.CallbackContext input)
    {
        if (input.phase==InputActionPhase.Performed)
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
        if (input.phase==InputActionPhase.Performed)
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


}
