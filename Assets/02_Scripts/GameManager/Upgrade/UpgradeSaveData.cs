[System.Serializable] // JsonUtility.ToJson를 사용하기 위해 꼭 작성해야됨
public class UpgradeSaveData
{
    public string[] Name;
    public int[] Level;

    public UpgradeSaveData(int count)
    {
        Name = new string[count];
        Level = new int[count];
    }
}
