using UnityEngine;

[CreateAssetMenu(fileName = "People", menuName = "Scriptable Objects/People")]
public class People : ScriptableObject
{
    public string FullName;

    public string TeamRole;

    public bool VGD;
    public bool AMI;

    //First
    public Sprite Image1;
    //Second
    public Sprite Image2;
    //Third
    public Sprite Image3;

    // ---- AMI Only ---- //

    //Fourth
    public Sprite Image4;
    // Fifth
    public Sprite Image5;
}
