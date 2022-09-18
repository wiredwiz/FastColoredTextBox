using System;
using System.Runtime.InteropServices;
using System.Windows.Automation.Provider;

// ReSharper disable once CheckNamespace
namespace FastColoredTextBoxNS.Types;

// The NativeUIA class provides access to the native UIA provider functions and data.
public class NativeUIA
{
   [DllImport("UIAutomationCore.dll", EntryPoint = "UiaReturnRawElementProvider", CharSet = CharSet.Unicode)]
   public static extern IntPtr UiaReturnRawElementProvider(
      IntPtr hwnd, IntPtr wParam, IntPtr lParam, IRawElementProviderSimple el);

   [DllImport("UIAutomationCore.dll", EntryPoint = "UiaHostProviderFromHwnd", CharSet = CharSet.Unicode)]
   public static extern int UiaHostProviderFromHwnd(
      IntPtr hwnd,
      [MarshalAs(UnmanagedType.Interface)] out IRawElementProviderSimple provider);

   public const int WM_GETOBJECT = 0x003D;
   public static IntPtr UiaRootObjectId = (IntPtr)(-25);
}