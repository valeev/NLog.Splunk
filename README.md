NLog.Splunk.Simple
===================

NLog target for sending logs to splunk by /receivers/simple

----------


Getting Started
-------------

Use the GUI or the following command in the Package Manager Console

    Install-Package NLog.Splunk.Simple

Then add it to your solution

    ConfigurationItemFactory.Default.Targets.RegisterDefinition("SplunkSimple", typeof(SplunkSimple));

----------

Configuration
-------------

    <target type="SplunkSimple" name="SplunkRestInfo" host="{SPLUNK_SERVER_URL}" username="{USERNAME}" password="{PASSWORD}"
              source="{SOURCE_IN_SPLUNK}" sourceType="{SOURCE_TYPE}"
              layout="${longdate}|${level:uppercase=true}|${logger}|${message} ${exception:format=tostring}">
    </target>

Don't forget to add your rule

    <logger name="*" writeTo="SplunkRestInfo" minLevel="Info" />
