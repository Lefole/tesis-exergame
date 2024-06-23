
using System.Collections.Generic;

[System.Serializable]
public class Repetition
{
    public float abduction_angle;
    public bool succesfull;
    public float test_time;
    public float rest_time;
    public float total_time;

    public Repetition(float abduction_angle, bool succesfull, float test_time, float rest_time, float total_time)
    {
        this.abduction_angle = abduction_angle;
        this.succesfull = succesfull;
        this.test_time = test_time;
        this.rest_time = rest_time;
        this.total_time = total_time;
    }
}

[System.Serializable]
public class Serie
{
    public Dictionary<int, Repetition> serie;

    public Serie()
    {
        serie = new Dictionary<int, Repetition>();
    }
}

[System.Serializable]
public class GameData
{
    public Dictionary<int, Serie> series;

    public GameData()
    {
        series = new Dictionary<int, Serie>();
    }
}