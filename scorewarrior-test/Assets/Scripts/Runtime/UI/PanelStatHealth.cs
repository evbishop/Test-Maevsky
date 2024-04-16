using UnityEngine;
using UnityEngine.UI;

namespace Scorewarrior.Runtime.UI
{
    public class PanelStatHealth : PanelStat
    {
        [field: SerializeField] private Image _imageBackground;
        [SerializeField] private HealthBarInfo _healthBarInfo;

        public void SetGreen()
        {
            imageForeground.color = _healthBarInfo.GreenTeamFG;
            _imageBackground.color = _healthBarInfo.GreenTeamBG;
        }

        public void SetRed()
        {
            imageForeground.color = _healthBarInfo.RedTeamFG;
            _imageBackground.color = _healthBarInfo.RedTeamBG;
        }
    }
}
