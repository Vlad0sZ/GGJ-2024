namespace BackendGGJ.Models;

[System.Serializable]
public struct ActionData
{
    public ActionData(int id, int coast, int damage)
    {
        Id = id;
        Coast = coast;
        Damage = damage;
    }

    public int Id { get; set; }

    public int Coast { get; set; }

    public int Damage { get; set; }


    public ActionCoastData CoastData => new(Id, Coast);
}

public record ActionCoastData(int Id, int Coast);

public record ActionDamageData(int Id, int Damage, int Team);