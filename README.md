##Set up du an
Follow tutorial : https://www.youtube.com/watch?v=DgVjEo3OGBI

-  Tao service `dotnet new webapi -n PlatformService`
   Trong thu muc PlatformService cai package: `dotnet add package package-name`

   -  `AutoMapper.Extensions.Microsoft.DependencyInjection`\
   -  `Microsoft.EntityFrameworkCore`
   -  `Microsoft.EntityFrameworkCore.Design`
   -  `Microsoft.EntityFrameworkCore.InMemory`
   -  `Microsoft.EntityFrameworkCore.SqlServe`

-  Tao service `dotnet new webapi -n CommandService`
   Trong thu muc PlatformService cai package: `dotnet add package package-name`
   -  `AutoMapper.Extensions.Microsoft.DependencyInjection`\
   -  `Microsoft.EntityFrameworkCore`
   -  `Microsoft.EntityFrameworkCore.Design`
   -  `Microsoft.EntityFrameworkCore.InMemory`
   -  `Microsoft.EntityFrameworkCore.SqlServe`
      Chạy ứng dụng:
      `dotnet build`
      run dev: `dotnet watch run`
      `dotnet run`
-  App Config Kubernetes mở terminal thư mục k8s: `kubectl apply - f .`
   -  `kubectl rollout restart deployment platform-service-deployment`
   -  `kubectl get service`
   -  ``
