#! /bin/sh

# First parameter is build mode, defaults to Debug

MODE=${1:-Debug}
NAME="Finite"

dotnet restore "$NAME.sln"
dotnet build "$NAME.sln" --configuration $MODE

find . -iname "*.Tests.csproj" -type f -exec dotnet test "{}" --configuration $MODE \;

dotnet pack ./src/$NAME --configuration $MODE --output ../../.build
