git pull;
rm -rf .PublishFiles;
dotnet build;
dotnet publish -o /home/Blog.Admin.Blazor/Blog.Admin.Blazor/bin/Debug/netstandard2.1;
cp -r /home/Blog.Admin.Blazor/Blog.Admin.Blazor/bin/Debug/netstandard2.1 .PublishFiles;
echo "Successfully!!!! ^ please see the file .PublishFiles";