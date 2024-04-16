using System;

namespace Scorewarrior.Runtime.UI
{
    public class ViewReplay : UIView
    {
        public event Action OnReplayClicked;

        // Called from Button - Replay
        public void ClickReplay()
        {
            OnReplayClicked?.Invoke();
            Hide();
        }
    }
}
