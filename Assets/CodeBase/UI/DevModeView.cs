using System.Collections.Generic;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Services.StaticData;
using CodeBase.Services.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI
{
  public class DevModeView : MonoBehaviour
  {
    [SerializeField] private int _countCellsInHorizontalBlock;
    [SerializeField] private Button _devModeCloseButton;
    [SerializeField] private GameObject _horizontalBlockPrefab;
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private GameObject _verticalBlock;

    private IGameUIService _gameUIService;
    private IStaticDataService _dataService;

    private void Awake()
    {
      _devModeCloseButton.onClick.AddListener(OnDevModeCloseButtonClicked);
    }

    [Inject]
    private void Constructor(IGameUIService gameUIService, IStaticDataService dataService)
    {
      _gameUIService = gameUIService;
      _dataService = dataService;
    }

    private void Start()
    {
      SpawnBlock(_dataService.EnemiesData.AllEnemiesData);
      SpawnBlock(_dataService.AlliesData.AllAlliesData);
      SpawnBlock(_dataService.OtherObjectsData.AllOtherObjectsData);
    }

    private void SpawnBlock(List<EntityData> entityData)
    {
      GameObject horizontalBlock = Instantiate(_horizontalBlockPrefab, _verticalBlock.transform);
      for (var i = 0; i < entityData.Count; i++)
      {
        GameObject cell = Instantiate(_cellPrefab, horizontalBlock.transform);
        int temp = i;
        cell.GetComponent<Button>().onClick.AddListener(() => OnCellButtonClicked(entityData[temp]));
        if (i % _countCellsInHorizontalBlock != 0)
          horizontalBlock = Instantiate(_horizontalBlockPrefab, _verticalBlock.transform);
      }
    }

    private void OnCellButtonClicked(EntityData entityData)
    {
      _gameUIService.SpawnUIClickedObject(entityData);
    }

    private void OnDevModeCloseButtonClicked() => gameObject.SetActive(false);
  }
}