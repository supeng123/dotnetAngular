### step one to set up a project
~~~
1. mkdir skinet
2. cd skinet
3. dotnet new slt
4. dotnet new webapi -o API
5. dotnet sln add API/
6. code .
7. cd API
8. dotnet run
9. dotnet dev-certs https -t
~~~

### check add libs
~~~
dotnet --info
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 6.0.8
dotnet restore

dotnet tool install --global dotnet-ef --version 6.0.8
dotnet tool list -g

dotnet ef -h
dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.8
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 11.0.0
dotnet ef migrations add InitialCreate -o Data/Migrations
dotnet ef database update
~~~

### new extra libs
~~~
dotnet new classlib -o Core
dotnet new classlib -o Infrastructure

dotnet sln add Core/
dotnet sln add Infrastructure/

cd API
dotnet add reference ../Infrastructure

cd ..
cd Infrastructure
dotnet add reference ../Core

cd..
dotnet restore
lsof -i :8080
kill -9 <PID>
~~~
### migration DB
~~~
dotnet ef database drop -p Infrastructure -s API
dotnet ef migrations remove -p Infrastructure -s API

dotnet ef migrations add InitialCreate -p Infrastructure -s API -o Data/Migrations


dotnet ef migrations add IdentityInitial -p Infrastructure -s API -c AppIdentityDbContext -o Identity/Migrations

dotnet ef database drop -p Infrastructure -s API -c StoreContext
~~~
### steps to create new feature(order)
~~~
1.define the entities in Core
2.configure entities in Infrastructure to declare the corelation between properties---> OrderConfiguration.cs, 
3.configure (StoreContext)to set up the tables and using (StoreContextSeed) to inject initial data
4.create migration ---> dotnet ef migrations add OrderEntityAdded -p Infrastructure -s API -c StoreContext
5.create service interface(IorderService) in Core and implement its interface in Infrustructure, then register it in ApplicationServicesExtension.cs
6.create controller (orderSerive), prepare dto and set the mapping relation in MappingProfiles
7.create transaction by using Unitwork and register it in ApplicationServicesExtension.cs
~~~