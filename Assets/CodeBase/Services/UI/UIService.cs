using System;

namespace CodeBase.Services.UI
{
  public class UIService : IUIService
  {
    public event Action TapAreaClicked;

    public void OnTapAreaClicked() => TapAreaClicked?.Invoke();
  }
}