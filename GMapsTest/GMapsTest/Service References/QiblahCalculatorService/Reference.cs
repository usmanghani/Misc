﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GMapsTest.QiblahCalculatorService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="QiblahCalculatorService.IQiblahCalculatorService")]
    public interface IQiblahCalculatorService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQiblahCalculatorService/FindQiblah", ReplyAction="http://tempuri.org/IQiblahCalculatorService/FindQiblahResponse")]
        double FindQiblah(string address, bool gpsUsed);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IQiblahCalculatorServiceChannel : GMapsTest.QiblahCalculatorService.IQiblahCalculatorService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class QiblahCalculatorServiceClient : System.ServiceModel.ClientBase<GMapsTest.QiblahCalculatorService.IQiblahCalculatorService>, GMapsTest.QiblahCalculatorService.IQiblahCalculatorService {
        
        public QiblahCalculatorServiceClient() {
        }
        
        public QiblahCalculatorServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public QiblahCalculatorServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public QiblahCalculatorServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public QiblahCalculatorServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public double FindQiblah(string address, bool gpsUsed) {
            return base.Channel.FindQiblah(address, gpsUsed);
        }
    }
}