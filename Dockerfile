#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5050 
EXPOSE 5001 
EXPOSE 80 
EXPOSE 443 

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Blog.MVP.Blazor.SSR/Blog.MVP.Blazor.SSR.csproj", "Blog.MVP.Blazor.SSR/"]
RUN dotnet restore "Blog.MVP.Blazor.SSR/Blog.MVP.Blazor.SSR.csproj"
COPY . .
WORKDIR "/src/Blog.MVP.Blazor.SSR"
RUN dotnet build "Blog.MVP.Blazor.SSR.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blog.MVP.Blazor.SSR.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blog.MVP.Blazor.SSR.dll"]
