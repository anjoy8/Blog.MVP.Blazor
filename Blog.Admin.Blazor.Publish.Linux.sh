git pull;
rm -rf .PublishFiles;
dotnet build;
dotnet publish -o /home/Blog.Admin.Blazor/Blog.Admin.Blazor/bin/Release/netstandard2.1/publish;
cp -r /home/Blog.Admin.Blazor/Blog.Admin.Blazor/bin/Release/netstandard2.1/publish .PublishFiles;
echo "Successfully!!!! ^ please see the file .PublishFiles";