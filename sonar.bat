dotnet sonarscanner begin /k:"NavigatorAttractionsAPI" /d:sonar.host.url="http://node-2:9100"  /d:sonar.cs.vscoveragexml.reportsPaths=coverage.xml /d:sonar.login="sqp_f87e7196701050d8bab7c6b673d1bd5720d0e7ee"
dotnet build
dotnet-coverage collect 'dotnet test' -f xml  -o 'coverage.xml'
dotnet sonarscanner end /d:sonar.login="sqp_f87e7196701050d8bab7c6b673d1bd5720d0e7ee"
