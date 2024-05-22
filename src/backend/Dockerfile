FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /src
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /src
COPY --from=build-env /src/out .
ENTRYPOINT ["dotnet", "OurForum.Backend.dll"]
