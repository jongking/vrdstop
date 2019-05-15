using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Assets.Scripts
{
    class KeyboardHook
    {
        //int hHook;
        //Win32Api.HookProc KeyboardHookDelegate;
        //public event KeyEventHandler OnKeyDownEvent;
        //public event KeyEventHandler OnKeyUpEvent;
        //public event KeyPressEventHandler OnKeyPressEvent;
        //public KeyboardHook() { }
        //public void SetHook()
        //{
        //    KeyboardHookDelegate = new Win32Api.HookProc(KeyboardHookProc);
        //    Process cProcess = Process.GetCurrentProcess();
        //    ProcessModule cModule = cProcess.MainModule;
        //    var mh = Win32Api.GetModuleHandle(cModule.ModuleName);
        //    hHook = Win32Api.SetWindowsHookEx(Win32Api.WH_KEYBOARD_LL, KeyboardHookDelegate, mh, 0);
        //}
        //public void UnHook()
        //{
        //    Win32Api.UnhookWindowsHookEx(hHook);
        //}
        //private List<GlobalHotKey.Keys> preKeysList = new List<GlobalHotKey.Keys>();//存放被按下的控制键，用来生成具体的键
        //private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        //{
        //    //如果该消息被丢弃（nCode<0）或者没有事件绑定处理程序则不会触发事件
        //    if ((nCode >= 0) && (OnKeyDownEvent != null || OnKeyUpEvent != null || OnKeyPressEvent != null))
        //    {
        //        Win32Api.KeyboardHookStruct KeyDataFromHook = (Win32Api.KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(Win32Api.KeyboardHookStruct));
        //        GlobalHotKey.Keys keyData = (GlobalHotKey.Keys)KeyDataFromHook.vkCode;
        //        //按下控制键
        //        if ((OnKeyDownEvent != null || OnKeyPressEvent != null) && (wParam == Win32Api.WM_KEYDOWN || wParam == Win32Api.WM_SYSKEYDOWN))
        //        {
        //            if (IsCtrlAltShiftKeys(keyData) && preKeysList.IndexOf(keyData) == -1)
        //            {
        //                preKeysList.Add(keyData);
        //            }
        //        }
        //        //WM_KEYDOWN和WM_SYSKEYDOWN消息，将会引发OnKeyDownEvent事件
        //        if (OnKeyDownEvent != null && (wParam == Win32Api.WM_KEYDOWN || wParam == Win32Api.WM_SYSKEYDOWN))
        //        {
        //            KeyEventArgs e = new KeyEventArgs(GetDownKeys(keyData));

        //            OnKeyDownEvent(this, e);
        //        }
        //        //WM_KEYDOWN消息将引发OnKeyPressEvent 
        //        if (OnKeyPressEvent != null && wParam == Win32Api.WM_KEYDOWN)
        //        {
        //            byte[] keyState = new byte[256];
        //            Win32Api.GetKeyboardState(keyState);
        //            byte[] inBuffer = new byte[2];
        //            if (Win32Api.ToAscii(KeyDataFromHook.vkCode, KeyDataFromHook.scanCode, keyState, inBuffer, KeyDataFromHook.flags) == 1)
        //            {
        //                KeyPressEventArgs e = new KeyPressEventArgs((char)inBuffer[0]);
        //                OnKeyPressEvent(this, e);
        //            }
        //        }
        //        //松开控制键
        //        if ((OnKeyDownEvent != null || OnKeyPressEvent != null) && (wParam == Win32Api.WM_KEYUP || wParam == Win32Api.WM_SYSKEYUP))
        //        {
        //            if (IsCtrlAltShiftKeys(keyData))
        //            {
        //                for (int i = preKeysList.Count - 1; i >= 0; i--)
        //                {
        //                    if (preKeysList[i] == keyData) { preKeysList.RemoveAt(i); }
        //                }
        //            }
        //        }
        //        //WM_KEYUP和WM_SYSKEYUP消息，将引发OnKeyUpEvent事件 
        //        if (OnKeyUpEvent != null && (wParam == Win32Api.WM_KEYUP || wParam == Win32Api.WM_SYSKEYUP))
        //        {
        //            KeyEventArgs e = new KeyEventArgs(GetDownKeys(keyData));
        //            OnKeyUpEvent(this, e);
        //        }
        //    }
        //    return Win32Api.CallNextHookEx(hHook, nCode, wParam, lParam);
        //}
        ////根据已经按下的控制键生成key
        //private GlobalHotKey.Keys GetDownKeys(GlobalHotKey.Keys key)
        //{
        //    GlobalHotKey.Keys rtnKey = GlobalHotKey.Keys.None;
        //    foreach (GlobalHotKey.Keys i in preKeysList)
        //    {
        //        if (i == GlobalHotKey.Keys.LControlKey || i == GlobalHotKey.Keys.RControlKey) { rtnKey = rtnKey | GlobalHotKey.Keys.Control; }
        //        if (i == GlobalHotKey.Keys.LMenu || i == GlobalHotKey.Keys.RMenu) { rtnKey = rtnKey | GlobalHotKey.Keys.Alt; }
        //        if (i == GlobalHotKey.Keys.LShiftKey || i == GlobalHotKey.Keys.RShiftKey) { rtnKey = rtnKey | GlobalHotKey.Keys.Shift; }
        //    }
        //    return rtnKey | key;
        //}
        //private Boolean IsCtrlAltShiftKeys(GlobalHotKey.Keys key)
        //{
        //    if (key == GlobalHotKey.Keys.LControlKey || key == GlobalHotKey.Keys.RControlKey || key == GlobalHotKey.Keys.LMenu || key == GlobalHotKey.Keys.RMenu || key == GlobalHotKey.Keys.LShiftKey || key == GlobalHotKey.Keys.RShiftKey) { return true; }
        //    return false;
        //}
    }
}
