MSBuild.SonarQube.Runner.exe begin /k:"EA2G" /n:"EA2Gliffy" /v:"0.1" /d:sonar.cs.vscoveragexml.reportsPaths="%CD%\VisualStudio.coveragexml"
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" /t:Rebuild
"%VSINSTALLDIR%\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe" collect /output:"%CD%\VisualStudio.coverage" 
"%VSINSTALLDIR%\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" "UnitTestProject1\bin\Debug\eatogliffyTest.dll"
"%VSINSTALLDIR%\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe" analyze /output:"%CD%\VisualStudio.coveragexml" "%CD%\VisualStudio.coverage"
MSBuild.SonarQube.Runner.exe end