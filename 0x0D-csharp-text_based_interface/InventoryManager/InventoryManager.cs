using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManager
{
    class Program
    {
        static void Main(string[] args)
        {
            JSONStorage storage = new JSONStorage();

            storage.Load();
            string prompt = @"Inventory Manager
-------------------------
<ClassNames> show all ClassNames of objects
<All> show all objects
<All [ClassName]> show all objects of a ClassName
<Create [ClassName]> a new object
<Show [ClassName object_id]> an object
<Update [ClassName object_id]> an object
<Delete [ClassName object_id]> an object
<Exit>
";
            string[] types = {"BaseClass", "User", "Item", "Inventory"};

            Console.Write(prompt);
            for (string r = Console.ReadLine(); r != null; r = Console.ReadLine()) {
                string[] commands = r.Split(' ');
                if (string.Compare(commands[0], "Exit", true) == 0)
                    break;
                if (commands[0].Length == 0) {
                    continue;
                }
                var a = storage.All();
                bool cFlag = false;
                bool iFlag = false;
                if (commands.Length >= 2) {
                    foreach (var type in types) {
                        if (string.Compare(commands[1], type, true) == 0)
                            cFlag = true;
                    }
                    if (cFlag == false) {
                        Console.WriteLine("{0} is not a valid object type", commands[1]);
                        continue;
                    }
                }
                if (commands.Length >= 3) {
                    foreach (var o in a.Values) {
                        if (string.Compare(o.id, commands[2], true) == 0)
                            iFlag = true;
                    }
                    if (iFlag == false) {
                        Console.WriteLine("Object {0} could not be found", commands[2]);
                        continue;
                    }
                }
                if (string.Compare(commands[0], "All", true) == 0) {
                    if (commands.Length == 1){
                        foreach (var key in a.Keys) {
                            Console.WriteLine("{0} {1}", key, a[key].ToString());
                        }
                    }
                    else {
                        foreach (var key in a.Keys) {
                            if (string.Compare(a[key].type, commands[1], true) == 0) {
                                Console.WriteLine("{0} {1}", key, a[key].ToString());
                            }
                        }
                    }
                }
                else if (string.Compare(commands[0], "Show", true) == 0) {
                    string key = string.Format("{0}.{1}", commands[1], commands[2]);
                    if (a.ContainsKey(key)) {
                        Console.WriteLine(a[key]);
                    }
                    else {
                        Console.WriteLine("Object {0} could not be found", commands[2]);
                    }
                }
                else if (string.Compare(commands[0], "Update", true) == 0) {
                    string key = string.Format("{0}.{1}", commands[1], commands[2]);
                    if (a.ContainsKey(key)) {
                        Console.WriteLine("Please enter the desired parameter to update followed by the new value, separated by a space");
                        string u = Console.ReadLine();
                        string[] up = u.Split(' ', 2);
                        string type = a[key].type;
                        bool pass = false;
                        if (string.Compare(type, "User") == 0)
                            pass = storage.updateObj<User>(key, up[0], up[1]);
                        else if (string.Compare(type, "Item") == 0)
                            pass = storage.updateObj<Item>(key, up[0], up[1]);
                        else if (string.Compare(type, "Inventory") == 0)
                            pass = storage.updateObj<Inventory>(key, up[0], up[1]);
                        else if (string.Compare(type, "BaseClass") == 0)
                            pass = storage.updateObj<BaseClass>(key, up[0], up[1]);
                        if (!pass) {
                            Console.WriteLine("Object has no parameter named {0}", up[0]);
                        }
                        storage.objects[key].date_updated = DateTime.Now;
                        storage.Save();
                    }
                    else {
                        Console.WriteLine("Object {0} could not be found", commands[2]);
                    }
                }
                else if (string.Compare(commands[0], "Create", true) == 0) {
                    if (string.Compare(commands[1], "User", true) == 0) {
                        Console.WriteLine("Required parameter: name");
                        string inp = Console.ReadLine();
                        User u = new User(inp);
                        storage.New(u);
                        storage.Save();
                    }
                    else if (string.Compare(commands[1], "Item", true) == 0) {
                        Console.WriteLine("Please enter item name");
                        string iName = Console.ReadLine();
                        Console.WriteLine("Please enter item description");
                        string iDesc = Console.ReadLine();
                        Console.WriteLine("Please enter item price as a float");
                        string iPrice = Console.ReadLine();
                        Console.WriteLine("Please enter item tags, separated by a space");
                        string iTags = Console.ReadLine();
                        float price = -1f;
                        List<string> tags;
                        if (string.IsNullOrEmpty(iName)) {
                            Console.WriteLine("name is a required parameter");
                            continue;
                        }
                        if (string.IsNullOrEmpty(iDesc)) {
                            iDesc = "";
                        }
                        if (!(string.IsNullOrEmpty(iPrice))) {
                            price = float.Parse(iPrice);
                        }
                        else {
                            price = -1f;
                        }
                        if (!(string.IsNullOrEmpty(iTags))) {
                            string[] tag = iTags.Split(' ');
                            tags = new List<string>(tag);
                        }
                        else {
                            tags = null;
                        }
                        Item i = new Item(iName, iDesc, price, tags);
                        storage.New(i);
                        storage.Save();
                    }
                    else if (string.Compare(commands[1], "Inventory", true) == 0) {
                        Console.WriteLine("Please input desired parameters in order separated by a space");
                        Console.WriteLine("Parameter order: user_id, item_id, quantity");
                        string inp = Console.ReadLine();
                        string[] inp2 = inp.Split(' ', 3);
                        if (inp2.Length < 3) {
                            Console.WriteLine("All parameters are required");
                        }
                        Inventory inv = new Inventory(inp2[0], inp2[1], int.Parse(inp2[2]));
                        storage.New(inv);
                        storage.Save();
                    }
                    else {
                        BaseClass n = new BaseClass();
                        storage.New(n);
                        storage.Save();
                    }
                }
                else if (string.Compare(commands[0], "Delete", true) == 0) {
                    string key = string.Format("{0}.{1}", commands[1], commands[2]);
                    if (a.ContainsKey(key)) {
                        storage.objects.Remove(key);
                        storage.Save();
                    }
                    else {
                        Console.WriteLine("Object {0} could not be found", commands[2]);
                    }
                }
                else if (string.Compare(commands[0], "ClassNames", true) == 0) {
                    List<string> names = new List<string>();
                    foreach (var v in a.Values) {
                        names.Add(v.GetType().ToString());
                    }
                    names = names.Distinct().ToList();
                    string res = string.Join(", ", names);
                    Console.WriteLine(res);
                }
                else {
                    Console.WriteLine("{0} is not a valid command", commands[0]);
                    continue;
                }
                Console.Write(prompt);
            }
        }
    }
}
