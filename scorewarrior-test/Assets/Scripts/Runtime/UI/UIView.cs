using System.Collections;
using UnityEngine;

namespace Scorewarrior.Runtime.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIView : MonoBehaviour
    {
        public enum VisibilityState : byte
        {
            None = 0,
            Hidden = 1,
            Showing = 2,
            Visible = 3,
            Hiding = 4,
        }

        [field: SerializeField] protected VisibilityState initialVisibility { get; private set; }
        [field: SerializeField] protected float showDuration { get; private set; }
        [field: SerializeField] protected float hideDuration { get; private set; }

        protected CanvasGroup canvasGroup { get; private set; }
        protected VisibilityState visibility;
        
        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        protected virtual void Start()
        {
            switch (initialVisibility)
            {
                case VisibilityState.Hidden:
                    canvasGroup.alpha = 0;
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;
                    break;
                case VisibilityState.Visible:
                    canvasGroup.alpha = 1;
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                    break;
                case VisibilityState.Showing:
                    Show();
                    break;
                case VisibilityState.Hiding:
                    Hide();
                    break;
            }
        }

        public virtual void Show(bool isAnimated = true)
        {
            if (isAnimated)
            {
                StartCoroutine(ShowCoroutine());
            }
            else
            {
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
        }

        public virtual void Hide(bool isAnimated = true)
        {
            if (isAnimated)
            {
                StartCoroutine(HideCoroutine());
            }
            else
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
        }

        protected virtual IEnumerator ShowCoroutine()
        {
            float currentTime = 0f;
            while (currentTime < showDuration)
            {
                currentTime += Time.deltaTime;
                canvasGroup.alpha = currentTime / showDuration;
                yield return null;
            }
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        protected virtual IEnumerator HideCoroutine()
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            float currentTime = 0f;
            while (currentTime < hideDuration)
            {
                currentTime += Time.deltaTime;
                canvasGroup.alpha = 1 - currentTime / hideDuration;
                yield return null;
            }
        }
    }
}
