using System;

public class InGameEventHandler : SingletonBase<InGameEventHandler>
{    
    protected override bool dontDestroyOnLoad { get; set; } = false;
    public EventHandler<ShowDamageTextEventArgs> ShowDamageTextHandler;
    public Action<EColliderCamp> CheakGameEndHandler;
}