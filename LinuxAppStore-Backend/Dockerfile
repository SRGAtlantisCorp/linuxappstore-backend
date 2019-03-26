FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["LinuxAppStore-Backend/LinuxAppStore-Backend.csproj", "LinuxAppStore-Backend/"]
RUN dotnet restore "LinuxAppStore-Backend/LinuxAppStore-Backend.csproj"
COPY . .
WORKDIR "/src/LinuxAppStore-Backend"
RUN dotnet build "LinuxAppStore-Backend.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "LinuxAppStore-Backend.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LinuxAppStore-Backend.dll"]