
[System.Serializable]
public class GlobalData
{
    //TODO : 설정 정보, 세이브 슬롯 정보, 등
    public SaveSlot[] Slots;

    public GlobalData() 
    {
        Slots = new SaveSlot[3];
    }
}
