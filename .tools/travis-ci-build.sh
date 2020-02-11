#!/bin/sh
echo "Executing MSBuild DLL begin command..."
dotnet .tools/sonar/SonarScanner.MSBuild.dll begin /o:${SONAR_ORGANIZATION} /k:${SONAR_PROJECT} /d:sonar.cs.vstest.reportsPaths="**/TestResults/*.trx" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.verbose=true /d:sonar.login=${SONAR_TOKEN} /d:sonar.language="cs" /d:sonar.exclusions="**/bin/**/*,**/obj/**/*" /d:sonar.coverage.exclusions="eaw-build.test/**,**/*Tests.cs,**/TestUtility.cs" /d:sonar.cs.opencover.reportsPaths="${dir}\lcov.opencover.xml"
echo "Running build..."
dotnet build eaw-build.sln
echo "Running tests..."
dotnet test eaw-build.test/eaw-build.test.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=\"opencover,lcov\" /p:CoverletOutput=../lcov
echo "Executing MSBuild DLL end command..."
dotnet .tools/sonar/SonarScanner.MSBuild.dll end /d:sonar.login=${SONAR_TOKEN}