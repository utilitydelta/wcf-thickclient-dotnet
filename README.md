# wcf-thickclient-dotnet
Ultra fast .NET n-tier thick client

## Goals
Do you have the luxury of using .NET on both client and server? This repo shows you how to implement this with WCF. It is designed to get data moving between client & server easily & as fast as possible.

- Authenticated backend with custom username/password authentication & token management via WCF
- Pure binary transfer, GZiped, over TCP for ultimate speed and capacity
- Designed to be deployed to IIS
- Encrypted transport via SSL certificates
- Service driven approach with shared server & client assemblies for developer productivity. Program against a .NET interface instead of WCF service references
- Dependency injection / service based architecture for Test Driven Development

## Server Setup

You'll need to enable some windows features. Install everything under:

- .NET Framework 4.7 Advanced Services\WCF Services
- Internet Information Services\Web Management Tools

Now we use the IIS Manager to generate a self-signed certificate. This is because the WCF service runs over SSL. To do this, open 'Server Certificates' in the IIS Manager. Then select 'Create Self-Signed Certificate' under the 'Actions' panel on the right. Give it a name, store the cert in 'Personal'.

The next step is to create an IIS Application Pool. Go to Application Pools and click 'Add Application Pool'. After you create it, go to 'Advanced Settings' and change the 'Application Pool Identity' to 'Network Service'. You could also use a custom account here. 

Next go to the 'Default Web Site' under 'Sites'. Change the application pool to the one you just created. This is done under 'Basic Settings'. Then go to 'Bindings' and make sure you have 'net.tcp' with binding infomation '808:*'. Now go to 'Advanced Settings' and change 'Enabled Protocols' to 'http,net.tcp'.

Now we need to give the application pool identity access to the self-signed certificate's private key. This is so WCF can encrypt traffic across the endpoint. Do this by: Start -> Run -> 'mmc' -> File -> Add or Remove Snap-ins -> 'Certificates' -> 'Computer account' -> 'Local Computer' -> 'OK'. Now open -> 'Certificates' -> 'Personal' -> 'Certificates' -> Right click on your certificate -> 'All tasks' -> 'Manage private keys' -> 'Add' -> type 'Network Service'. Make sure it has full control and then click OK.

The server runs a WCF endpoint over IIS. Make sure you run Visual Studio as administrator. This will allow you to open a project that targets IIS (the UtilityDelta.Server.Endpoint project). Open up the 'Server.sln' solution. 'UtilityDelta.Server.Endpoint' should open OK. Right click on it, go to properties -> 'Web'. Under servers, make sure the drop down says 'Local IIS' and the Url is 'http://localhost/Service'. Click 'Create Virtual Directory'.

Now go back to your IIS Manager. Open the 'Service' virtual directory and click 'Advanced Settings'. Change the 'Enabled Protocols' to 'http,net.tcp'.

Now you have a virtual directory pointing to your project location. So when you hit 'F5/Play' in Visual Studio, IIS should hook into the dlls that you are debugging. Give it a go :) Your browser should open. Try to open 'Service.svc' and then go to the wsdl: "http://localhost/Service/Service.svc?wsdl".

## Client Setup

Open 'Client.sln' in a seperate Visual Studio instance. Make sure your Server is running in debug, and then hit Play on the client. You should be able to run some operations now.