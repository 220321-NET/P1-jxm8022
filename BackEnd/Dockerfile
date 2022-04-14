# base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# set work directory from /app (if there is none it creates one)
WORKDIR /telescopeApp

# copy source code over
COPY . .

RUN dotnet clean P1-jxm8022.sln
RUN dotnet public TelescopeStoreAPI --configuration Release -o ./publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS run

WORKDIR /telescopeApp

COPY --from=build /app/publish .

CMD ["dotnet", "TelescopeStoreAPI.dll"]