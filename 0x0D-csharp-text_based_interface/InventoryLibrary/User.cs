using System;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary> User Class </summary>
public class User : BaseClass
{
    /// <summary> Object type for json serialization </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string type {get; set;} = "User";
    /// <summary> Name string of User </summary>
    public string name { get; set; }
    /// <summary> User constructor </summary>
    public User(string name) {
        this.name = name;
    }
    /// <summary> String override </summary>
    public override string ToString() {
        this.type = null;
        var res = JsonSerializer.Serialize(this);
        this.type = "User";
        return(res);
    }
}
