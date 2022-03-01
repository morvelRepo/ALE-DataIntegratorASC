// Decompiled with JetBrains decompiler
// Type: DataIntegratorASC.Bussiness.Catalogos
// Assembly: DataIntegratorASC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CACEFAEB-425A-4474-9813-A6D977B283DD
// Assembly location: C:\Users\morat\OneDrive\Escritorio\DataIntegratorASC.exe

using DataIntegratorASC.Clases;
using DataIntegratorASC.DomainModel;
using DataIntegratorASC.Objetos;
using Microsoft.CSharp.RuntimeBinder;
using NucleoBase.Core;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;

namespace DataIntegratorASC.Bussiness
{
  public class Catalogos
  {
    public bool Import()
    {
      try
      {
        bool flag = false;
        new Bancos().ObtieneTipoCambioDia();
        foreach (SocioNegocio oSoc in this.ArmaListaSociosNegocio())
        {
          if (this.CreateSapDoc(oSoc))
          {
            if (oSoc.oEstatus.sMensaje != string.Empty)
              Utils.GuardarBitacora("Socio correcto: " + oSoc.oEstatus.sMensaje);
          }
          else
            Utils.GuardarBitacora("Ocurrio un error al agregar al BP: " + oSoc.sCardCode + ", Socio de Negocio:" + oSoc.sCardName + ", Base:" + oSoc.sBase + ". =>" + oSoc.oEstatus.sMensaje);
        }
        return flag;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private bool CreateSapDoc(SocioNegocio oSoc)
    {
      try
      {
        bool sapDoc = true;
        int num1 = 0;
        
        
        SAPbobsCOM.BusinessPartners businessPartners = MyGlobals.oCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners);
        if (oSoc.sLicTradNumRFC != string.Empty)
        {
          string empty1 = string.Empty;
          int num2 = -1;
          if (oSoc.oTipoBP == BoCardTypes.cSupplier)
            num2 = this.ValidaExisteProveedor(oSoc.sLicTradNumRFC, oSoc.sCardName, ref empty1);
          else if (oSoc.oTipoBP == BoCardTypes.cCustomer)
            num2 = this.ValidaExisteCliente(oSoc.sLicTradNumRFC, oSoc.sCardName, ref empty1);
          switch (num2)
          {
            case 0:
              if (oSoc.sBase == "TLC")
              {
                // ISSUE: reference to a compiler-generated method
                businessPartners.UserFields.Fields.Item((object) "U_IdCorridorT").Value = (object) oSoc.iCompanyKeyOri.S();
              }
              else
              {
                // ISSUE: reference to a compiler-generated method
                businessPartners.UserFields.Fields.Item((object) "U_IdCorridorM").Value = (object) oSoc.iCompanyKeyOri.S();
              }
              businessPartners.CardCode = oSoc.sCardCode;
              businessPartners.CardName = oSoc.sCardName;
              businessPartners.CardType = oSoc.oTipoBP;
              businessPartners.FederalTaxID = oSoc.sLicTradNumRFC;
              businessPartners.Currency = oSoc.sCurrency;
              businessPartners.Phone1 = oSoc.sPhone1;
              businessPartners.Phone2 = oSoc.sPhone2;
              businessPartners.Fax = oSoc.sFax;
              businessPartners.GroupCode = oSoc.iGroupCode;
              if (oSoc.sLicTradNumRFC.Length == 12)
                businessPartners.CompanyPrivate = BoCardCompanyTypes.cPrivate;
              else if (oSoc.sLicTradNumRFC.Length == 13)
                businessPartners.CompanyPrivate = BoCardCompanyTypes.cCompany;
              businessPartners.Cellular = "";
              businessPartners.EmailAddress = oSoc.sEmail;
              businessPartners.Website = oSoc.sIntrntSite;
              businessPartners.UnifiedFederalTaxID = oSoc.sVatInUnCmp;
              businessPartners.Notes = oSoc.sNotes;
              businessPartners.AliasName = oSoc.sAliasName;
              if (oSoc.sQryGroup1 == "Y")
              {
                // ISSUE: reference to a compiler-generated method
                businessPartners.set_Properties(1, BoYesNoEnum.tYES);
              }
              if (oSoc.sQryGroup2 == "Y")
              {
                // ISSUE: reference to a compiler-generated method
                businessPartners.set_Properties(2, BoYesNoEnum.tYES);
              }
              if (oSoc.oLstDir != null)
              {
                for (int index = 0; index < oSoc.oLstDir.Count; ++index)
                {
                  businessPartners.Addresses.AddressType = oSoc.oLstDir[index].oAdressType;
                  businessPartners.Addresses.AddressName = oSoc.oLstDir[index].oAdressType == BoAddressType.bo_ShipTo ? "ENTREGA" : "FISCAL";
                  businessPartners.Addresses.Street = oSoc.oLstDir[index].sStreetNumber;
                  businessPartners.Addresses.City = oSoc.oLstDir[index].sCiudad;
                  businessPartners.Addresses.State = oSoc.oLstDir[index].sState;
                  businessPartners.Addresses.Country = oSoc.oLstDir[index].sCountry;
                  businessPartners.Addresses.County = oSoc.oLstDir[index].sCounty;
                  businessPartners.Addresses.StreetNo = oSoc.oLstDir[index].sStreetNo;
                  businessPartners.Addresses.BuildingFloorRoom = oSoc.oLstDir[index].sBuilding;
                  businessPartners.Addresses.Block = oSoc.oLstDir[index].sBlockColonia;
                  businessPartners.Addresses.ZipCode = oSoc.oLstDir[index].sCodigoPostal;
                  // ISSUE: reference to a compiler-generated method
                  businessPartners.Addresses.Add();
                }
              }
              // ISSUE: reference to a compiler-generated method
              num1 = businessPartners.Add();
              break;
            case 1:
              // ISSUE: reference to a compiler-generated method
              if (businessPartners.GetByKey(empty1))
              {
                if (oSoc.sBase == "TLC")
                {
                  // ISSUE: reference to a compiler-generated method
                  businessPartners.UserFields.Fields.Item((object) "U_IdCorridorT").Value = (object) oSoc.iCompanyKeyOri.S();
                }
                else
                {
                  // ISSUE: reference to a compiler-generated method
                  businessPartners.UserFields.Fields.Item((object) "U_IdCorridorM").Value = (object) oSoc.iCompanyKeyOri.S();
                }
                // ISSUE: reference to a compiler-generated method
                num1 = businessPartners.Update();
                break;
              }
              break;
          }
          string empty2 = string.Empty;
          if (num1 != 0)
          {
            oSoc.oEstatus.iEstatus = 0;
            // ISSUE: reference to a compiler-generated method
            // ISSUE: reference to a compiler-generated method
            throw new Exception("Error al guardar el documento en SAP.  [" + MyGlobals.oCompany.GetLastErrorCode().S() + "] - " + MyGlobals.oCompany.GetLastErrorDescription());
          }
          if (num2 == 0)
          {
            sapDoc = true;
            oSoc.oEstatus.iEstatus = 1;
            oSoc.oEstatus.sMensaje = "Se creo el socio de negocios. CardCode[" + oSoc.sCardCode + "] - CardName[" + oSoc.sCardName + "] - Tabla[OCRD], Base:" + oSoc.sBase;
          }
        }
        return sapDoc;
      }
      catch (Exception ex)
      {
        string empty = string.Empty;
        string str = "Error al importar Socio de Negocios:" + oSoc.sCardName + ", Base:" + oSoc.sBase + ", Tabla[" + oSoc.oEstatus.sTabla + "] - ID[" + oSoc.oEstatus.iID.S() + "] Mensaje de Error: " + ex.Message;
        oSoc.oEstatus.iEstatus = 0;
        oSoc.oEstatus.sMensaje = str.Replace("'", "");
        return false;
      }
      finally
      {
        Utils.DestroyCOMObject((object) oSoc);
      }
    }

    private List<SocioNegocio> ArmaListaSociosNegocio()
    {
      try
      {
        List<SocioNegocio> socioNegocioList = new List<SocioNegocio>();
        DBCorridor dbCorridor = new DBCorridor();
        DataTable getObtieneClientes = dbCorridor.DBGetObtieneClientes;
        DataTable obtieneProveedores = dbCorridor.DBGetObtieneProveedores;
        DataTable clientesProveedores = dbCorridor.DBGetObtieneDireccionClientesProveedores;
        if (getObtieneClientes != null && getObtieneClientes.Rows.Count > 0)
        {
          foreach (DataRow row in (InternalDataCollectionBase) getObtieneClientes.Rows)
          {
            SocioNegocio socioNegocio = new SocioNegocio();
            socioNegocio.iCompanyKeyOri = row["CLIENTE"].S().I();
            string str1 = string.Empty;
            if (row["CATEGORIA_CLIENTE"].S() == "C - EXT")
            {
              if (row["BASE"].S() == "TLC")
                str1 = "CET" + row["CLIENTE"].S().PadLeft(5, '0');
              else if (row["BASE"].S() == "MTY")
                str1 = "CEM" + row["CLIENTE"].S().PadLeft(5, '0');
            }
            else if (row["BASE"].S() == "TLC")
              str1 = "CNT" + row["CLIENTE"].S().PadLeft(5, '0');
            else if (row["BASE"].S() == "MTY")
              str1 = "CNM" + row["CLIENTE"].S().PadLeft(5, '0');
            socioNegocio.sCardCode = str1;
            socioNegocio.sCardName = row["NOMBRE"].S();
            socioNegocio.sLicTradNumRFC = row["NIT"].S();
            socioNegocio.oTipoBP = BoCardTypes.cCustomer;
            socioNegocio.CardFName = row["ALIAS"].S();
            socioNegocio.iGroupCode = !(row["CATEGORIA_CLIENTE"].S() == "C - EXT") ? 100 : 103;
            socioNegocio.sCurrency = "##";
            if (row["TELEFONO1"].S().Length > 20)
              row["TELEFONO1"] = (object) row["TELEFONO1"].S().Substring(0, 20);
            if (row["TELEFONO2"].S().Length > 20)
              row["TELEFONO2"] = (object) row["TELEFONO2"].S().Substring(0, 12);
            socioNegocio.sPhone1 = row["TELEFONO1"].S();
            socioNegocio.sPhone2 = row["TELEFONO2"].S();
            if (row["FAX"].S() != string.Empty)
            {
              if (row["FAX"].S().Length > 20)
                row["FAX"] = (object) row["FAX"].S().Substring(0, 20);
              socioNegocio.sFax = row["FAX"].S();
            }
            else
            {
              if (row["FAX2"].S().Length > 20)
                row["FAX2"] = (object) row["FAX2"].S().Substring(0, 20);
              socioNegocio.sFax = row["FAX2"].S();
            }
            if (row["NOTAS"].S().Length > 100)
              row["NOTAS"] = (object) row["NOTAS"].S().Substring(0, 100);
            socioNegocio.sNotes = row["NOTAS"].S();
            socioNegocio.sAliasName = row["ALIAS"].S();
            socioNegocio.sBase = row["BASE"].S();
            if (row["BASE"].S() == "TLC")
            {
              socioNegocio.sQryGroup1 = "Y";
              socioNegocio.sQryGroup2 = "N";
            }
            else
            {
              socioNegocio.sQryGroup1 = "N";
              socioNegocio.sQryGroup2 = "Y";
            }
            DataRow[] dataRowArray = clientesProveedores.Select("DETALLE_DIRECCION = " + row["DETALLE_DIRECCION"].S());
            if (dataRowArray != null && dataRowArray.Length > 0)
            {
              for (int index = 0; index < dataRowArray.Length; ++index)
              {
                string empty1 = string.Empty;
                string empty2 = string.Empty;
                DireccionSocioNegocio direccionSocioNegocio1 = new DireccionSocioNegocio();
                direccionSocioNegocio1.oAdressType = BoAddressType.bo_BillTo;
                direccionSocioNegocio1.sStreetNumber = dataRowArray[index]["CAMPO_1"].S();
                direccionSocioNegocio1.sBlockColonia = dataRowArray[index]["CAMPO_7"].S();
                direccionSocioNegocio1.sCiudad = dataRowArray[index]["CAMPO_5"].S();
                direccionSocioNegocio1.sCodigoPostal = dataRowArray[index]["CAMPO_10"].S();
                string str2 = this.ObtieneCodigoEstado(dataRowArray[index]["CAMPO_8"].S());
                string str3 = this.ObtieneCodigoPais(dataRowArray[index]["CAMPO_9"].S());
                direccionSocioNegocio1.sState = str2;
                direccionSocioNegocio1.sCountry = str3;
                direccionSocioNegocio1.sCounty = dataRowArray[index]["CAMPO_11"].S();
                direccionSocioNegocio1.sStreetNo = dataRowArray[index]["CAMPO_2"].S();
                direccionSocioNegocio1.sBuilding = dataRowArray[index]["CAMPO_3"].S();
                socioNegocio.oLstDir.Add(direccionSocioNegocio1);
                DireccionSocioNegocio direccionSocioNegocio2 = new DireccionSocioNegocio();
                direccionSocioNegocio2.oAdressType = BoAddressType.bo_ShipTo;
                direccionSocioNegocio2.sStreetNumber = dataRowArray[index]["CAMPO_1"].S();
                direccionSocioNegocio2.sBlockColonia = dataRowArray[index]["CAMPO_7"].S();
                direccionSocioNegocio2.sCiudad = dataRowArray[index]["CAMPO_5"].S();
                direccionSocioNegocio2.sCodigoPostal = dataRowArray[index]["CAMPO_10"].S();
                string str4 = this.ObtieneCodigoEstado(dataRowArray[index]["CAMPO_8"].S());
                string str5 = this.ObtieneCodigoPais(dataRowArray[index]["CAMPO_9"].S());
                direccionSocioNegocio2.sState = str4;
                direccionSocioNegocio2.sCountry = str5;
                direccionSocioNegocio2.sCounty = dataRowArray[index]["CAMPO_11"].S();
                direccionSocioNegocio2.sStreetNo = dataRowArray[index]["CAMPO_2"].S();
                direccionSocioNegocio2.sBuilding = dataRowArray[index]["CAMPO_3"].S();
                socioNegocio.oLstDir.Add(direccionSocioNegocio2);
              }
            }
            socioNegocioList.Add(socioNegocio);
          }
        }
        if (obtieneProveedores != null && obtieneProveedores.Rows.Count > 0)
        {
          foreach (DataRow row in (InternalDataCollectionBase) obtieneProveedores.Rows)
          {
            SocioNegocio socioNegocio = new SocioNegocio();
            socioNegocio.iCompanyKeyOri = row["PROVEEDOR"].S().I();
            string empty = string.Empty;
            string str = !(row["BASE"].S() == "TLC") ? "PM" + row["PROVEEDOR"].S().PadLeft(5, '0') : "PT" + row["PROVEEDOR"].S().PadLeft(5, '0');
            socioNegocio.sCardCode = str;
            socioNegocio.sCardName = row["NOMBRE"].S();
            socioNegocio.sLicTradNumRFC = row["NIT"].S();
            socioNegocio.oTipoBP = BoCardTypes.cSupplier;
            socioNegocio.CardFName = row["ALIAS"].S();
            socioNegocio.iGroupCode = !(row["CATEGORIA_PROVEED"].S() == "C - EXT") ? 101 : 102;
            socioNegocio.sCurrency = "##";
            if (row["TELEFONO1"].S().Length > 20)
              row["TELEFONO1"] = (object) row["TELEFONO1"].S().Substring(0, 20);
            if (row["TELEFONO2"].S().Length > 20)
              row["TELEFONO2"] = (object) row["TELEFONO2"].S().Substring(0, 20);
            if (row["FAX"].S().Length > 20)
              row["FAX"] = (object) row["FAX"].S().Substring(0, 20);
            socioNegocio.sPhone1 = row["TELEFONO1"].S();
            socioNegocio.sPhone2 = row["TELEFONO2"].S();
            socioNegocio.sFax = row["FAX"].S();
            if (row["NOTAS"].S().Length > 100)
              row["NOTAS"] = (object) row["NOTAS"].S().Substring(0, 100);
            socioNegocio.sNotes = row["NOTAS"].S();
            socioNegocio.sAliasName = row["ALIAS"].S();
            socioNegocio.sBase = row["BASE"].S();
            if (row["BASE"].S() == "TLC")
            {
              socioNegocio.sQryGroup1 = "Y";
              socioNegocio.sQryGroup2 = "N";
            }
            else
            {
              socioNegocio.sQryGroup1 = "N";
              socioNegocio.sQryGroup2 = "Y";
            }
            DataRow[] dataRowArray = clientesProveedores.Select("DETALLE_DIRECCION = " + row["DETALLE_DIRECCION"].S());
            if (dataRowArray != null && dataRowArray.Length > 0)
            {
              for (int index = 0; index < dataRowArray.Length; ++index)
              {
                socioNegocio.oLstDir.Add(new DireccionSocioNegocio()
                {
                  oAdressType = BoAddressType.bo_BillTo,
                  sStreetNumber = dataRowArray[index]["CAMPO_1"].S(),
                  sBlockColonia = dataRowArray[index]["CAMPO_7"].S(),
                  sCiudad = dataRowArray[index]["CAMPO_5"].S(),
                  sCodigoPostal = dataRowArray[index]["CAMPO_10"].S(),
                  sState = this.ObtieneCodigoEstado(dataRowArray[index]["CAMPO_8"].S()),
                  sCountry = this.ObtieneCodigoPais(dataRowArray[index]["CAMPO_9"].S()),
                  sCounty = dataRowArray[index]["CAMPO_11"].S(),
                  sStreetNo = dataRowArray[index]["CAMPO_2"].S(),
                  sBuilding = dataRowArray[index]["CAMPO_3"].S()
                });
                socioNegocio.oLstDir.Add(new DireccionSocioNegocio()
                {
                  oAdressType = BoAddressType.bo_ShipTo,
                  sStreetNumber = dataRowArray[index]["CAMPO_1"].S(),
                  sBlockColonia = dataRowArray[index]["CAMPO_7"].S(),
                  sCiudad = dataRowArray[index]["CAMPO_5"].S(),
                  sCodigoPostal = dataRowArray[index]["CAMPO_10"].S(),
                  sState = this.ObtieneCodigoEstado(dataRowArray[index]["CAMPO_8"].S()),
                  sCountry = this.ObtieneCodigoPais(dataRowArray[index]["CAMPO_9"].S()),
                  sCounty = dataRowArray[index]["CAMPO_11"].S(),
                  sStreetNo = dataRowArray[index]["CAMPO_2"].S(),
                  sBuilding = dataRowArray[index]["CAMPO_3"].S()
                });
              }
            }
            socioNegocioList.Add(socioNegocio);
          }
        }
        return socioNegocioList;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private string ObtieneCodigoEstado(string sDescEstado)
    {
      try
      {
        return new DBAccesoSAP().DBGetObtieneCodigoEstado(sDescEstado);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private string ObtieneCodigoPais(string sDescPais)
    {
      try
      {
        return new DBAccesoSAP().DBGetObtieneCodigoPais(sDescPais);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private bool ValidaExisteTipoCambioDia(string sFecha)
    {
      try
      {
        bool flag = false;
        DataSet obtieneDatosDeQuery = new DBAccesoSAP().DBGetObtieneDatosDeQuery("SELECT * FROM ORTT WHERE RateDate = '" + sFecha + "' AND Currency = 'USD'");
        if (obtieneDatosDeQuery != null && obtieneDatosDeQuery.Tables.Count > 0 && obtieneDatosDeQuery.Tables[0].Rows.Count > 0)
          flag = true;
        return flag;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private int ValidaExisteCliente(string sRFC, string sNombre, ref string sCardCode)
    {
      try
      {
        DataTable dataTable = new DataTable();
        string configApp1 = Globales.GetConfigApp<string>("RFCGenericoE");
        string configApp2 = Globales.GetConfigApp<string>("RFCGenericoN");
        int num;
        if (sRFC == configApp1 || sRFC == configApp2)
        {
          DataTable table = new DBAccesoSAP().DBGetObtieneDatosDeQuery("SELECT CardCode, CardName, CardType, GroupCode, CmpPrivate,U_IdCorridorT,U_IdCorridorM FROM OCRD (NOLOCK) WHERE CardName = '" + sNombre + "' AND CardType = 'C'").Tables[0];
          if (table != null && table.Rows.Count > 0)
          {
            if (table.Rows[0]["U_IdCorridorT"].S().I() == 0 || table.Rows[0]["U_IdCorridorM"].S().I() == 0)
            {
              num = 1;
              sCardCode = table.Rows[0]["CardCode"].S();
            }
            else
              num = 2;
          }
          else
            num = 0;
        }
        else
        {
          DataTable table = new DBAccesoSAP().DBGetObtieneDatosDeQuery("SELECT CardCode, CardName, CardType, GroupCode, CmpPrivate," + "CASE WHEN U_IdCorridorT IS NULL THEN 0 ELSE U_IdCorridorT END AS U_IdCorridorT," + "CASE WHEN U_IdCorridorM IS NULL THEN 0 ELSE U_IdCorridorM END AS U_IdCorridorM " + "FROM OCRD (NOLOCK) WHERE LicTradNum = '" + sRFC + "' AND CardType = 'C'").Tables[0];
          num = table == null || table.Rows.Count <= 0 ? 0 : (table.Rows[0]["U_IdCorridorT"].S().I() != 0 && table.Rows[0]["U_IdCorridorM"].S().I() != 0 ? 2 : 1);
        }
        return num;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private int ValidaExisteProveedor(string sRFC, string sNombre, ref string sCardCode)
    {
      try
      {
        DataTable dataTable = new DataTable();
        string configApp1 = Globales.GetConfigApp<string>("RFCGenericoE");
        string configApp2 = Globales.GetConfigApp<string>("RFCGenericoN");
        int num;
        if (sRFC == configApp1 || sRFC == configApp2)
        {
          DataTable table = new DBAccesoSAP().DBGetObtieneDatosDeQuery("SELECT CardCode, CardName, CardType, GroupCode, CmpPrivate,U_IdCorridorT,U_IdCorridorM FROM OCRD (NOLOCK) WHERE CardName = '" + sNombre + "'").Tables[0];
          if (table != null && table.Rows.Count > 0)
          {
            if (table.Rows[0]["U_IdCorridorT"].S().I() == 0 || table.Rows[0]["U_IdCorridorM"].S().I() == 0)
            {
              num = 1;
              sCardCode = table.Rows[0]["CardCode"].S();
            }
            else
              num = 2;
          }
          else
            num = 0;
        }
        else
        {
          DataTable table = new DBAccesoSAP().DBGetObtieneDatosDeQuery("SELECT CardCode, CardName, CardType, GroupCode, CmpPrivate," + "CASE WHEN U_IdCorridorT IS NULL THEN 0 ELSE U_IdCorridorT END AS U_IdCorridorT," + "CASE WHEN U_IdCorridorM IS NULL THEN 0 ELSE U_IdCorridorM END AS U_IdCorridorM " + "FROM OCRD (NOLOCK) WHERE LicTradNum = '" + sRFC + "' AND CardType = 'S'").Tables[0];
          num = table == null || table.Rows.Count <= 0 ? 0 : (table.Rows[0]["U_IdCorridorT"].S().I() != 0 && table.Rows[0]["U_IdCorridorM"].S().I() != 0 ? 2 : 1);
        }
        return num;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
