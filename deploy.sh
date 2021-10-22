#!/bin/bash
dotnet nuget push ./src/DsRule/bin/Release/DsRule.$VERSION_NUMBER.nupkg --api-key $NUGET_KEY --source https://api.nuget.org/v3/index.json

