[System.Serializable]
public class ItemInstance
{
    public QuestItemSO itemData;
    public int stackSize;

    public ItemInstance(QuestItemSO data, int count = 1) 
    {
        itemData = data;
        stackSize = count;
    }
}
