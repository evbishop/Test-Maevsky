using System;
using UnityEngine;

namespace Scorewarrior.Runtime.UI
{
    public class UserInterfaceHandler : MonoBehaviour
    {
        [SerializeField] private ViewContinue viewContinue;
        [SerializeField] private ViewReplay viewReplay;

        public event Action OnContinueClicked
        {
            add => viewContinue.OnContinueClicked += value;
            remove => viewContinue.OnContinueClicked -= value;
        }

        public event Action OnReplayClicked
        {
            add => viewReplay.OnReplayClicked += value;
            remove => viewReplay.OnReplayClicked -= value;
        }

        public void ShowContinue()
        {
            viewContinue.Show();
        }

        public void ShowReplay()
        {
            viewReplay.Show();
        }

        public void HideContinue()
        {
            viewContinue.Hide();
        }

        public void HideReplay()
        {
            viewReplay.Hide();
        }
    }
}
