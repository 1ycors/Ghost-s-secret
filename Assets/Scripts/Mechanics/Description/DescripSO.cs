using UnityEngine;

[CreateAssetMenu(fileName = "NewDescrip", menuName = "Description/DescripSO")]
public class DescripSO : ScriptableObject
{
    [TextArea(1, 5)]
    public string[] descripLines;
}
