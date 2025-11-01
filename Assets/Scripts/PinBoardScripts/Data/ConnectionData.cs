using System;

[Serializable]
public class ConnectionData
{
    public string clueA_ID;
    public string clueB_ID;

    public ConnectionData(string a, string b)
    {
        clueA_ID = a;
        clueB_ID = b;
    }
}
