docker postgresql container created
 • docker run --name docker_postgres -e POSTGRES_PASSWORD=123456 -d -p 5432:5432 postgres


dotnet ef migrations add init --project ../eReconciliation.DataAccess/eReconciliation.DataAccess.csproj

dotnet ef database update --project ../eReconciliation.DataAccess/eReconciliation.DataAccess.csproj