# NavigatorAttractionsAPI

[![This image on DockerHub](https://img.shields.io/docker/pulls/stuartshay/navigator-attractions-api.svg)](https://hub.docker.com/r/stuartshay/navigator-attractions-api/)

[![Deploy Linux Container](https://github.com/stuartshay/NavigatorAttractionsAPI/actions/workflows/deploy-container.yml/badge.svg)](https://github.com/stuartshay/NavigatorAttractionsAPI/actions/workflows/deploy-container.yml)

```
L: admin
P: SonarAdmin@2022

dotnet sonarscanner begin /k:"NavigatorAttractionsAPI" /d:sonar.host.url="http://node-2:9100"  /d:sonar.login="sqp_c6dd5d7a747e1bc7db6247073435b43465b1f168"
dotnet build --no-incremental
dotnet test --collect "Code Coverage"
dotnet sonarscanner end /d:sonar.login="sqp_c6dd5d7a747e1bc7db6247073435b43465b1f168"

```
