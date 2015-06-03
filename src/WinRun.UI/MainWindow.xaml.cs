﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WinRun.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ClockWindow clockWindow = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            HotkeyHelper helper = new HotkeyHelper(this);
            helper.Register(Key.F3, ModifierKeys.Windows, delegate
            {
                WindowInteropHelper interop = new WindowInteropHelper(this);
                SendMessage(interop.Handle, WM_SYSCOMMAND, SC_MONITORPOWER, MONITOR_OFF);
            });
            helper.Register(Key.F4, ModifierKeys.Windows, delegate
            {
                System.Windows.Forms.Application.SetSuspendState(System.Windows.Forms.PowerState.Suspend, true, true);
            });
            helper.Register(Key.F12, ModifierKeys.Windows, delegate
            {
                System.Windows.Forms.Application.SetSuspendState(System.Windows.Forms.PowerState.Hibernate, true, true);
            });
            helper.Register(Key.F5, ModifierKeys.Windows, delegate
            {
                Process.Start(@"C:\Windows\explorer.exe", "::{7007ACC7-3202-11D1-AAD2-00805FC1270E}");
            });
            helper.Register(Key.F6, ModifierKeys.Windows, delegate
            {
                if (clockWindow == null)
                {
                    clockWindow = new ClockWindow();
                    clockWindow.Closed += (sender, args) => { clockWindow = null; };
                }
                clockWindow.SetClockSize(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.Right) ? ClockSize.Medium : ClockSize.Large);
                clockWindow.Show();
                clockWindow.Activate();
            });
            helper.Register(Key.L, ModifierKeys.Windows | ModifierKeys.Shift, delegate
            {
                LockWorkStation();
                WindowInteropHelper interop = new WindowInteropHelper(this);
                SendMessage(interop.Handle, WM_SYSCOMMAND, SC_MONITORPOWER, MONITOR_OFF);
            });
        }

        [DllImport("user32.dll")]
        public static extern void LockWorkStation();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        static int WM_SYSCOMMAND = 0x112;
        static IntPtr SC_MONITORPOWER = new IntPtr(0xF170);

        static IntPtr MONITOR_ON = new IntPtr(-1);
        static IntPtr MONITOR_OFF = new IntPtr(2);
        static IntPtr MONITOR_STANBY = new IntPtr(1);

        private void Window_Activated_1(object sender, EventArgs e)
        {
            Hide();
        }
    }
}