using UnityEngine;

public class SampleData : IKeyedData
{
    public string Key { get; set; }
}

[CreateAssetMenu(fileName = "GimmicDataSO", menuName = "Scriptable Object/Gimmic Data SO")]
public class CharacterDataSO : AParsingData<CharacterContext>
{
    public void a()
    {
        Debug.Log("CharacterDataSO a");
    }
}