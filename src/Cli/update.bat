dotnet tool uninstall -g Cli
dotnet pack
dotnet tool install --global --add-source ./nupkg Cli
