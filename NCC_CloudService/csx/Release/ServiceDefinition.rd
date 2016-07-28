<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="NCC_CloudService" generation="1" functional="0" release="0" Id="46afc6d6-1a0d-4867-a4a6-6966230bab68" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="NCC_CloudServiceGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="Control.UI:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/NCC_CloudService/NCC_CloudServiceGroup/LB:Control.UI:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="Control.UI:APPINSIGHTS_INSTRUMENTATIONKEY" defaultValue="">
          <maps>
            <mapMoniker name="/NCC_CloudService/NCC_CloudServiceGroup/MapControl.UI:APPINSIGHTS_INSTRUMENTATIONKEY" />
          </maps>
        </aCS>
        <aCS name="Control.UI:Conexao" defaultValue="">
          <maps>
            <mapMoniker name="/NCC_CloudService/NCC_CloudServiceGroup/MapControl.UI:Conexao" />
          </maps>
        </aCS>
        <aCS name="Control.UI:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/NCC_CloudService/NCC_CloudServiceGroup/MapControl.UI:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="Control.UI:URLSite" defaultValue="">
          <maps>
            <mapMoniker name="/NCC_CloudService/NCC_CloudServiceGroup/MapControl.UI:URLSite" />
          </maps>
        </aCS>
        <aCS name="Control.UIInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/NCC_CloudService/NCC_CloudServiceGroup/MapControl.UIInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:Control.UI:Endpoint1">
          <toPorts>
            <inPortMoniker name="/NCC_CloudService/NCC_CloudServiceGroup/Control.UI/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapControl.UI:APPINSIGHTS_INSTRUMENTATIONKEY" kind="Identity">
          <setting>
            <aCSMoniker name="/NCC_CloudService/NCC_CloudServiceGroup/Control.UI/APPINSIGHTS_INSTRUMENTATIONKEY" />
          </setting>
        </map>
        <map name="MapControl.UI:Conexao" kind="Identity">
          <setting>
            <aCSMoniker name="/NCC_CloudService/NCC_CloudServiceGroup/Control.UI/Conexao" />
          </setting>
        </map>
        <map name="MapControl.UI:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/NCC_CloudService/NCC_CloudServiceGroup/Control.UI/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapControl.UI:URLSite" kind="Identity">
          <setting>
            <aCSMoniker name="/NCC_CloudService/NCC_CloudServiceGroup/Control.UI/URLSite" />
          </setting>
        </map>
        <map name="MapControl.UIInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/NCC_CloudService/NCC_CloudServiceGroup/Control.UIInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="Control.UI" generation="1" functional="0" release="0" software="C:\Users\Noel\Source\Repos\control\NCC_CloudService\csx\Release\roles\Control.UI" entryPoint="base\x86\WaHostBootstrapper.exe" parameters="base\x86\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="APPINSIGHTS_INSTRUMENTATIONKEY" defaultValue="" />
              <aCS name="Conexao" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="URLSite" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;Control.UI&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;Control.UI&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/NCC_CloudService/NCC_CloudServiceGroup/Control.UIInstances" />
            <sCSPolicyUpdateDomainMoniker name="/NCC_CloudService/NCC_CloudServiceGroup/Control.UIUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/NCC_CloudService/NCC_CloudServiceGroup/Control.UIFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="Control.UIUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="Control.UIFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="Control.UIInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="04385ef9-7008-4d79-879a-b210b1889d3f" ref="Microsoft.RedDog.Contract\ServiceContract\NCC_CloudServiceContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="d3012c4f-4b21-422b-bd87-58755f4c38e3" ref="Microsoft.RedDog.Contract\Interface\Control.UI:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/NCC_CloudService/NCC_CloudServiceGroup/Control.UI:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>