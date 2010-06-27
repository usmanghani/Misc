<?xml version="1.0"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="FacerAzureHost" generation="1" functional="0" release="0" Id="1ebbee71-d2da-42ab-81d7-713d55ed397a" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="FacerAzureHostGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="HttpIn" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/FacerAzureHost/FacerAzureHostGroup/FELoadBalancerHttpIn" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="WebInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/FacerAzureHost/FacerAzureHostGroup/MapWebInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="FELoadBalancerHttpIn">
          <toPorts>
            <inPortMoniker name="/FacerAzureHost/FacerAzureHostGroup/Web/HttpIn" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapWebInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/FacerAzureHost/FacerAzureHostGroup/WebInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="Web" generation="1" functional="0" release="0" software="C:\libfaces\FacerAzureHost\FacerAzureHost\obj\Debug\FacerAzureHost_WebRole\" entryPoint="ucruntime" parameters="Microsoft.ServiceHosting.ServiceRuntime.Internal.WebRoleMain" memIndex="1024" hostingEnvironment="frontendfulltrust">
            <componentports>
              <inPort name="HttpIn" protocol="http" />
            </componentports>
            <resourcereferences>
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
            <eventstreams>
              <eventStream name="Microsoft.ServiceHosting.ServiceRuntime.RoleManager.Critical" kind="Default" severity="Critical" signature="Basic_string" />
              <eventStream name="Microsoft.ServiceHosting.ServiceRuntime.RoleManager.Error" kind="Default" severity="Error" signature="Basic_string" />
              <eventStream name="Critical" kind="Default" severity="Critical" signature="Basic_string" />
              <eventStream name="Error" kind="Default" severity="Error" signature="Basic_string" />
              <eventStream name="Warning" kind="OnDemand" severity="Warning" signature="Basic_string" />
              <eventStream name="Information" kind="OnDemand" severity="Info" signature="Basic_string" />
              <eventStream name="Verbose" kind="OnDemand" severity="Verbose" signature="Basic_string" />
            </eventstreams>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/FacerAzureHost/FacerAzureHostGroup/WebInstances" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyID name="WebInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="c17357fd-da33-4c98-8841-99ba499b42ee" ref="Microsoft.RedDog.Contract\ServiceContract\FacerAzureHostContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="20ad65f2-23a9-41b0-8b27-86ffcb26fdeb" ref="Microsoft.RedDog.Contract\Interface\HttpIn@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/FacerAzureHost/FacerAzureHostGroup/HttpIn" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>