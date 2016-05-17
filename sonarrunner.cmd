MSBuild.SonarQube.Runner.exe begin /k:"EA2G" /n:"EA2Gliffy" /v:"0.1" /d:sonar.cs.opencover.reportsPaths="%CD%\opencover.xml"
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" /t:Rebuild
"%LOCALAPPDATA%\Apps\OpenCover\OpenCover.Console.exe" -output:"%CD%\opencover.xml" -register:user -target:"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" -targetargs:"eatogliffyTest\bin\Debug\eatogliffyTest.dll"
MSBuild.SonarQube.Runner.exe end