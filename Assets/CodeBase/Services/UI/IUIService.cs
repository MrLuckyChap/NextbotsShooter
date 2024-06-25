using System;

namespace CodeBase.Services.UI
{
  public interface IUIService
  {
    event Action TapAreaClicked;

    void OnTapAreaClicked();
  }
}