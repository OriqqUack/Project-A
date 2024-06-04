
[System.Serializable]
public class GlobalData
{
    //TODO : 설정 정보, 세이브 슬롯 정보, 등
    public bool[] existSaveFile;
    public string[] SaveSlotName;
    public float[] SaveSlotPlayTime;


    public GlobalData() 
    {
        existSaveFile = new bool[3];
        SaveSlotName = new string[3];
        SaveSlotPlayTime = new float[3];
    }
}
