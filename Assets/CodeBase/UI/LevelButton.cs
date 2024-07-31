using UnityEngine;

namespace CodeBase.UI
{
  public class LevelButton : MonoBehaviour
  {
    public int Level => _level;

    [SerializeField] private int _level;
  }
}