using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotGrabber
{
    public enum MouseInputButtons
    {
        None,
        LeftButton,
        RightButton,
        MiddleButton,
        ScrollWheel,
        XButton1,
        XButton2,
    }

    public struct KeyEvent
    {
        public Keys key;
        public bool shift;
        public bool ctrl;
        public bool alt;
        public bool capsLock;
        public bool newPress;
    }

    public static class InputManager
    {
        #region Fields

        static GraphicsDevice graphicsDevice;

        public static int CursorID;

        static MouseState mouse;
        static MouseState prevMouse;
        static KeyboardState keyboard;
        static KeyboardState prevKeyboard;
        static Point mousePosition;
        static Vector2 mousePositionVec;
        static Vector2 mousePositionDelta;
        static float cursorSpeed;
        static int newScrollWheelPress;

        static KeyEvent keyEvent;
        static KeyEvent prevKeyEvent;
        static float repeatDelayTimer;
        static Keys[] keys;
        static Keys[] prevKeys;

        public static KeyEvent KeyEvent => keyEvent;
        public static KeyEvent PrevKeyEvent => prevKeyEvent;


        #endregion

        #region Initialization

        public static void Initialize(GraphicsDevice gd)
        {
            graphicsDevice = gd;
        }

        public static void ResetInputs()
        {
            prevMouse = mouse;
            prevKeyboard = keyboard;
        }

        #endregion

        #region Raw Mouse

        public static void SetMousePos(int x, int y)
        {
            Mouse.SetPosition(x, y);
        }

        public static void SetMousePos(Point p)
        {
            SetMousePos(p.X, p.Y);
        }

        public static void ResetMousePos(int x, int y)
        {
            Mouse.SetPosition(x, y);
            mouse = Mouse.GetState();
            mousePosition.X = mouse.X;
            mousePosition.Y = mouse.Y;
            mousePositionVec.X = mouse.X;
            mousePositionVec.Y = mouse.Y;
            mousePositionDelta.X = 0;
            mousePositionDelta.Y = 0;
        }

        public static Vector2 GetMousePosVec()
        {
            return mousePositionVec;
        }

        public static Point GetMousePos()
        {
            return mousePosition;
        }

        public static Vector2 GetMousePosDelta()
        {
            return mousePositionDelta;
        }

        public static int GetMouseWheelDelta()
        {
            return mouse.ScrollWheelValue - prevMouse.ScrollWheelValue;
        }

        public static bool IsMouseMoved()
        {
            return mousePositionDelta.X != 0 || mousePositionDelta.Y != 0;
        }

        public static bool IsMouseButtonPressed(MouseInputButtons button)
        {
            switch (button)
            {
                case MouseInputButtons.LeftButton:
                    return mouse.LeftButton == ButtonState.Pressed;
                case MouseInputButtons.RightButton:
                    return mouse.RightButton == ButtonState.Pressed;
                case MouseInputButtons.MiddleButton:
                    return mouse.MiddleButton == ButtonState.Pressed;
                case MouseInputButtons.XButton1:
                    return mouse.XButton1 == ButtonState.Pressed;
                case MouseInputButtons.XButton2:
                    return mouse.XButton2 == ButtonState.Pressed;
                case MouseInputButtons.ScrollWheel:
                    return mouse.ScrollWheelValue != prevMouse.ScrollWheelValue;
                default:
                    return false;
            }
        }

        public static bool IsMouseButtonJustPressed(MouseInputButtons button)
        {
            switch (button)
            {
                case MouseInputButtons.LeftButton:
                    return mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released;
                case MouseInputButtons.RightButton:
                    return mouse.RightButton == ButtonState.Pressed && prevMouse.RightButton == ButtonState.Released;
                case MouseInputButtons.MiddleButton:
                    return mouse.MiddleButton == ButtonState.Pressed && prevMouse.MiddleButton == ButtonState.Released;
                case MouseInputButtons.XButton1:
                    return mouse.XButton1 == ButtonState.Pressed && prevMouse.XButton1 == ButtonState.Released;
                case MouseInputButtons.XButton2:
                    return mouse.XButton2 == ButtonState.Pressed && prevMouse.XButton2 == ButtonState.Released;
                case MouseInputButtons.ScrollWheel:
                    return mouse.ScrollWheelValue != prevMouse.ScrollWheelValue && prevMouse.ScrollWheelValue == newScrollWheelPress;
                default:
                    return false;
            }
        }

        public static bool IsMouseButtonReleased(MouseInputButtons button)
        {
            switch (button)
            {
                case MouseInputButtons.LeftButton:
                    return mouse.LeftButton == ButtonState.Released;
                case MouseInputButtons.RightButton:
                    return mouse.RightButton == ButtonState.Released;
                case MouseInputButtons.MiddleButton:
                    return mouse.MiddleButton == ButtonState.Released;
                case MouseInputButtons.XButton1:
                    return mouse.XButton1 == ButtonState.Released;
                case MouseInputButtons.XButton2:
                    return mouse.XButton2 == ButtonState.Released;
                case MouseInputButtons.ScrollWheel:
                    return mouse.ScrollWheelValue == prevMouse.ScrollWheelValue;
                default:
                    return false;
            }
        }

        public static bool IsMouseButtonJustReleased(MouseInputButtons button)
        {
            switch (button)
            {
                case MouseInputButtons.LeftButton:
                    return mouse.LeftButton == ButtonState.Released && prevMouse.LeftButton == ButtonState.Pressed;
                case MouseInputButtons.RightButton:
                    return mouse.RightButton == ButtonState.Released && prevMouse.RightButton == ButtonState.Pressed;
                case MouseInputButtons.MiddleButton:
                    return mouse.MiddleButton == ButtonState.Released && prevMouse.MiddleButton == ButtonState.Pressed;
                case MouseInputButtons.XButton1:
                    return mouse.XButton1 == ButtonState.Released && prevMouse.XButton1 == ButtonState.Pressed;
                case MouseInputButtons.XButton2:
                    return mouse.XButton2 == ButtonState.Released && prevMouse.XButton2 == ButtonState.Pressed;
                case MouseInputButtons.ScrollWheel:
                    return mouse.ScrollWheelValue == prevMouse.ScrollWheelValue && prevMouse.ScrollWheelValue != newScrollWheelPress;
                default:
                    return false;
            }
        }

        public static bool IsMouseButtonChanged(MouseInputButtons button)
        {
            switch (button)
            {
                case MouseInputButtons.LeftButton:
                    return mouse.LeftButton != prevMouse.LeftButton;
                case MouseInputButtons.RightButton:
                    return mouse.RightButton != prevMouse.RightButton;
                case MouseInputButtons.MiddleButton:
                    return mouse.MiddleButton != prevMouse.MiddleButton;
                case MouseInputButtons.XButton1:
                    return mouse.XButton1 != prevMouse.XButton1;
                case MouseInputButtons.XButton2:
                    return mouse.XButton2 != prevMouse.XButton2;
                case MouseInputButtons.ScrollWheel:
                    return mouse.ScrollWheelValue != prevMouse.ScrollWheelValue;
                default:
                    return false;
            }
        }

        public static MouseInputButtons GetMouseButtonPressed()
        {
            if (mouse.LeftButton == ButtonState.Pressed) return MouseInputButtons.LeftButton;
            if (mouse.MiddleButton == ButtonState.Pressed) return MouseInputButtons.MiddleButton;
            if (mouse.RightButton == ButtonState.Pressed) return MouseInputButtons.RightButton;
            if (mouse.XButton1 == ButtonState.Pressed) return MouseInputButtons.XButton1;
            if (mouse.XButton2 == ButtonState.Pressed) return MouseInputButtons.XButton2;
            if (mouse.ScrollWheelValue != prevMouse.ScrollWheelValue) return MouseInputButtons.ScrollWheel;
            return MouseInputButtons.None;
        }

        public static MouseInputButtons GetMouseButtonJustPressed()
        {
            if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released) return MouseInputButtons.LeftButton;
            if (mouse.MiddleButton == ButtonState.Pressed && prevMouse.MiddleButton == ButtonState.Released) return MouseInputButtons.MiddleButton;
            if (mouse.RightButton == ButtonState.Pressed && prevMouse.RightButton == ButtonState.Released) return MouseInputButtons.RightButton;
            if (mouse.XButton1 == ButtonState.Pressed && prevMouse.XButton1 == ButtonState.Released) return MouseInputButtons.XButton1;
            if (mouse.XButton2 == ButtonState.Pressed && prevMouse.XButton2 == ButtonState.Released) return MouseInputButtons.XButton2;
            if (mouse.ScrollWheelValue != prevMouse.ScrollWheelValue && prevMouse.ScrollWheelValue == newScrollWheelPress) return MouseInputButtons.ScrollWheel;
            return MouseInputButtons.None;
        }

        #endregion

        #region Raw Keys

        public static Keys[] GetPressedKeys()
        {
            return keyboard.GetPressedKeys();
        }

        public static Keys[] GetPressedKeysPrev()
        {
            return prevKeyboard.GetPressedKeys();
        }

        public static bool IsKeyPressed(Keys key)
        {
            return keyboard.IsKeyDown(key);
        }

        public static bool IsKeyReleased(Keys key)
        {
            return keyboard.IsKeyUp(key);
        }

        public static bool IsKeyJustPressed(Keys key)
        {
            return keyboard.IsKeyDown(key) && prevKeyboard.IsKeyUp(key);
        }

        public static bool IsKeyJustReleased(Keys key)
        {
            return keyboard.IsKeyUp(key) && prevKeyboard.IsKeyDown(key);
        }

        public static bool IsKeyChanged(Keys key)
        {
            return keyboard[key] != prevKeyboard[key];
        }

        public static Keys GetNumKeyPressedNew()
        {
            var keys = keyboard.GetPressedKeys();
            foreach (var key in keys)
            {
                if (key >= Keys.D0 && key <= Keys.D9)
                {
                    var keys2 = prevKeyboard.GetPressedKeys();
                    foreach (var key2 in keys2)
                    {
                        if (key2 == key) return Keys.None;
                    }
                    return key;
                }
            }
            return Keys.None;
        }



        #endregion

        #region Update

        public static void Update(GameTime gameTime)
        {
            var pmsw = prevMouse.ScrollWheelValue;

            prevMouse = mouse;
            mouse = Mouse.GetState();

            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            prevKeyEvent = keyEvent;


            UpdateKeyboard(gameTime);
            UpdateMouse();
        }

        static void UpdateMouse()
        {

            mousePosition.X = mouse.X;
            mousePosition.Y = mouse.Y;
            mousePositionVec.X = mouse.X;
            mousePositionVec.Y = mouse.Y;

        }

        static void UpdateKeyboard(GameTime gameTime)
        {
            keyEvent.alt = keyEvent.shift = keyEvent.ctrl = false;
            keyEvent.newPress = false;
            keyEvent.key = Keys.None;

            int count = keyboard.GetPressedKeys().Count();
            if (count == 0)
            {
                repeatDelayTimer = 0;
                return;
            }

            if (keys == null || keys.Length < count)
            {
                keys = new Keys[count];
                prevKeys = new Keys[count];
            }

            keys = keyboard.GetPressedKeys();
            prevKeys = prevKeyboard.GetPressedKeys();

            repeatDelayTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            bool haveNewKey = false;

            foreach (var k in keys)
            {
                if (k != Keys.None)
                {
                    if (k == Keys.LeftShift || k == Keys.RightShift) keyEvent.shift = true;
                    else if (k == Keys.LeftControl || k == Keys.RightControl) keyEvent.ctrl = true;
                    else if (k == Keys.LeftAlt || k == Keys.RightAlt) keyEvent.alt = true;
                    else if (k == Keys.CapsLock) keyEvent.capsLock = !keyEvent.capsLock;
                    else if (!haveNewKey)
                    {
                        keyEvent.newPress = true;
                        keyEvent.key = k;

                        foreach (var last in prevKeys)
                        {
                            if (last == k)
                            {
                                keyEvent.newPress = false;
                                break;
                            }
                        }

                        if (keyEvent.newPress)
                        {
                            // we have found a new key, so we use this key. any other keys are ignored
                            // (it only makes sense to process one keypress, and a new keypress overrides a previously held keypress
                            // but we still have to loop through the entire array to pick up any ctrl key presses
                            haveNewKey = true;
                            repeatDelayTimer = 0.6f;
                        }
                        else if (repeatDelayTimer <= 0)
                            repeatDelayTimer = 0.06f;
                        else
                            keyEvent.key = Keys.None;
                    }
                }
            }
        }

        #endregion
    }

}
