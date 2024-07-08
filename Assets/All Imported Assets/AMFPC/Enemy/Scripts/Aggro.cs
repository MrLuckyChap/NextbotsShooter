using System;
using System.Collections;
using CodeBase;
using UnityEngine;

namespace All_Imported_Assets.AMFPC.Enemy.Scripts
{
  public class Aggro : MonoBehaviour
  {
    public event Action PlayerEscaped;

    public bool HasAggroTarget => _hasAggroTarget;
    
    [SerializeField] private AgentMoveToPlayer _agentMoveToPlayer;
    [SerializeField] private TriggerObserver _triggerObserver;
    [SerializeField] private float _cooldown;

    private bool _hasAggroTarget;
    private WaitForSeconds _switchFollowOffAfterCooldown;
    private Coroutine _aggroCoroutine;

    private void Awake()
    {
      _switchFollowOffAfterCooldown = new WaitForSeconds(_cooldown);

      _triggerObserver.TriggerEnter += TriggerEnter;
      _triggerObserver.TriggerExit += TriggerExit;
      _agentMoveToPlayer.PlayerCaught += OnPlayerCaught;
      _agentMoveToPlayer.enabled = false;
    }

    private void TriggerEnter(Collider obj)
    {
      if (_hasAggroTarget) return;

      StopAggroCoroutine();

      SwitchFollowOn();
    }

    private void TriggerExit(Collider obj)
    {
      if (!_hasAggroTarget) return;

      _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
    }

    private void StopAggroCoroutine()
    {
      if (_aggroCoroutine == null) return;

      StopCoroutine(_aggroCoroutine);
      _aggroCoroutine = null;
    }

    private IEnumerator SwitchFollowOffAfterCooldown()
    {
      yield return _switchFollowOffAfterCooldown;
      SwitchFollowOff();
    }

    private void SwitchFollowOn()
    {
      _hasAggroTarget = true;
      _agentMoveToPlayer.enabled = true;
    }

    private void SwitchFollowOff()
    {
      PlayerEscaped?.Invoke();
      _agentMoveToPlayer.enabled = false;
      _hasAggroTarget = false;
    }

    private void OnPlayerCaught()
    {
      SwitchFollowOff();
    }

    private void OnDestroy()
    {
      _triggerObserver.TriggerEnter -= TriggerEnter;
      _triggerObserver.TriggerExit -= TriggerExit;
      _agentMoveToPlayer.PlayerCaught -= OnPlayerCaught;
    }
  }
}