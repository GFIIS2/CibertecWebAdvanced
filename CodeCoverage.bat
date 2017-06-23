@echo off

opencover.console.exe -target:"dotnet.exe" -targetargs:"test -f netcoreapp1.1 -c Release E:\CibertecWeb\Cibertec.Web.Tests\Cibertec.Web.Tests.csproj" -mergeoutput -hideskipped:File -output:coverage.xml -oldStyle -filter:"+[Cibertec.*]* -[Cibertec.Web.Tests*]*" -searchdirs:"E:\CibertecWeb\Cibertec.Web.Tests\bin\Release\netcoreapp1.1" -register:user

reportgenerator.exe -reports:coverage.xml -targetdir:coverage -verbosity:Error