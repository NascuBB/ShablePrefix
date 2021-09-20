using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Discord;
using System.IO;
using ShablePrefics.ServerIds;

namespace ShablePrefics
{
    public static class Prefix
    {
        //!
        private static string ConfigFolder = "Resourses";
        private static string ConfigFile = "Prefixes.json";
        private static string ConfigPath = ConfigFolder + "/" + ConfigFile;

       /// <summary>
       /// Sets prefix to a chosen guild
       /// </summary>
        public static Task SetPrefix(IGuild guild, string prefix)
        {
            string text = "";
            ulong id = guild.Id;
            try
            {
                //checking for directory
                if (!Directory.Exists(ConfigFolder))
                {
                    Directory.CreateDirectory(ConfigFolder);
                }
                //checking if file with prefixes exists
                if (!File.Exists(ConfigPath))
                {
                    //if no, creating a new one and writing first guild with its prefix
                    string[] str = { "test", $"{id}={prefix}" };
                    var rem = new Shable
                    {
                        PrefToId = str
                    };
                    text = JsonConvert.SerializeObject(rem);
                    File.WriteAllText(ConfigPath, text);
                }
                else
                {
                    bool add = false;
                    var item = id.ToString();
                    //deserializing json file with prefixes
                    var obj = JsonConvert.DeserializeObject<Shable>(File.ReadAllText(ConfigPath));
                    //getting list of guilds
                    string[] ids = obj.PrefToId;
                    //searching for requested guild
                    foreach(string str in ids)
                    {
                        if (str.Contains(item))
                        {
                            add = true;
                            break;
                        }

                    }
                    if (add)
                    {
                        
                        //finding index of guild in list
                        int index = Array.FindIndex(ids,x => x.Contains(item));
                        //copying item from list
                        string desiredStr = ids[index];
                        //rosbivaem string on two halfs, one is guild id and second is its prefix
                        string[] half = desiredStr.Split('=');
                        //overwriting prefix
                        string strin = $"{half[0]}={prefix}";
                        //overwriting item
                        ids[index] = strin;
                        //serealizing and rewriting json document
                        var rem = new Shable
                        {
                            PrefToId = ids
                        };
                        text = JsonConvert.SerializeObject(rem);
                        File.WriteAllText(ConfigPath, text);
                    }
                    else
                    {
                        string newId = $"{id}={prefix}";
                        string[] result = Ext.Append(ids, newId);
                        var rem = new Shable
                        {
                            PrefToId = result
                        };
                        text = JsonConvert.SerializeObject(rem);
                        File.WriteAllText(ConfigPath, text);
                    }
                }

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }
        /// <summary>
        /// Gets prefix to a requered guild
        /// </summary>
        /// <returns>Prefix of requested guild</returns>
        public static Task<string> GetPrefix(IGuild guild, string defaultPrefix)
        {
            try
            {
                //checking for directory
                if (!Directory.Exists(ConfigFolder))
                {
                    Directory.CreateDirectory(ConfigFolder);
                }
                //checking if file with prefixes exists
                if (!File.Exists(ConfigPath))
                {
                    throw new Exception("client doesn't have special file. Try set prefix once");
                }
                bool add = false;
                ulong id = guild.Id;
                string item = id.ToString();
                //deserializing json file with prefixes
                var obj = JsonConvert.DeserializeObject<Shable>(File.ReadAllText(ConfigPath));
                //getting list of guilds
                string[] ids = obj.PrefToId;
                foreach (string str in ids)
                {
                    if (str.Contains(item))
                    {
                        add = true;
                        break;
                    }
                    
                }
                if (add)
                {
                    int index = Array.FindIndex(ids, x => x.Contains(item));
                    string desiredStr = ids[index];
                    string[] halfs = desiredStr.Split('=');
                    string prefix = halfs[1];

                    return Task.FromResult(prefix);
                }
                else
                {
                    return Task.FromResult(defaultPrefix);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// removes custom prefix for chosen guild
        /// </summary>
        public static Task RemovePrefix(IGuild guild)
        {

            try
            {

                bool add = false;
                ulong id = guild.Id;
                string item = id.ToString();
                //deserializing json file with prefixes
                var obj = JsonConvert.DeserializeObject<Shable>(File.ReadAllText(ConfigPath));
                //getting list of guilds
                string[] ids = obj.PrefToId;
                foreach (string str in ids)
                {
                    if (str.Contains(item))
                    {
                        int RemIndex = Array.FindIndex(ids, x => x.Contains(item));

                        ids = ids.Where((source, index) => index != RemIndex).ToArray();

                        var rem = new Shable
                        {
                            PrefToId = ids
                        };
                        var text = JsonConvert.SerializeObject(rem);
                        File.WriteAllText(ConfigPath, text);
                        return Task.CompletedTask;
                    }
                }
                if (add == true)
                {
                    throw new Exception("This server has no custom prefix");
                }
                else
                {
                    return Task.CompletedTask;
                }


            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }
    }
}







//File.WriteAllText("prefixes.json", JsonConvert.SerializeObject(rem));
//string jsonData = JsonConvert.SerializeObject(rem);


//if(obj.PrefToId.Contains(id.ToString()))
//{

//    string[] half = previous.Split('=');
//    string oldPref = half[1];
//    string str = half[1].Replace(oldPref,prefix);
//    File.WriteAllText(ConfigPath, $"{half[0]}={str}\"}}");
//}
//else
//{

//    string complete = previous + "\n" + text;
//    File.WriteAllText(ConfigPath, complete);
//}
