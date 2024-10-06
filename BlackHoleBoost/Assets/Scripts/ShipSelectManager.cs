using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShipSelectManager : MonoBehaviour
{
    [SerializeField] private GameObject FirstPlayerSelectMenu;
    [SerializeField] private GameObject SecondPlayerSelectMenu;
    private int _playerIndex;

    [SerializeField] private Transform CanvasTran;

    private void Start()
    {
        PlayerInputManager.instance.playerPrefab = FirstPlayerSelectMenu;
        _playerIndex = 0;
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        if (playerInput.playerIndex==0)
        {

            playerInput.transform.SetParent(CanvasTran);
            float xPos = CanvasTran.GetComponent<RectTransform>().rect.width / 4;
            playerInput.gameObject.transform.localPosition = new Vector3(-xPos, 0, 0);
            playerInput.transform.GetComponent<Image>().color = Color.red;
            PlayerInputManager.instance.playerPrefab = SecondPlayerSelectMenu;
            //playerInput.SwitchCurrentControlScheme("Scheme_1");

            //playerInput.SwitchCurrentActionMap("ShipSelect_1");
        }
        else if (playerInput.playerIndex==1)
        {
            playerInput.transform.SetParent(CanvasTran);
            float xPos = CanvasTran.GetComponent<RectTransform>().rect.width / 4;
            playerInput.gameObject.transform.localPosition = new Vector3(xPos, 0, 0);
            playerInput.transform.GetComponent<Image>().color = Color.blue;
            //playerInput.SwitchCurrentControlScheme("Scheme_2");
            //playerInput.SwitchCurrentActionMap("ShipSelect_2");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (_playerIndex==0)
            {
                PlayerInput p= PlayerInput.Instantiate(FirstPlayerSelectMenu);
                p.transform.SetParent(CanvasTran);
                float xPos = CanvasTran.GetComponent<RectTransform>().rect.width / 4;
                p.gameObject.transform.localPosition = new Vector3(-xPos, 0, 0);
            }
            else if (_playerIndex==1)
            {
                PlayerInput p = PlayerInput.Instantiate(SecondPlayerSelectMenu);
                p.transform.SetParent(CanvasTran);
                float xPos = CanvasTran.GetComponent<RectTransform>().rect.width / 4;
                p.gameObject.transform.localPosition = new Vector3(xPos, 0, 0);
            }
            _playerIndex++;
        }
    }
}
