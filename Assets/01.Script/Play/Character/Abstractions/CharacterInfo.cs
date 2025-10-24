using System;

[Serializable]
public class CharacterStat
{
    public int Hp = 1000;
    public int Attack = 100;
    public int Defense = 15;
    public float MoveSpeed = 5f; // 이동 속도
    public float MaxSpeed = 7f; // 최대 속도
    public float DashSpeed = 20f; // 대쉬 속도
    public float DashInterval = 3f; // 대쉬 간격
    public float AttackRange = 10f; // 공격 사거리
    public float FireInterval = 2f; // 공격 간격
    public int HpRegenAmount = 1;
    public float HpRegenTerm = 1;
};