FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy the csproj file and restore the project dependencies 
COPY MovieListApi.csproj ./
RUN dotnet restore "MovieListApi.csproj"

# Copy everything
COPY . ./
# Build and publish a release
RUN dotnet publish "MovieListApi.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "MovieListApi.dll"]