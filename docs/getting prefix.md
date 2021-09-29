# How to get prefix of guild
Maybe it would be a ping command or something else.To get prefix you must:
```cs
        //command example (little bit stupid, but nevermind)
        [Command("getPrefix")]
        public async Task GetPrefix()
        {
            //to get prefix use GetPrefix
            string prefix = await Prefix.GetPrefix(Context.Guild, _prefics);
            await ReplyAsync($"My prefix on this server is: `{prefix}`");
        }
```
