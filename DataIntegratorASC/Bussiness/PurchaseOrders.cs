// Decompiled with JetBrains decompiler
// Type: DataIntegratorASC.Bussiness.PurchaseOrders
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
  public class PurchaseOrders
  {
    public bool Import()
    {
      try
      {
        bool flag = false;
        MyGlobals.sStepLog = "Prepara Purchase Orders";
        List<PurchaseOrder> purchaseOrderList = new List<PurchaseOrder>();
        MyGlobals.sStepLog = "Consulta Purchase Orders Pendientes";
        List<PurchaseOrder> purchasePendientes = new DBPurchase().DBGetPurchasePendientes();
        Utils.GuardarBitacora("Armó la lista de PurchaseOrders con:" + purchasePendientes.Count.S());
        foreach (PurchaseOrder purchaseOrder in purchasePendientes)
        {
          string str1 = string.Empty;
          string empty = string.Empty;
          MyGlobals.sStepLog = "Purcharse Order => BASE: " + purchaseOrder.sBase + " VIM: " + purchaseOrder.sVim + ", Razon Social: " + purchaseOrder.sRazonSocial;
          string str2 = this.ObtieneCardCodeProveedorporRFC(purchaseOrder.sRFC, purchaseOrder.sRazonSocial.Replace("'", ""));
          if (str2 != string.Empty)
          {
            purchaseOrder.sCardCode = str2;
          }
          else
          {
            str1 = this.ObtieneCardCodeDepeniendoBase(purchaseOrder.iProveedor, purchaseOrder.sBase);
            purchaseOrder.sCardCode = str1;
          }
          if (str1 != string.Empty || str2 != string.Empty)
          {
            if (purchaseOrder.sTipoDoc == "FC")
            {
              if (!new DBPurchase().DBGetValidaExisteDocumentoEnSAP(nameof (PurchaseOrders), purchaseOrder.sVim, purchaseOrder.sBase))
              {
                if (this.CreateSapDoc(purchaseOrder))
                  Utils.GuardarBitacora("Exito:" + purchaseOrder.oEstatus.sMensaje + ", PurcharseOrder: " + purchaseOrder.sVim + ", Base: " + purchaseOrder.sBase);
                else
                  Utils.GuardarBitacora("Error: " + purchaseOrder.oEstatus.sMensaje + ", Tipo Documento: FACTURA, PurcharseOrder: " + purchaseOrder.sVim + ", Fecha del documento:" + purchaseOrder.dtFecha.S() + ", Proveedor: " + purchaseOrder.iProveedor.S() + ", Base: " + purchaseOrder.sBase);
              }
            }
            else if (!new DBPurchase().DBGetValidaExisteDocumentoEnSAP("CreditNote", purchaseOrder.sVim, purchaseOrder.sBase))
            {
              if (this.CreateSapDocNotaCredito(purchaseOrder))
                Utils.GuardarBitacora("Exito:" + purchaseOrder.oEstatus.sMensaje + ", PurcharseOrder: " + purchaseOrder.sVim + ", Base: " + purchaseOrder.sBase);
              else
                Utils.GuardarBitacora("Error: " + purchaseOrder.oEstatus.sMensaje + ", Tipo Documento: NOTA CREDITO,  PurcharseOrder: " + purchaseOrder.sVim + ", Fecha del documento:" + purchaseOrder.dtFecha.S() + ", Proveedor: " + purchaseOrder.iProveedor.S() + ", Base: " + purchaseOrder.sBase);
            }
          }
          else
            Utils.GuardarBitacora("No encontró al provedor: " + purchaseOrder.iProveedor.S() + ", Razon Social:" + purchaseOrder.sRazonSocial + ", Base: " + purchaseOrder.sBase + ",  PurcharseOrder: " + purchaseOrder.sVim + ", Fecha del documento:" + purchaseOrder.dtFecha.S());
        }
        return flag;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public bool CreateSapDoc(PurchaseOrder oF)
    {
      bool sapDoc = false;
      
          SAPbobsCOM.Documents oSapObject = MyGlobals.oCompany.GetBusinessObject(BoObjectTypes.oPurchaseOrders);
      oSapObject.DocType = BoDocumentTypes.dDocument_Service;
      try
      {
        oSapObject.CardCode = oF.sCardCode;
        oSapObject.TaxDate = oF.dtFecha;
        oSapObject.DocDate = oF.dtFecha;
        oSapObject.NumAtCard = oF.sFactura;
        if (oF.sMoneda == "MXN")
          oF.sMoneda = "MXP";
        if (oF.sMoneda != "MXP")
          oSapObject.DocRate = double.Parse(oF.dTipoCambioMoneda.S());
        oSapObject.DocCurrency = oF.sMoneda;
        if (string.IsNullOrEmpty(oF.sSerie))
        {
          object valueByQuery = new DBAccesoSAP().GetValueByQuery("SELECT Series FROM NNM1 WHERE SeriesName='Corridor' AND ObjectCode = 22");
          if (!string.IsNullOrEmpty(valueByQuery.S()))
            oSapObject.Series = valueByQuery.S().I();
        }
        oSapObject.DiscountPercent = double.Parse(oF.dDescuento.S());
        oSapObject.Comments = oF.sDocumento.S();
        oSapObject.JournalMemo = oF.sDocumento.S();
        string configApp1 = Globales.GetConfigApp<string>("IvaNal");
        string configApp2 = Globales.GetConfigApp<string>("IvaInt");
        string configApp3 = Globales.GetConfigApp<string>("IvaNA");
        string configApp4 = Globales.GetConfigApp<string>("IvaCero");
        string str1 = configApp1.Split('|')[0];
        string str2 = configApp1.Split('|')[1];
        string str3 = configApp2.Split('|')[0];
        string str4 = configApp2.Split('|')[1];
        string str5 = string.Empty;
        if (oF.sCodigoImpuesto.S().D() == str1.S().D())
          str5 = str2;
        if (oF.sCodigoImpuesto.S().D() == 0M && oF.sCodigoImpuesto.S() == configApp3.S())
          str5 = configApp4;
        oSapObject.Lines.TaxCode = str5;
        oF.sDimension1 = !(oF.sBase == "TLC") ? "MTY" : "TLC";
        // ISSUE: reference to a compiler-generated method
        oSapObject.UserFields.Fields.Item("U_UBICACION").Value = oF.sBase;
        string configApp5 = Globales.GetConfigApp<string>("CuentaContable");
        oSapObject.Lines.AccountCode = configApp5;
        oSapObject.Lines.ItemDescription = oF.sDocumento;
        oSapObject.Lines.LineTotal = oF.dSubtotal.S().Db();
        oSapObject.Lines.ProjectCode = oF.sProyecto;
        oSapObject.Lines.CostingCode = oF.sDimension1;
        oSapObject.Lines.CostingCode2 = oF.sDimension2;
        oSapObject.Lines.CostingCode3 = oF.sDimension3;
        // ISSUE: reference to a compiler-generated method
        oSapObject.Lines.UserFields.Fields.Item("U_TextoLargo").Value = oF.sDocumento;
        // ISSUE: reference to a compiler-generated method
        oSapObject.Lines.Add();
        string empty = string.Empty;
        // ISSUE: reference to a compiler-generated method
        if (oSapObject.Add() != 0)
        {
          oF.oEstatus.iEstatus = 0;
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          string str6 = "Error al guardar el documento en SAP.  [" + MyGlobals.oCompany.GetLastErrorCode().S() + "] - " + MyGlobals.oCompany.GetLastErrorDescription();
          oF.oEstatus.sMensaje = str6;
        }
        else
        {
          sapDoc = true;
          oF.oEstatus.iEstatus = 1;
          // ISSUE: reference to a compiler-generated method
          oF.oEstatus.iSapDoc = MyGlobals.oCompany.GetNewObjectKey().S().I();
          if (oF.oEstatus.iSapDoc < 1)
          {
            DataTable table = new DBAccesoSAP().DBGetObtieneDatosDeQuery("SELECT TOP 1 DocNum FROM OPOR(NOLOCK) WHERE DocEntry = " + oF.oEstatus.iSapDoc.S()).Tables[0];
            if (table.Rows.Count > 0)
              oF.oEstatus.iSapDoc = table.Rows[0][0].S().I();
            else
              oF.oEstatus.iSapDoc = 0;
          }
          else
            oF.oEstatus.sMensaje = "Se creo una factura de proveedor en SAP. DB[" + MyGlobals.oCompany.CompanyDB + "] - DocNum[" + oF.oEstatus.iSapDoc.S() + "] - ID_Tabla[OPOR]";
          new DBPurchase().InsertaDocumentoProcesado(nameof (PurchaseOrders), oF.sVim, oF.oEstatus.sMensaje, oF.sBase);
        }
        return sapDoc;
      }
      catch (Exception ex)
      {
        string empty = string.Empty;
        string str = "Error al importar registro de DB Intermedia. Tabla[" + oF.oEstatus.sTabla + "] - ID[" + oF.oEstatus.iID.S() + "] Mensaje de Error: " + ex.Message;
        oF.oEstatus.iEstatus = 0;
        oF.oEstatus.sMensaje = str.Replace("'", "");
        return false;
      }
      finally
      {
        Utils.DestroyCOMObject((object) oSapObject);
      }
    }

    public bool CreateSapDocNotaCredito(PurchaseOrder oR)
    {
      try
      {
        bool sapDocNotaCredito = false;
        
        Documents oSapObject = MyGlobals.oCompany.GetBusinessObject(BoObjectTypes.oDrafts);
        oSapObject.DocType = BoDocumentTypes.dDocument_Service;
        oSapObject.DocObjectCodeEx = "19";
        try
        {
          oSapObject.CardCode = oR.sCardCode;
          oSapObject.NumAtCard = oR.sFactura;
          oSapObject.TaxDate = oR.dtFecha;
          oSapObject.DocDate = oR.dtFecha;
          oSapObject.Comments = oR.sComentarios;
          oSapObject.DiscountPercent = double.Parse(oR.dDescuento.S());
          if (oR.sMoneda == "MXN")
            oR.sMoneda = "MXP";
          oSapObject.DocCurrency = oR.sMoneda;
          double num = 0.0;
          if (oR.sMoneda != "MXP")
          {
            num = double.Parse(oR.dTipoCambioDolar.S());
            if (num > 100.0)
              num = double.Parse(oR.dTipoCambioDolar.S().Replace('.', ','));
          }
          oSapObject.DocRate = num;
          string str1 = string.Empty;
          string configApp1 = Globales.GetConfigApp<string>("IvaNal");
          string configApp2 = Globales.GetConfigApp<string>("IvaInt");
          string configApp3 = Globales.GetConfigApp<string>("IvaNA");
          string configApp4 = Globales.GetConfigApp<string>("IvaCero");
          string str2 = configApp1.Split('|')[0];
          string str3 = configApp1.Split('|')[1];
          string str4 = configApp2.Split('|')[0];
          string str5 = configApp2.Split('|')[1];
          if (oR.sCodigoImpuesto.S().D() ==  str2.S().D())
            str1 = str3;
          if (oR.sCodigoImpuesto.S().D() == 0M && oR.sCodigoImpuesto.S() == configApp3.S())
            str1 = configApp4;
          oR.sDimension1 = !(oR.sBase == "TLC") ? "MTY" : "TLC";
          // ISSUE: reference to a compiler-generated method
          oSapObject.UserFields.Fields.Item((object) "U_UBICACION").Value = (object) oR.sBase;
          oSapObject.Lines.AccountCode = Globales.GetConfigApp<string>("CuentaContable");
          oSapObject.Lines.ItemDescription = oR.sDocumento;
          oSapObject.Lines.UnitPrice = oR.dSubtotal.S().Db();
          oSapObject.Lines.TaxCode = str1;
          oSapObject.Lines.ProjectCode = oR.sProyecto;
          oSapObject.Lines.CostingCode = oR.sDimension1;
          oSapObject.Lines.CostingCode2 = oR.sDimension2;
          oSapObject.Lines.CostingCode3 = oR.sDimension3;
          // ISSUE: reference to a compiler-generated method
          oSapObject.Lines.Add();
          string empty = string.Empty;
          // ISSUE: reference to a compiler-generated method
          if (oSapObject.Add() != 0)
          {
            oR.oEstatus.iEstatus = 0;
            // ISSUE: reference to a compiler-generated method
            // ISSUE: reference to a compiler-generated method
            Utils.GuardarBitacora("Error al guardar el documento en SAP.  [" + MyGlobals.oCompany.GetLastErrorCode().S() + "] - " + MyGlobals.oCompany.GetLastErrorDescription());
          }
          else
          {
            sapDocNotaCredito = true;
            oR.oEstatus.iEstatus = 1;
            // ISSUE: reference to a compiler-generated method
            oR.oEstatus.iSapDoc = MyGlobals.oCompany.GetNewObjectKey().S().I();
            if (oR.oEstatus.iSapDoc < 1)
            {
              oR.oEstatus.iSapDoc = new DBAccesoSAP().GetValueByQuery("SELECT TOP 1 DocNum FROM ORPC (NOLOCK) WHERE DocEntry = " + oR.oEstatus.iSapDoc.S()).S().I();
            }
            else
            {
              string str6 = new DBAccesoSAP().GetValueByQuery("SELECT TOP 1 DocNum FROM ORPC (NOLOCK) WHERE DocEntry = " + oR.oEstatus.iSapDoc.S()).S();
              oR.oEstatus.sMensaje = "Se creo una NOTA DE CRÉDITO en SAP. DB[" + MyGlobals.oCompany.CompanyDB + "] - DocNum[" + str6 + "] - ID_Tabla[ORPC]";
            }
            new DBPurchase().InsertaDocumentoProcesado("CreditNote", oR.sVim, oR.oEstatus.sMensaje, oR.sBase);
          }
          return sapDocNotaCredito;
        }
        catch (Exception ex)
        {
          string empty = string.Empty;
          string str = "Error al importar registro de DB Intermedia. Tabla[" + oR.oEstatus.sTabla + "] - ID[" + oR.oEstatus.iID.S() + "] Mensaje de Error: " + ex.Message;
          oR.oEstatus.iEstatus = 0;
          oR.oEstatus.sMensaje = str.Replace("'", "");
          return false;
        }
        finally
        {
          Utils.DestroyCOMObject((object) oSapObject);
        }
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public string ObtieneCardCodeDepeniendoBase(int iProveedor, string sBase)
    {
      try
      {
        string empty = string.Empty;
        return new DBAccesoSAP().GetValueByQuery(!(sBase == "TLC") ? "SELECT TOP 1 CardCode FROM OCRD (NOLOCK) WHERE CardType = 'S' AND CONVERT(INT,U_IdCorridorM) = " + (object) iProveedor : "SELECT TOP 1 CardCode FROM OCRD (NOLOCK) WHERE CardType = 'S' AND CONVERT(INT,U_IdCorridorT) = " + (object) iProveedor).S();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string ObtieneCardCodeClienteporRFC(string sRFC, string sNombre)
    {
      try
      {
        object obj = new object();
        string configApp1 = Globales.GetConfigApp<string>("RFCGenericoE");
        string configApp2 = Globales.GetConfigApp<string>("RFCGenericoN");
        object valueByQuery;
        if (sRFC == configApp1 || sRFC == configApp2)
        {
          valueByQuery = new DBAccesoSAP().GetValueByQuery("SELECT TOP 1 CardCode FROM OCRD (NOLOCK) WHERE CardType = 'C' AND CardName = '" + sNombre + "'");
        }
        else
        {
          string empty = string.Empty;
          valueByQuery = new DBAccesoSAP().GetValueByQuery("SELECT TOP 1 CardCode FROM OCRD (NOLOCK) WHERE CardType = 'C' AND LicTradNum = '" + sRFC + "'");
        }
        return valueByQuery.S();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string ObtieneCardCodeProveedorporRFC(string sRFC, string sNombre)
    {
      try
      {
        object obj = new object();
        string configApp1 = Globales.GetConfigApp<string>("RFCGenericoE");
        string configApp2 = Globales.GetConfigApp<string>("RFCGenericoN");
        object valueByQuery;
        if (sRFC == configApp1 || sRFC == configApp2)
        {
          valueByQuery = new DBAccesoSAP().GetValueByQuery("SELECT TOP 1 CardCode FROM OCRD (NOLOCK) WHERE CardType = 'S' AND CardName = '" + sNombre + "'");
        }
        else
        {
          string empty = string.Empty;
          valueByQuery = new DBAccesoSAP().GetValueByQuery("SELECT TOP 1 CardCode FROM OCRD (NOLOCK) WHERE CardType = 'S' AND LicTradNum = '" + sRFC + "'");
        }
        return valueByQuery.S();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
