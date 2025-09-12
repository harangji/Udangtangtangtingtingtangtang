using System;

public class InGameEventHandler : SingletonBase<InGameEventHandler>
{    
    protected override bool dontDestroyOnLoad { get; set; } = false;
    public EventHandler<ShowAmountTextEventArgs> ShowDamageTextHandler;
    public Action<EColliderCamp> CheakGameEndHandler;
    public Action<float> GyroShakeHandler;
}