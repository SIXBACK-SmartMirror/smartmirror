using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMirror.Helpers
{
    public static class FormHelper
    {
        public static (Screen primaryScreen, Screen secondaryScreen) SetupScreens(int outputMonitor, ref int inputMonitor, Screen[] screens)
        {
            // inputMonitor가 유효하지 않은 경우 설정
            if (screens.Length == 2)
            {
                inputMonitor = 0; // 2개의 모니터 중 첫 번째로 설정
            }

            // primary와 secondary 스크린 설정
            Screen primaryScreen = screens[inputMonitor];
            Screen secondaryScreen = screens[outputMonitor];

            return (primaryScreen, secondaryScreen);
        }

        public static void SwitchToForm(Form inputForm, Form outputForm, Screen primaryScreen, Screen secondaryScreen)
        {
            inputForm.StartPosition = FormStartPosition.Manual;
            inputForm.Location = primaryScreen.Bounds.Location;
            inputForm.Size = new Size(primaryScreen.Bounds.Width, primaryScreen.Bounds.Height);

            outputForm.StartPosition = FormStartPosition.Manual;
            outputForm.Location = secondaryScreen.Bounds.Location;
            outputForm.Size = new Size(secondaryScreen.Bounds.Width, secondaryScreen.Bounds.Height);

            inputForm.Show();
            outputForm.Show();
        }
    }
}
