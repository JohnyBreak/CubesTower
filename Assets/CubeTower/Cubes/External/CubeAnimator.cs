using System;
using Cubes;
using DG.Tweening;
using UnityEngine;

public class CubeAnimator : MonoBehaviour
{
   [SerializeField] private Transform _firstHoleT;
   [SerializeField] private Transform _secondHoleT;
   
   public void DropToHole(Cube target, Action onCompleteCallback)
   {
      var sequence = DOTween.Sequence();
      
      sequence.AppendCallback(() => target.ToggleCollider(false));
      sequence.Append(target.transform.DOMove(_firstHoleT.position, 0.5f).SetEase(Ease.OutSine));
      sequence.Append(target.transform.DORotate(new Vector3(0,0,45), 0.3f).SetEase(Ease.OutSine));
      sequence.AppendCallback(() => target.ToggleMaskable(true));
      sequence.Append(target.transform.DOMove(_secondHoleT.position, 0.5f).SetEase(Ease.OutSine));
      sequence.OnComplete(() =>
      {
         target.ToggleCollider(true);
         target.ToggleMaskable(false);
         onCompleteCallback?.Invoke();
      });
   }
   
   public void Destroy(Cube target, Action onCompleteCallback)
   {
      var sequence = DOTween.Sequence();
      sequence.AppendCallback(() => target.ToggleCollider(false));
      sequence.Append(target.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutElastic));
      sequence.OnComplete(() =>
      {
         target.ToggleCollider(true);
         onCompleteCallback?.Invoke();
      });
   }

   public void JumpTo(Cube target, Vector3 position)
   {
      var sequence = DOTween.Sequence();
      sequence.AppendCallback(() => target.ToggleCollider(false));
      sequence.Append(target.transform.DOJump(position, 2, 1, 0.5f));
      sequence.OnComplete(() =>
      {
         target.ToggleCollider(true);
      });
   }
}
