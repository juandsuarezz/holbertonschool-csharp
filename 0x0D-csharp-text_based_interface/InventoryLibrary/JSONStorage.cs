using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

/// <summary> Storage Class using JSON </summary>
public class JSONStorage
{
    /// <summary> Dictionary of objects to be saved </summary>
    public Dictionary<string, dynamic> objects { get; set; } = new Dictionary<string, dynamic>();
    /// <summary> User constructor </summary>
    public Dictionary<string, dynamic> All() {
        return (objects);
    }
    /// <summary> Adds new object to dictionary </summary>
    public void New(BaseClass obj) {
        string key;

        if (obj == null)
            return;
        key = String.Format("{0}.{1}", obj.GetType(), obj.id);
        this.objects.Add(key, obj);
    }
    /// <summary> Saves all objects to file </summary>
    public void Save() {
        string jsonString = JsonSerializer.Serialize(this.objects);

        Directory.CreateDirectory("storage");
        File.WriteAllText("storage/inventory_manager.json", jsonString);
    }
    /// <summary> Loads all objects from file </summary>
    public void Load() {
        Dictionary<string, dynamic> tmp;
        Dictionary<string, dynamic> objs = new Dictionary<string, dynamic>();
        if (Directory.Exists("storage") && File.Exists("storage/inventory_manager.json")) {
            tmp = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(File.ReadAllText("storage/inventory_manager.json"));
            var keys = tmp.Keys;
            foreach (var key in keys) {
                objs.Add(key, nestedDeserialize(tmp[key].ToString()));
            }
            this.objects = objs;
        }
    }
    private dynamic nestedDeserialize(string obj) {
        Dictionary<string, string> tmp = JsonSerializer.Deserialize<Dictionary<string, string>>(obj);
        var keys = tmp.Keys;
        if (tmp["type"] == "User")
            return (JsonSerializer.Deserialize<User>(obj));
        else if (tmp["type"] == "Item")
            return (JsonSerializer.Deserialize<Item>(obj));
        else if (tmp["type"] == "Inventory")
            return (JsonSerializer.Deserialize<Inventory>(obj));
        else
            return (JsonSerializer.Deserialize<BaseClass>(obj));
    }
    public bool updateObj<T>(string key, string paramName, dynamic val) {
        var obj = this.objects[key];
        this.objects.Remove(key);
        var jsonString = JsonSerializer.Serialize(obj);
        Dictionary<string, string> objDict = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
        if (objDict.ContainsKey(paramName)) {
            objDict[paramName] = val.ToString();
            jsonString = JsonSerializer.Serialize(objDict);
            obj = JsonSerializer.Deserialize<T>(jsonString);
            New(obj);
            Save();
            return (true);
        }
        else {
            return (false);
        }
    }
}
