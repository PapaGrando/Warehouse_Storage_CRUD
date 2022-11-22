# Warehouse_Storage_CRUD
Study ASP.NET Core project, simulates warehouse storage work (FrontEnd in progress) <br/>
Powered by 
- .NET 6,
- PostgreSQL, EF Core,
- ASP.NET Core WebApi,
- ~~RazorPages~~,
- React (in Progress)
- Docker

# How to Run/Test
**You need instaled Docker, .Net 6+ SDK**
<details><summary>How install dev certificates for https</summary>
<p>

####   see ```StorageWebApi/docker-compose.yml``` for more info

####   You can use certificates of Letsencrypt - https://letsencrypt.org/
####   or via more simple way -

```
  dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p { password here }
  dotnet dev-certs https --trust
```

</p>
</details>

 - Clone project
 - ```cd /StorageWebApi```
 - ```dotnet publish -c Debug``` - in Release swagger disabled
 - ```cd bin\Release\net6.0\publish\pub```
 - ```docker-compose up```
 
 Open browser - ```localhost:5001/swagger``` and you should see:
 ![image](https://user-images.githubusercontent.com/36087533/199022508-ee7071aa-785b-4c79-9295-c667ecf3712e.png)

# Enjoy
