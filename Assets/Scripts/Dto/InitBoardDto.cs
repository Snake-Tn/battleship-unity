using System;

using System.Collections.Generic;


[Serializable]
public class InitBoardDto
{
    public string action = "init-board";
    public List<UnitDto>  instruction = new List<UnitDto>();
}
