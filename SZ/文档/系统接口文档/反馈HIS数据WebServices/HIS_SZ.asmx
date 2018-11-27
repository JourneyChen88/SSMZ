<?xml version="1.0" encoding="utf-8"?>
<wsdl:definitions xmlns:http="http://schemas.xmlsoap.org/wsdl/http/" xmlns:soap="http://schemas.xmlsoap.org/wsdl/soap/" xmlns:s="http://www.w3.org/2001/XMLSchema" xmlns:soapenc="http://schemas.xmlsoap.org/soap/encoding/" xmlns:tns="http://tempuri.org/" xmlns:tm="http://microsoft.com/wsdl/mime/textMatching/" xmlns:mime="http://schemas.xmlsoap.org/wsdl/mime/" targetNamespace="http://tempuri.org/" xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">
  <wsdl:types>
    <s:schema elementFormDefault="qualified" targetNamespace="http://tempuri.org/">
      <s:element name="OperationConfirm">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="OperInfo" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="OperationConfirmResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="OperationConfirmResult" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="OperationEnd">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="OperInfo" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="OperationEndResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="OperationEndResult" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="OperCancle">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="ApplyID" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="OperCancleResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="OperCancleResult" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
    </s:schema>
  </wsdl:types>
  <wsdl:message name="OperationConfirmSoapIn">
    <wsdl:part name="parameters" element="tns:OperationConfirm" />
  </wsdl:message>
  <wsdl:message name="OperationConfirmSoapOut">
    <wsdl:part name="parameters" element="tns:OperationConfirmResponse" />
  </wsdl:message>
  <wsdl:message name="OperationEndSoapIn">
    <wsdl:part name="parameters" element="tns:OperationEnd" />
  </wsdl:message>
  <wsdl:message name="OperationEndSoapOut">
    <wsdl:part name="parameters" element="tns:OperationEndResponse" />
  </wsdl:message>
  <wsdl:message name="OperCancleSoapIn">
    <wsdl:part name="parameters" element="tns:OperCancle" />
  </wsdl:message>
  <wsdl:message name="OperCancleSoapOut">
    <wsdl:part name="parameters" element="tns:OperCancleResponse" />
  </wsdl:message>
  <wsdl:portType name="OperAisthServiceSoap">
    <wsdl:operation name="OperationConfirm">
      <documentation xmlns="http://schemas.xmlsoap.org/wsdl/">手术排班确认</documentation>
      <wsdl:input message="tns:OperationConfirmSoapIn" />
      <wsdl:output message="tns:OperationConfirmSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="OperationEnd">
      <documentation xmlns="http://schemas.xmlsoap.org/wsdl/">手术结束</documentation>
      <wsdl:input message="tns:OperationEndSoapIn" />
      <wsdl:output message="tns:OperationEndSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="OperCancle">
      <documentation xmlns="http://schemas.xmlsoap.org/wsdl/">手术取消</documentation>
      <wsdl:input message="tns:OperCancleSoapIn" />
      <wsdl:output message="tns:OperCancleSoapOut" />
    </wsdl:operation>
  </wsdl:portType>
  <wsdl:binding name="OperAisthServiceSoap" type="tns:OperAisthServiceSoap">
    <soap:binding transport="http://schemas.xmlsoap.org/soap/http" style="document" />
    <wsdl:operation name="OperationConfirm">
      <soap:operation soapAction="http://tempuri.org/OperationConfirm" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="OperationEnd">
      <soap:operation soapAction="http://tempuri.org/OperationEnd" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="OperCancle">
      <soap:operation soapAction="http://tempuri.org/OperCancle" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
  </wsdl:binding>
  <wsdl:service name="OperAisthService">
    <wsdl:port name="OperAisthServiceSoap" binding="tns:OperAisthServiceSoap">
      <soap:address location="http://10.0.100.114:8080/SZYYOperService/OperAisthService.asmx" />
    </wsdl:port>
  </wsdl:service>
</wsdl:definitions>