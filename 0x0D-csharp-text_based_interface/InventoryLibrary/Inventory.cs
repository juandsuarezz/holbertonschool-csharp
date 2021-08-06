using System;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary> Inventory Class </summary>
public class Inventory : BaseClass
{
    /// <summary> Object type for json serialization </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string type {get; set;} = "Inventory";
    /// <summary> User ID from user object </summary>
    public string user_id { get; set; }
    /// <summary> Item ID from item object </summary>
    public string item_id { get; set; }
    /// <summary> Quantity of item </summary>
    public int quantity {get; set; }
    /// <summary> Inventory constructor </summary>
    public Inventory(string user_id, string item_id, int quantity=1) {
        this.user_id = user_id;
        this.item_id = item_id;
        if (quantity > 0)
            this.quantity = quantity;
        else
            this.quantity = 1;
    }
    /// <summary> String override </summary>
    public override string ToString() {
        this.type = null;
        var res = JsonSerializer.Serialize(this);
        this.type = "Inventory";
        return(res);
    }
}
