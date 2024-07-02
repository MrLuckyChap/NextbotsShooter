using CodeBase.Services.Level;
using CodeBase.Services.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI
{
  public class MainMenuView : MonoBehaviour
  {
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;

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
      _playButton.onClick.AddListener(OnStartLevelButtonClicked);
      _exitButton.onClick.AddListener(Application.Quit);
    }

    private void OnStartLevelButtonClicked()
    {
      _levelService.StartLevel();
    }
  }
}