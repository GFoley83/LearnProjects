<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="LearnAzureWebsites" generation="1" functional="0" release="0" Id="09cbea17-51ea-4cf4-99e0-59db3952ed41" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="LearnAzureWebsitesGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="MyWebRole:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/LearnAzureWebsites/LearnAzureWebsitesGroup/LB:MyWebRole:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="MyWebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/LearnAzureWebsites/LearnAzureWebsitesGroup/MapMyWebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="MyWebRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/LearnAzureWebsites/LearnAzureWebsitesGroup/MapMyWebRoleInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:MyWebRole:Endpoint1">
          <toPorts>
            <inPortMoniker name="/LearnAzureWebsites/LearnAzureWebsitesGroup/MyWebRole/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapMyWebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/LearnAzureWebsites/LearnAzureWebsitesGroup/MyWebRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapMyWebRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/LearnAzureWebsites/LearnAzureWebsitesGroup/MyWebRoleInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="MyWebRole" generation="1" functional="0" release="0" software="C:\Users\johnmor\Documents\GitHub\LearnProjects\LearnAzureWebsites\LearnAzureWebsites\LearnAzureWebsites\csx\Debug\roles\MyWebRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;MyWebRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;MyWebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/LearnAzureWebsites/LearnAzureWebsitesGroup/MyWebRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/LearnAzureWebsites/LearnAzureWebsitesGroup/MyWebRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/LearnAzureWebsites/LearnAzureWebsitesGroup/MyWebRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="MyWebRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="MyWebRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="MyWebRoleInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="6f2e5bc0-e757-4cac-8504-63bfb440d8ab" ref="Microsoft.RedDog.Contract\ServiceContract\LearnAzureWebsitesContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="326a6091-839b-4c12-b3a3-48401e8e7732" ref="Microsoft.RedDog.Contract\Interface\MyWebRole:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/LearnAzureWebsites/LearnAzureWebsitesGroup/MyWebRole:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>