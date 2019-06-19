using System;

[Serializable]
public class UnitDto
{
    public string orientation;
    public Coordination position;
    public int size;

    public UnitDto(string orientation,Coordination position,int size)
    {
        this.orientation = orientation;
        this.position = position;
        this.size = size;
    }
}

