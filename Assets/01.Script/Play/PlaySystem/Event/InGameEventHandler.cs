using System;

public class InGameEventHandler : SingletonBase<InGameEventHandler>
{    
    protected override bool dontDestroyOnLoad { get; set; } = false;
    public event EventHandler<ShowAmountTextEventArgs> ShowDamageTextHandler;
    public event Action<EColliderCamp> CheckGameEndHandler;
    public event Action<float> GyroShakeHandler;

    // 이벤트를 발생시키는 메서드를 추가합니다.
    public void OnShowDamageText(object sender, ShowAmountTextEventArgs e)
    {
        ShowDamageTextHandler?.Invoke(sender, e);
    }
}