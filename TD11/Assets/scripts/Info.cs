using UnityEngine;

public class Info : MonoBehaviour
{
    public int Soldier { get; private set;}
    public int Gold  { get; private set;}
    public int Civillian  { get; private set;}

    public void AddSoldier(int soldier)
    {
        Soldier += soldier;
        
    }

    public void AddAllGold(int gold)
    {
        Gold += gold;
    }

    public void AddCivillian(int civillian)
    {
        Civillian += civillian;
    }
}
