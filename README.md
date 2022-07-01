# StarlessNight
A [BepInEx](https://github.com/BepInEx/BepInEx/releases) plugin for [20 Minutes Till Dawn](https://store.steampowered.com/app/1966900/20_Minutes_Till_Dawn/).

## Feature

- Makes the fog of war darker
- Horde enemies (BrainMonster, Boomer, Lamprey, EyeMonster and projectile) don't appear through the fog
- XP and Heart pickup don't emit light
- Other source of light remained for now (fire, bosses, bullet impacts)

<img src="icon.png" width="400">

## For modders

- Clone the [repo](https://github.com/NeoKaios/20MTD-StarlessNightMod)
- Open repo in VSCode
- Setup $GameDir variable in *StarlessNightMod.csproj*
- ```dotnet build``` to build and deploy mod
- ```dotnet publish``` to publish a .zip file
