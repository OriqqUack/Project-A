
[System.Serializable]
public class GlobalData
{
    //TODO : ���� ����, ���̺� ���� ����, ��
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
