FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
COPY --from=build-env /app/out .
# Allows the container to run as read-only
ENV DOTNET_EnableDiagnostics=0
ENTRYPOINT [ "dotnet", "ExpenseService.Api.dll" ]
