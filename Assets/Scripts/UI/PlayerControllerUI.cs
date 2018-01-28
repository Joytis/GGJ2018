using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerUI : MonoBehaviour
{
    public Text thrust;
    public Slider thrustSlider;
    public Text turnSpeed;
    public Slider turnSpeedSlider;
    public Text maxTurn;
    public Slider maxTurnSlider;
    public Text mass;
    public Slider massSlider;
    public Dropdown forward;
    public Dropdown back;
    public Dropdown left;
    public Dropdown right;
    public Dropdown craft;

    float _thrust;
    float _turnSpeed;
    float _maxTurn;
    float _mass;
    KeyCode _forward;
    KeyCode _back;
    KeyCode _left;
    KeyCode _right;
    SpaceCraftManager.SpaceCraft _craft;

    // Use this for initialization
    void Start () {
        Init();
    }

    int KeyCodeCast(KeyCode code) {
        switch (code) {
            case KeyCode.UpArrow:
                return 0;
            case KeyCode.DownArrow:
                return 1;
            case KeyCode.LeftArrow:
                return 2;
            case KeyCode.RightArrow:
                return 3;
            case KeyCode.A:
                return 4;
            case KeyCode.B:
                return 5;
            case KeyCode.C:
                return 6;
            case KeyCode.D:
                return 7;
            case KeyCode.E:
                return 8;
            case KeyCode.F:
                return 9;
            case KeyCode.G:
                return 10;
            case KeyCode.H:
                return 11;
            case KeyCode.I:
                return 12;
            case KeyCode.J:
                return 13;
            case KeyCode.K:
                return 14;
            case KeyCode.L:
                return 15;
            case KeyCode.M:
                return 16;
            case KeyCode.N:
                return 17;
            case KeyCode.O:
                return 18;
            case KeyCode.P:
                return 19;
            case KeyCode.Q:
                return 20;
            case KeyCode.R:
                return 21;
            case KeyCode.S:
                return 22;
            case KeyCode.T:
                return 23;
            case KeyCode.U:
                return 24;
            case KeyCode.V:
                return 25;
            case KeyCode.W:
                return 26;
            case KeyCode.X:
                return 27;
            case KeyCode.Y:
                return 28;
            case KeyCode.Z:
                return 29;
            default:
                return 0;
        }
    }

    KeyCode CastKeyCode (int code) {
        switch (code) {
            case 0:
                return KeyCode.UpArrow;
            case 1:
                return KeyCode.DownArrow;
            case 2:
                return KeyCode.LeftArrow;
            case 3:
                return KeyCode.RightArrow;
            case 4:
                return KeyCode.A;
            case 5:
                return KeyCode.B;
            case 6:
                return KeyCode.C;
            case 7:
                return KeyCode.D;
            case 8:
                return KeyCode.E;
            case 9:
                return KeyCode.F;
            case 10:
                return KeyCode.G;
            case 11:
                return KeyCode.H;
            case 12:
                return KeyCode.I;
            case 13:
                return KeyCode.J;
            case 14:
                return KeyCode.K;
            case 15:
                return KeyCode.L;
            case 16:
                return KeyCode.M;
            case 17:
                return KeyCode.N;
            case 18:
                return KeyCode.O;
            case 19:
                return KeyCode.P;
            case 20:
                return KeyCode.Q;
            case 21:
                return KeyCode.R;
            case 22:
                return KeyCode.S;
            case 23:
                return KeyCode.T;
            case 24:
                return KeyCode.U;
            case 25:
                return KeyCode.V;
            case 26:
                return KeyCode.W;
            case 27:
                return KeyCode.X;
            case 28:
                return KeyCode.Y;
            case 29:
                return KeyCode.Z;
            default:
                return 0;
        }
    }

    public void Init () {
        thrustSlider.value = SpaceCraftManager.Instance.Thrust;
        turnSpeedSlider.value = SpaceCraftManager.Instance.Turn;
        maxTurnSlider.value = SpaceCraftManager.Instance.MaxTurn;
        massSlider.value = SpaceCraftManager.Instance.Mass;
        forward.value = KeyCodeCast(SpaceCraftManager.Instance.Forward);
        back.value = KeyCodeCast(SpaceCraftManager.Instance.Back);
        left.value = KeyCodeCast(SpaceCraftManager.Instance.Left);
        right.value = KeyCodeCast(SpaceCraftManager.Instance.Right);
        craft.value = (int)SpaceCraftManager.Instance.current;
    }

    public void UpdateThrust (float value) { thrust.text = string.Format("Thrust: {0:00}", value); _thrust = value; }
    public void UpdateTurn (float value) { turnSpeed.text = string.Format("Turn Speed: {0:00}", value); _turnSpeed = value; }
    public void UpdateMaxTurn (float value) { maxTurn.text = string.Format("Max Turn: {0:00}", value); _maxTurn = value; }
    public void UpdateMass (float value) { mass.text = string.Format("Mass: {0:00}", value); _mass = value; }
    public void UpdateForward (int value) { _forward = CastKeyCode(value); }
    public void UpdateBack (int value) { _back = CastKeyCode(value); }
    public void UpdateLeft (int value) { _left = CastKeyCode(value); }
    public void UpdateRight (int value) { _right = CastKeyCode(value); }
    public void UpdateCraft (int value) { _craft = (SpaceCraftManager.SpaceCraft)value; }

    public void OnSave () {
        SpaceCraftManager.Instance.Thrust = _thrust;
        SpaceCraftManager.Instance.Turn = _turnSpeed;
        SpaceCraftManager.Instance.MaxTurn = _maxTurn;
        SpaceCraftManager.Instance.Mass = _mass;
        SpaceCraftManager.Instance.Forward = _forward;
        SpaceCraftManager.Instance.Back = _back;
        SpaceCraftManager.Instance.Left = _left;
        SpaceCraftManager.Instance.Right = _right;
        SpaceCraftManager.Instance.UpdateCraft(_craft);
    }
}
