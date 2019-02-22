FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["ExampleForKubernetes/ExampleForKubernetes.csproj", "ExampleForKubernetes/"]
RUN dotnet restore "ExampleForKubernetes/ExampleForKubernetes.csproj"
COPY . .
WORKDIR "/src/ExampleForKubernetes"
RUN dotnet build "ExampleForKubernetes.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ExampleForKubernetes.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ExampleForKubernetes.dll"]