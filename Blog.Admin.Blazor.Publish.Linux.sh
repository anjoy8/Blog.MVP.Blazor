git pull;
rm -rf .PublishFiles;
dotnet build;
cd Blog.MVP.Blazor.SSR
dotnet publish -o /home/Blog.MVP.Blazor/Blog.MVP.Blazor.SSR/bin/Debug/netcoreapp3.1/publish;
cp -r /home/Blog.MVP.Blazor/Blog.MVP.Blazor.SSR/bin/Debug/netcoreapp3.1/publish .PublishFiles;
echo "Successfully!!!! ^ please see the file .PublishFiles";