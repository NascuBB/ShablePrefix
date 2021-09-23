# How to remove prefix from guild


```cs
        [Command("setDefaultPrefix")]
        public async Task DefaultPrefix()
        {
            //To remove prefix use RemovePrefix(Requested guild)
            await Prefix.RemovePrefix(Context.Guild);
            await ReplyAsync(:white_check_mark:Done!)
        }
```
