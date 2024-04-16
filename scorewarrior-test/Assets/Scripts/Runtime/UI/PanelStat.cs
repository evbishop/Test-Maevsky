using UnityEngine;
using UnityEngine.UI;

namespace Scorewarrior.Runtime.UI
{
    public abstract class PanelStat : MonoBehaviour
    {
        [field: SerializeField] protected Image imageForeground { get; private set; }

        public void Setup(float fillAmount)
        {
            imageForeground.fillAmount = fillAmount;
        }
    }
}
