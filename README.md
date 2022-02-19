# Discord Sharding Test

This repository is used to test Discord's ability of [sharding](https://discord.com/developers/docs/topics/gateway#sharding) which is entirely user controlled.

The idea is to implement a simple bot (in Go, JavaScript, C#, Python) with one command `!ping` that responds `Pong` to the user in a public channel or by private message.
We plan to add the bot to 3 servers/guilds and see what's happening.

## Python

We will not implement a Python bot. The [discord.py](https://github.com/Rapptz/discord.py) library is archived and a viable alternative is currently missing.
