using UnityEngine;

namespace CodeBase.Player
{
  public class PlayerAnimator : MonoBehaviour
  {
    [SerializeField] private Animator _animator;

    private static readonly int _running = Animator.StringToHash("Running");

    public void PlayRunning()
    {
      _animator.SetBool(_running, true);
    }

    public void ResetToIdle()
    {
      _animator.SetBool(_running, false);
    }
  }
}