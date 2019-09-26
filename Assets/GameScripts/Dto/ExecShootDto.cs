using System;

[Serializable]
public class ExecShootDto
{
    public string type = "exec-shoot";
    public Coordination instruction;

    public ExecShootDto(Coordination coordination) {

        this.instruction = coordination;
    }
}

