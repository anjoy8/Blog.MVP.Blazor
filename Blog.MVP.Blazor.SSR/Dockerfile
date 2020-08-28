FROM swr.cn-south-1.myhuaweicloud.com/mcr/aspnet:3.1-alpine

ENV certPassword 123456
 
RUN openssl genrsa -des3 -passout pass:${certPassword} -out server.key 2048
RUN openssl rsa -passin pass:${certPassword} -in server.key -out server.key
RUN openssl req -sha256 -new -key server.key -out server.csr -subj '/CN=localhost'
RUN openssl x509 -req -sha256 -days 365 -in server.csr -signkey server.key -out server.crt
RUN openssl pkcs12 -export -out server.pfx -inkey server.key -in server.crt -certfile server.crt -passout pass:${certPassword}

WORKDIR /app
COPY . . 
EXPOSE 5050 
ENTRYPOINT ["dotnet", "Blog.MVP.Blazor.SSR.dll","-b","0.0.0.0"]