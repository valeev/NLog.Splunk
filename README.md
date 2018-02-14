NLog.Splunk.Simple
===================

NLog target for sending logs to splunk by /receivers/simple 

[![NuGet](https://img.shields.io/nuget/v/nlog.svg)](https://www.nuget.org/packages/NLog.Splunk.Simple)
[![license](https://img.shields.io/github/license/mashape/apistatus.svg)]()

----------


Getting Started
-------------

Use the GUI or the following command in the Package Manager Console

    Install-Package NLog.Splunk.Simple

Not necessary but sometimes you have to register custom target, try to register it in Global.asax with a next code

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

How to create custom target for NLog:
	https://github.com/nlog/nlog/wiki/How-to-write-a-custom-target
	
