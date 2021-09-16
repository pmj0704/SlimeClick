using System.Collections.Generic;

[System.Serializable]
public class User
{
    public string nickname;
    public long slimes;
    public int slimePerClick;
    public List<Units> unitList = new List<Units>();
}
