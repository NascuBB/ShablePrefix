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
        /// <param name="guild">Requested guild</param>
        /// <param name="prefix">New prefix</param>
        /// <returns></returns>
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
                    string[] str = { "0", $"{id}⠀{prefix}" };
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

                        //finding index of guild in list
                        int index = Array.FindIndex(ids, x => x.Contains(item));
                        //overwriting prefix
                        string strin = $"{id}⠀{prefix}";
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
                        string newId = $"{id}⠀{prefix}";
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
        /// Checks for json file for prefixes
        /// </summary>
        public static Task StartShable()
        {
            try
            {
                //checking for directory
                if (!Directory.Exists(ConfigFolder))
                {
                    Directory.CreateDirectory(ConfigFolder);
                    //checking if file with prefixes exists
                    if (!File.Exists(ConfigPath))
                    {
                        //if no, creating a new one and writing first guild with its prefix
                        string[] str = { "0" };
                        var rem = new Shable
                        {
                            PrefToId = str
                        };
                        var text = JsonConvert.SerializeObject(rem);
                        File.WriteAllText(ConfigPath, text);
                        return Task.CompletedTask;
                    }
                }
                if (!File.Exists(ConfigPath))
                {

                    string[] str = { "0" };
                    var rem = new Shable
                    {
                        PrefToId = str
                    };
                    var text = JsonConvert.SerializeObject(rem);
                    File.WriteAllText(ConfigPath, text);
                    return Task.CompletedTask;
                }
                return Task.CompletedTask;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets prefix to a requered guild
        /// </summary>
        /// <param name="guild"></param>
        /// <param name="defaultPrefix">Your default prefix</param>
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
                    string prefix = desiredStr.Substring(19);

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
        /// <param name="guild">Requested guild</param>
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
