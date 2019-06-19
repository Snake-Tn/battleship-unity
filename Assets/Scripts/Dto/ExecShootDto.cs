using System;

[Serializable]
public class ExecShootDto
{
    public string action = "exec-shoot";
    public Coordination coordination;

    public ExecShootDto(Coordination coordination) {

        this.coordination = coordination;
    }
}

