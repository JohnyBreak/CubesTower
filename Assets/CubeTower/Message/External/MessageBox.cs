using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Message
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MessageBox : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _text;
        
        private Sequence _sequence;
        private int _priority = 0;
        
        private void Awake()
        {
            _canvasGroup.alpha = 0;
        }

        public void Show(string text, int priority = 0)
        {
            if (_sequence != null 
                && _sequence.IsPlaying() 
                && priority < _priority)
            {
                return;
            }

            _priority = priority;
            _text.text = text;

            if (_sequence != null)
            {
                _sequence.Kill();
            }

            _sequence = DOTween.Sequence();

            _sequence.Append(_canvasGroup.DOFade(1, 0.3f));
            _sequence.AppendInterval(1f);
            _sequence.Append(_canvasGroup.DOFade(0, 0.3f));
            _sequence.OnComplete(() => _priority = 0);
        }
        
        public void HideImmediately()
        {
            _canvasGroup.alpha = 0;
        }
    }
}
