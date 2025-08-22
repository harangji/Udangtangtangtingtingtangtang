
using System.Threading.Tasks;

public interface ISkill
{
    public TaskCompletionSource<bool> Tcs { get; set; }
}