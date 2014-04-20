appcmd.exe set site "Default Web Site" -+bindings.[protocol='net.tcp',bindingInformation='808:*']
appcmd.exe set site "Default Web Site" -+bindings.[protocol='net.pipe',bindingInformation='*']
appcmd.exe set site "Default Web Site" -+bindings.[protocol='net.msmq',bindingInformation='localhost']

appcmd.exe set app "Default Web Site/evals" /enabledProtocols:http,net.pipe,net.tcp,net.msmq
