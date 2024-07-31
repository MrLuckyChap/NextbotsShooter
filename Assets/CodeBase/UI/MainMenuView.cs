using System.Collections.Generic;
using CodeBase.Services.Level;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI
{
  public class MainMenuView : MonoBehaviour
  {
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private List<LevelButton> _levelButtons;

    private ILevelService _levelService;
    private int _currentLevel;

    private void Awake()
    {
      _playButton.interactable = false;
      foreach (LevelButton levelButton in _levelButtons)
      {
        levelButton.GetComponent<Button>().onClick.AddListener(() =>
        {
          _currentLevel = levelButton.Level;
          _playButton.interactable = true;
        });
      }
    }

    [Inject]
    private void Constructor(ILevelService levelService)
    {
      _levelService = levelService;
    }

    private void Start()
    {
      _playButton.onClick.AddListener(OnStartLevelButtonClicked);
      _exitButton.onClick.AddListener(Application.Quit);
    }

    private void OnStartLevelButtonClicked()
    {
      _levelService.StartLevel(_currentLevel);
    }
  }
}