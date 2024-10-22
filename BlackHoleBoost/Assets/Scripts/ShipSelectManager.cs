using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShipSelectManager : MonoBehaviour
{

    [SerializeField] private GameObject PlayerSelectMenu;

    [SerializeField] private Transform CanvasTran;

    private int readyPlayerNum;

    private static ShipSelectManager _instance;
    public static ShipSelectManager Instance=>_instance;

    

    private void Awake()
    {
        if (Instance!=null)
        {
            Destroy(Instance.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        SpawnSelectMenu();
    }

    public void PlayerGetReady(Color headColor, Color bodyColor)
    {
        readyPlayerNum++;
        if (readyPlayerNum==1)
        {
            LookOfPlayerShip.FirstPlayerHeadColor = headColor;
            LookOfPlayerShip.FirstPlayerBodyColor = bodyColor;
        }
        else if (readyPlayerNum==2)
        {
            LookOfPlayerShip.SecondPlayerHeadColor = headColor;
            LookOfPlayerShip.SecondPlayerBodyColor = bodyColor;
        }
        if (readyPlayerNum>=2)
        {
            StateMachine.Instance.ChangeState(new GamePlayingState());
        }
    }

    private void SpawnSelectMenu()
    {
        PlayerInput p=null;
        InputDevice[] devices=null;
        float xPos = CanvasTran.GetComponent<RectTransform>().rect.width / 4;

        if (Gamepad.all.Count >= 1)
        {
            devices= new InputDevice[] { Keyboard.current, Gamepad.all[0] };
        }
        else
        {
            devices= new InputDevice[] { Keyboard.current };
        }
        p = PlayerInput.Instantiate(PlayerSelectMenu, pairWithDevices: devices);
        p.SwitchCurrentActionMap("ShipSelect_1");
        p.transform.SetParent(CanvasTran);
        p.gameObject.transform.localPosition = new Vector3(-xPos, 0, 0);
        p.transform.GetComponent<Image>().color = Color.red;
        p.transform.GetComponent<ShipSelectControl>().InstructionText.text = "Use WASD(Keyboard) or Joystick(GamePad) to select the look of your ship, and press E(Keyboard) or A(Gamepad) to get ready.";

        if (Gamepad.all.Count >= 2)
        {
            devices= new InputDevice[] { Keyboard.current, Gamepad.all[1] };
        }
        else
        {
           devices= new InputDevice[] { Keyboard.current };
        }
        p = PlayerInput.Instantiate(PlayerSelectMenu, pairWithDevices: devices);
        p.SwitchCurrentActionMap("ShipSelect_2");
        p.transform.SetParent(CanvasTran);
        p.gameObject.transform.localPosition = new Vector3(xPos, 0, 0);
        p.transform.GetComponent<Image>().color = Color.blue;
        p.transform.GetComponent<ShipSelectControl>().InstructionText.text = "Use Arrow keys(Keyboard) or Joystick(GamePad) to select the look of your ship, and press 1(Keyboard) or A(Gamepad) to get ready.";
    }
}
