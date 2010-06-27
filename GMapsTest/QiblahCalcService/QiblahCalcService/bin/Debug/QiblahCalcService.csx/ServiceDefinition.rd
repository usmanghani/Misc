<?xml version="1.0"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="QiblahCalcService" generation="1" functional="0" release="0" Id="a3134460-4811-4b99-983d-253d3b44ea1a" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="QiblahCalcServiceGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="HttpIn" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/QiblahCalcService/QiblahCalcServiceGroup/FELoadBalancerHttpIn" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="Web:GoogleMapsApiKey" defaultValue="">
          <maps>
            <mapMoniker name="/QiblahCalcService/QiblahCalcServiceGroup/MapWeb:GoogleMapsApiKey" />
          </maps>
        </aCS>
        <aCS name="WebInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/QiblahCalcService/QiblahCalcServiceGroup/MapWebInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="FELoadBalancerHttpIn">
          <toPorts>
            <inPortMoniker name="/QiblahCalcService/QiblahCalcServiceGroup/Web/HttpIn" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapWeb:GoogleMapsApiKey" kind="Identity">
          <setting>
            <aCSMoniker name="/QiblahCalcService/QiblahCalcServiceGroup/Web/GoogleMapsApiKey" />
          </setting>
        </map>
        <map name="MapWebInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/QiblahCalcService/QiblahCalcServiceGroup/WebInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="Web" generation="1" functional="0" release="0" software="C:\Users\ughani\Desktop\GMapsTest\QiblahCalcService\QiblahCalcService\obj\Debug\QiblahCalcWeb\" entryPoint="ucruntime" parameters="Microsoft.ServiceHosting.ServiceRuntime.Internal.WebRoleMain" memIndex="1024" hostingEnvironment="frontend">
            <componentports>
              <inPort name="HttpIn" protocol="http" />
            </componentports>
            <settings>
              <aCS name="GoogleMapsApiKey" defaultValue="" />
            </settings>
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
            <sCSPolicyIDMoniker name="/QiblahCalcService/QiblahCalcServiceGroup/WebInstances" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyID name="WebInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="d84246df-ebf9-4c4e-9778-6660ffb37f83" ref="Microsoft.RedDog.Contract\ServiceContract\QiblahCalcServiceContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="52e12e87-cbd7-468c-a80f-46e97bdb3e18" ref="Microsoft.RedDog.Contract\Interface\HttpIn@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/QiblahCalcService/QiblahCalcServiceGroup/HttpIn" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>