
using System.Threading.Tasks;

public class SamplePlayer1 : CharacterBase
{
    public override ICharacter.Stat InitializeStat()
    {
        UnitStat = new ICharacter.Stat()
        {
            Attack = 10,
        };
        return UnitStat;
    }

    public override void Shove()
    {
        throw new System.NotImplementedException();
    }

    public override void Shoved()
    {
        throw new System.NotImplementedException();
    }

    public override Task ExecuteSkill()
    {
        throw new System.NotImplementedException();
    }
}
