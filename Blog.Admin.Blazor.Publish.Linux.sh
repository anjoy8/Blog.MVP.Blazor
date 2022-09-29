git pull;
rm -rf .PublishFiles;
dotnet build;
cd Blog.MVP.Blazor.SSR
dotnet publish -o /home/Blog.MVP.Blazor/Blog.MVP.Blazor.SSR/bin/Debug/netcoreapp5.0/publish;
cp -r /home/Blog.MVP.Blazor/Blog.MVP.Blazor.SSR/bin/Debug/netcoreapp5.0/publish /home/Blog.MVP.Blazor/.PublishFiles;
echo "Successfully!!!! ^ please see the file .PublishFiles";
