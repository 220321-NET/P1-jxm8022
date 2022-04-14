# base image
FROM mcr.microsoft.com/dotnet/sdk:6.0

# set work directory from /app (if there is none it creates one)
WORKDIR /telescopeApp

# copy source code over
COPY ../ .

RUN dotnet build
RUN dotnet public TelescopeStoreAPI --configuration Debug -o ./publish

EXPOSE 7100

CMD ["dotnet", "./publish/TelescopeStoreAPI.dll"]