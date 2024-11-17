
[System.Serializable]
public class LeaderBoardInfo
{
    public string Name;
    public int SurvivedPhases;
    public int SurvivedTime;

    public LeaderBoardInfo(string name,int survivedPhases, int survivedTime)
    {
        Name = name;
        SurvivedPhases = survivedPhases;
        SurvivedTime = survivedTime;
    }
}
