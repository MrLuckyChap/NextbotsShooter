using System.Collections;
using CodeBase.Services.Level;
using CodeBase.Services.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI
{
  public class TapToPlayArea : MonoBehaviour
  {
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _text;

    private const string LevelCompletedText = "Level Comleted. Tap to Next";
    private const string LevelStartText = "Tap To Play";

    private IUIService _uiService;
    private ILevelService _levelService;

    [Inject]
    private void Constructor(IUIService uiService, ILevelService levelService)
    {
      _uiService = uiService;
      _levelService = levelService;
    }

    private void Start()
    {
      _text.text = LevelStartText;
      _levelService.LastLevelPointPassed += OnLastLevelPointJoined;
      _button.onClick.AddListener(OnTapToPlayAreaClicked);
    }

    private void OnLastLevelPointJoined() => StartCoroutine(EnableText());

    private void OnLevelCompleteButtonClicked()
    {
      _button.onClick.RemoveListener(OnLevelCompleteButtonClicked);
      _levelService.RestartLevel();
    }

    private void OnTapToPlayAreaClicked()
    {
      _button.onClick.RemoveListener(OnTapToPlayAreaClicked);
      StartCoroutine(DisableText());
      _uiService.OnTapAreaClicked();
    }

    private IEnumerator EnableText()
    {
      _text.text = LevelCompletedText;
      while (_text.alpha < 1)
      {
        _text.alpha += 0.05f;
        yield return new WaitForSeconds(0.02f);
      }

      _button.onClick.AddListener(OnLevelCompleteButtonClicked);
    }

    private IEnumerator DisableText()
    {
      while (_text.alpha > 0)
      {
        _text.alpha -= 0.03f;
        yield return new WaitForSeconds(0.02f);
      }
    }

    private void OnDestroy()
    {
      _levelService.LastLevelPointPassed -= OnLastLevelPointJoined;
    }
  }
}