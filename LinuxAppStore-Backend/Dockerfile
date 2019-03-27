FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /app

# Open ports
EXPOSE 80
EXPOSE 443

# Copy csproj and restore
COPY *.csproj ./
RUN dotnet restore

# Copy everything and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "LinuxAppStore-Backend.dll"]