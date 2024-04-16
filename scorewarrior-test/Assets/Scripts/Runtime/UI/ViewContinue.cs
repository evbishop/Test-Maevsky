using System;

namespace Scorewarrior.Runtime.UI
{
    public class ViewContinue : UIView
    {
        public event Action OnContinueClicked;

        // Called from Button - Continue
        public void ClickContinue()
        {
            OnContinueClicked?.Invoke();
            Hide();
        }
    }
}
