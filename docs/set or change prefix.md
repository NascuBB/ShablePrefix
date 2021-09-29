# How to set or change prefix for a guild
```cs
        [Command("setprefix")]
        public async Task SetPrefixAsync(string prefix)
        {
                //For setting or changing prefix use SetPrefix(Requested guild, new prefix)
                await Prefix.SetPrefix(Context.Guild, prefix);
                await ReplyAsync(":white_check_mark:Done!");
        }
```
