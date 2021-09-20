## ShablePrefix
Simple custom prefix handler on c# for discord bot



## possibilities:

- Discord.NET compatible
- up to 500 servers(actually unlimited count of servers, but response speed decreases)
- Default prefix
- simple to use

## How to use:
To connect custom prefixes to your bot you need:
```cs
using ShablePrefics;
.
.
.
//when bot is ready
private async Task OnReady()
            {
            /*if you connecting Shable prefix first time, 
            before checking you must to create json file for guilds and their prefixes.
            Just write await Prefix.StartShable(), and you don't have to touch it anymore.
            */
                   await Prefix.StartShable();
                   //ready stuff
            }
.
.
.
//your command handler
private async Task HandleCommandAsync(SocketMessage rawMessage)
{
            //your code
            
            /*
            when you're checking for prefix you must replace your default string prefix with
            await Prefix.GetPrefix(guild that requested(Example: contet.Guild), default prefix of your bot)
            */
            if (message.HasStringPrefix(await Prefix.GetPrefix(context.Guild,_defaultPrefics), ref argPos) || !(message.HasMentionPrefix(_client.CurrentUser, ref argPos)))
            {
                        //command responding etc
            }


}
```
