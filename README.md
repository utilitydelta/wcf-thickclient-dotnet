# wcf-thickclient-dotnet
Ultra fast .NET n-tier thick client

## Goals
Have you ever needed to build a thick client? Did you add an app server into the architecture? This repo shows you how to implement this. If you have control both the client and server then this is for you.

- Authenticated backend with custom username/password authentication & token management via WCF
- Pure binary transfer over TCP for ultimate speed and capacity
- Designed to be deployed to IIS or the cloud
- Encrypted transport via SSL certificates
- Service driven approach with shared server & client assemblies for developer productivity. Program against a .NET interface instead of WCF service references
