using System;


[Serializable]
public class MessageDto
{
    public string action;
    public Object instruction;

    public MessageDto(string action, Object instruction)
    {
        this.action = action;
        this.instruction = instruction;
    }
}

