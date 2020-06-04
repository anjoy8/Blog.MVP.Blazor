git pull;
rm -rf .PublishFiles;
dotnet build;
dotnet publish -o /home/Blog.MVP.Blazor/Blog.MVP.Blazor/bin/Release/netstandard2.1/publish;
cp -r /home/Blog.MVP.Blazor/Blog.MVP.Blazor/bin/Release/netstandard2.1/publish .PublishFiles;
echo "Successfully!!!! ^ please see the file .PublishFiles";