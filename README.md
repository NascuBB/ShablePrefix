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
//command handler
private async Task HandleCommandAsync(SocketMessage rawMessage)
{
            //your code

            //when you're checking for prefix 
            if (message.HasStringPrefix(await Prefix.GetPrefix(context.Guild,_prefics), ref argPos) || !(message.HasMentionPrefix(_client.CurrentUser, ref argPos)))
            {
            
                    
            }


}
```
