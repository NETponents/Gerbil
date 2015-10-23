﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Gerbil
{
    namespace Gerbil_DataService
    {
        namespace Models
        {
            namespace Devices
            {
                /// <summary>
                /// Partial data model to represent a discoverable device connected to the network.
                /// </summary>
                public partial class Device
                {
                    private SecurityLevel machineSecurityRel;
                    private string networkName;
                    private IPAddress deviceAddress;
                    protected Dictionary<string, string> properties;

                    public Device(IPAddress dAddr)
                    {
                        deviceAddress = dAddr;
                        networkName = "";
                        machineSecurityRel = SecurityLevel.Undetermined;
                        properties = new Dictionary<string, string>();
                    }
                    public Device(IPAddress dAddr, string mName)
                    {
                        deviceAddress = dAddr;
                        networkName = mName;
                        machineSecurityRel = SecurityLevel.Undetermined;
                        properties = new Dictionary<string, string>();
                    }
                    public Device(IPAddress dAddr, string mName, SecurityLevel secState)
                    {
                        deviceAddress = dAddr;
                        networkName = mName;
                        machineSecurityRel = secState;
                        properties = new Dictionary<string, string>();
                    }
                    public IPAddress getDeviceIPAddress()
                    {
                        return deviceAddress;
                    }
                    public string getDeviceNetworkName()
                    {
                        return networkName;
                    }
                    public SecurityLevel getDeviceSecurityLevel()
                    {
                        return machineSecurityRel;
                    }
                    public void setDeviceSecurityLevel(SecurityLevel sl)
                    {
                        machineSecurityRel = sl;
                    }
                }
                /// <summary>
                /// Data model to represent a computer such as a PC or Mac.
                /// </summary>
                public class Computer : Device
                {
                    public Computer(IPAddress dAddr)
                        : base(dAddr)
                    {

                    }
                    public Computer(IPAddress dAddr, string mName)
                        : base(dAddr, mName)
                    {

                    }
                    public Computer(IPAddress dAddr, string mName, SecurityLevel secState)
                        : base(dAddr, mName, secState)
                    {

                    }
                    public Computer(IPAddress dAddr, string mName, SecurityLevel secState, string operatingSystemClass)
                        : base(dAddr, mName, secState)
                    {
                        properties.Add("OSCLASS", operatingSystemClass);
                    }
                }
                /// <summary>
                /// Data model to represent networking equipment such as routers and switches.
                /// </summary>
                public class NetworkDevice : Device
                {
                    public NetworkDevice(IPAddress dAddr)
                        : base(dAddr)
                    {

                    }
                    public NetworkDevice(IPAddress dAddr, string mName)
                        :base(dAddr, mName)
                    {

                    }
                    public NetworkDevice(IPAddress dAddr, string mName, SecurityLevel secState)
                        : base(dAddr, mName, secState)
                    {

                    }
                    public NetworkDevice(IPAddress dAddr, string mName, SecurityLevel secState, string netDeviceType)
                        : base(dAddr, mName, secState)
                    {
                        properties.Add("NETDEVICETYPE", netDeviceType);
                    }
                }
                /// <summary>
                /// Enumerated value to show discovered relative security level of associated device.
                /// </summary>
                public enum SecurityLevel
                {
                    Undetermined,
                    None,
                    Low,
                    Medium,
                    High,
                    Locked
                };
                /// <summary>
                /// Enumerated value to show function of associated device.
                /// </summary>
                public enum DeviceFunction
                {
                    Server,
                    Router,
                    Client
                };
            }
        }
        namespace Databases
        {
            class dbOStoServices
            {
                private Dictionary<string, string[]> dataholder;

                public dbOStoServices()
                {
                    dataholder = new Dictionary<string, string[]>();
                }
                public void insertRecord(string OSName, string[] activeServices)
                {
                    dataholder.Add(OSName, activeServices);
                }
                public string[][] getServicesForOS(string osName)
                {
                    List<string[]> serviceList = new List<string[]>();
                    foreach(KeyValuePair<string,string[]> i in dataholder)
                    {
                        if(i.Key == osName)
                        {
                            serviceList.Add(i.Value);
                        }
                    }
                    return serviceList.ToArray();
                }
                public Dictionary<string, string[]> getRawDB()
                {
                    return dataholder;
                }
            }
        }
    }
}
