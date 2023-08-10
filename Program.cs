using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NFA_RCOM;

namespace WindowsFormsApp84
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            
            //clsDevice var_dll = new clsDevice();
        }
    }
}
