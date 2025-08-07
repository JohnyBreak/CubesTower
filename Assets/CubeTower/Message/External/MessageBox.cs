using System.Collections;
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

        private Vector3 _startPosition;
        private Sequence _sequence;
        private int _priority = 0;
        private int _verticalOffset = 100;
        
        private void Awake()
        {
            _canvasGroup.alpha = 0;
        }

        private IEnumerator Start()
        {
            yield return null;
            _startPosition = transform.position;
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
            _sequence.AppendCallback(() =>
            {
                _canvasGroup.alpha = 0;
                transform.position = _startPosition;
            });
            _sequence.Append(transform.DOMoveY(_startPosition.y - _verticalOffset, 0.75f));
            _sequence.Join(_canvasGroup.DOFade(1, 0.3f));
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
