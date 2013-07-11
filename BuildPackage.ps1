$env:Path = $env:Path + ";C:\Windows\Microsoft.NET\Framework\v4.0.30319"
MSBuild.exe Tumbaga.sln /p:Configuration=Release /p:Platform="Any CPU" /target:Rebuild
.\.nuget\NuGet.exe pack .\Tumbaga\Tumbaga.nuspec