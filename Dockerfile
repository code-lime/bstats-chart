# Set the base image as the .NET 8.0 SDK (this includes the runtime)
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build-env

# Copy everything and publish the release (publish implicitly restores and builds)
COPY . ./
RUN dotnet publish ./src/DotNet.GitHubAction/DotNet.GitHubAction.csproj -c Release -o out --no-self-contained

# Label as GitHub action
LABEL com.github.actions.name="bStats Chart"
LABEL com.github.actions.description="Create custom chart image from bstats.org"
LABEL com.github.actions.icon="bar-chart-2"
LABEL com.github.actions.color="blue"

# Relayer the .NET SDK, anew with the build output
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine
COPY --from=build-env /out .
ENTRYPOINT [ "dotnet", "/DotNet.GitHubAction.dll" ]
