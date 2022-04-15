# base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# set work directory from /app (if there is none it creates one)
WORKDIR /telescopeApp

# copy source code over
COPY . .

RUN dotnet clean ./BackEnd/P1-jxm8022.sln
RUN dotnet publish ./BackEnd/TelescopeStoreAPI --configuration Release -o ./BackEnd/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS run

WORKDIR /telescopeApp

COPY --from=build /telescopeApp/BackEnd/publish .

CMD ["dotnet", "TelescopeStoreAPI.dll"]