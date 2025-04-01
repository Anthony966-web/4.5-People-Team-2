using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private int minutes;

    public int Minutes 
    { get { return minutes; } set { minutes = value; OnMinutesChange(value); } }








    public void OnMinutesChange(int value)
    {
        //throw new NotImplementedException();
    }
}
