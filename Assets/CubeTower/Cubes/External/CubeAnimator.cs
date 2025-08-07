using System;
using System.Collections.Generic;
using System.Threading;
using Cubes;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class CubeAnimator : MonoBehaviour
{
   [SerializeField] private Transform _firstHoleT;
   [SerializeField] private Transform _secondHoleT;
   
   private CancellationToken _cancellationToken;
   
   private void Awake()
   {
      _cancellationToken = this.GetCancellationTokenOnDestroy();
   }

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

   public void DropCubesDown(List<Cube> cubes)
   {
      DropCubesDownTask(cubes).Forget();
   }

   private async UniTaskVoid DropCubesDownTask(List<Cube> cubes)
   {
      foreach (var cube in cubes)
      {
         DropCube(cube);
         await UniTask.Delay(
            TimeSpan.FromSeconds(0.05f),
            false, 
            PlayerLoopTiming.Update, 
            _cancellationToken);
      }
   }

   
   
   private async UniTask DropCube(Cube cube)
   {
      var newPos = cube.transform.position;
      newPos.y -= cube.Size.y;
      cube.ToggleCollider(false);
      
      await cube.transform
         .DOMove(newPos, .3f)
         .SetEase(Ease.InCubic)
         .OnComplete(() => cube.ToggleCollider(true))
         .WithCancellation(_cancellationToken);
   }

   public void MoveTo(Cube target, Vector3 position)
   {
      var sequence = DOTween.Sequence();
      sequence.AppendCallback(() => target.ToggleCollider(false));
      sequence.Append(target.transform.DOMove(position, 0.2f).SetEase(Ease.InCubic));
      sequence.OnComplete(() =>
      {
         target.ToggleCollider(true);
      });
   }
}
