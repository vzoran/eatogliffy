MSBuild.SonarQube.Runner.exe begin /k:"EA2G" /n:"EA2Gliffy" /v:"0.1" /d:sonar.cs.opencover.reportsPaths="%CD%\opencover.xml"
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" /t:Rebuild
"%LOCALAPPDATA%\Apps\OpenCover\OpenCover.Console.exe" -output:"%CD%\opencover.xml" -register:user -target:"c:\Program Files (x86)\NUnit.org\nunit-console\nunit3-console.exe" -targetargs:"eatogliffyTest\bin\Debug\eatogliffyTest.dll"
MSBuild.SonarQube.Runner.exe end