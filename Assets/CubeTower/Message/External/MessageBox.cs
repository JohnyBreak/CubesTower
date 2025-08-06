using TMPro;
using UnityEngine;

namespace Message
{
    public class MessageBox : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _text;
        
        public void Show(string text)
        {
            _text.text = text;
            _canvasGroup.alpha = 1;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0;
        }
    }

}
