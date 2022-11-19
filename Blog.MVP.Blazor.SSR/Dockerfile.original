FROM swr.cn-south-1.myhuaweicloud.com/mcr/aspnet:3.1-alpine
WORKDIR /app
COPY . . 
EXPOSE 5050 
EXPOSE 5001 
EXPOSE 80 
ENTRYPOINT ["dotnet", "Blog.MVP.Blazor.SSR.dll","-b","0.0.0.0"]