using System.Collections;
using Core;
using UnityEngine;
using World;

namespace UI
{
    public class CanvasFadeIn : MonoBehaviour
    {
        private CanvasGroup canvGroup;
        [SerializeField] private float len = 1f;
        [SerializeField] private float delay;
        [SerializeField] private AnimationCurve curve;
        
        void Awake()
        {
            canvGroup = GetComponent<CanvasGroup>();
            canvGroup.interactable = false;
        }
        
        void OnEnable()
        {
            EndCutsceneManager.EndCutsceneEvent += FadeIn;
        }
        
        void OnDisable()
        {
            EndCutsceneManager.EndCutsceneEvent -= FadeIn;
        }

        public void FadeIn()
        {
            StartCoroutine(FadeInRoutine());
        }

        IEnumerator FadeInRoutine()
        {
            yield return new WaitForSeconds(delay / Game.Instance.TimeScale);
            canvGroup.interactable = true;
            for (float t = 0; t < len; t += Game.Instance.DeltaTime)
            {
                float a = curve.Evaluate(t / len);
                canvGroup.alpha = a;
                yield return null;
            }
        }
    }
}