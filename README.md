# wcf-thickclient-dotnet
Ultra fast .NET n-tier thick client

## Goals
Do you have the luxury of using .NET on both client and server? This repo shows you how to implement this with WCF. It is designed to get data moving between client & server easily & as fast as possible.

- Authenticated backend with custom username/password authentication & token management via WCF
- Pure binary transfer over TCP for ultimate speed and capacity
- Designed to be deployed to IIS
- Encrypted transport via SSL certificates
- Service driven approach with shared server & client assemblies for developer productivity. Program against a .NET interface instead of WCF service references
- Dependency injection / service based architecture for Test Driven Development
