using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

/// <summary> Item Class </summary>
public class Item : BaseClass
{
    /// <summary> Object type for json serialization </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string type {get; set;} = "Item";
    /// <summary> Name string of item </summary>
    public string name { get; set; }
    /// <summary> Optional description </summary>
    public string description { get; set; }
    /// <summary> Optional price </summary>
    public float price { get; set; }
    /// <summary> Optional list of tags </summary>
    public List<string> tags { get; set; }
    /// <summary> Item constructor </summary>
    public Item(string name, string description=null, float price=-1, List<string> tags=null) {
        this.name = name;
        this.description = description;
        if (price != -1) {
            this.price = (float)Math.Round(price * 100f) / 100f;
        }
        if (tags != null)
            this.tags = tags.ToList<string>();
    }
    /// <summary> String override </summary>
    public override string ToString() {
        this.type = null;
        var res = JsonSerializer.Serialize(this);
        this.type = "Item";
        return(res);
    }
}
