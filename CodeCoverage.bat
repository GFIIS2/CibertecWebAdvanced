

opencover.console.exe -target:"dotnet.exe" -targetargs:"test -f netcoreapp1.1 -c Release E:\CibertecWeb\Cibertec.Repositories.Tests\Cibertec.Repositories.Tests.csproj" -mergeoutput -hideskipped:File -output:coverage.xml -oldStyle -filter:"+[Cibertec.*]* -[Cibertec.Repositories.Tests*]*" -searchdirs:"E:\CibertecWeb\Cibertec.Repositories.Tests\bin\Release\netcoreapp1.1" -register:user

reportgenerator.exe -reports:coverage.xml -targetdir:coverage -verbosity:Error