dotnet sonarscanner begin /k:"NavigatorAttractionsAPI" /d:sonar.host.url="http://node-2:9100" /d:sonar.cs.vscoveragexml.reportsPaths=coverage.xml /d:sonar.login="squ_fa2e49f4466717f9557d4801d252f6a3eabe54b4"
dotnet build 
dotnet-coverage collect 'dotnet test' -f xml  -o 'coverage.xml'
dotnet sonarscanner end /d:sonar.login="squ_fa2e49f4466717f9557d4801d252f6a3eabe54b4"