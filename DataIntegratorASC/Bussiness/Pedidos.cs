// Decompiled with JetBrains decompiler
// Type: DataIntegratorASC.Bussiness.Pedidos
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
  public class Pedidos
  {
    public bool Import()
    {
      try
      {
        bool flag = false;
        MyGlobals.sStepLog = "Arma lista de Pedidos";
        List<Pedido> armaListaPedidos = this.ArmaListaPedidos;
        Utils.GuardarBitacora("Armó la lista de SalesOrders con:" + armaListaPedidos.Count.S();
        foreach (Pedido oT in armaListaPedidos)
        {
          string empty = string.Empty;
          string str = new PurchaseOrders().ObtieneCardCodeClienteporRFC(oT.sRFC, oT.sNombreCliente);
          if (str != string.Empty)
          {
            oT.sCardCode = str;
            if (!new DBPurchase().DBGetValidaExisteDocumentoEnSAP_WOSO("SalesOrder", oT.sNumPedido))
            {
              if (oT.olsLineas.Count > 0)
              {
                if (this.CreateSapDoc(oT))
                  Utils.GuardarBitacora("Exito: " + oT.oEstatus.sMensaje + ", SalesOrder: " + oT.sOrdenCompra + ", Base: " + oT.sBASE);
                else
                  Utils.GuardarBitacora("Ocurrio un error al guardar la SalesOrder en SAP: " + oT.oEstatus.sMensaje + "SalesOrder: " + oT.sOrdenCompra + ", Numero SalesOrder: " + oT.sNumPedido + ", Fecha del documento:" + oT.sFechaPedido.S() + ", Base: " + oT.sBASE);
              }
              else
                Utils.GuardarBitacora("Error: Las Sales Order no tiene partidas; " + oT.oEstatus.sMensaje + "SalesOrder: " + oT.sOrdenCompra + ", Numero SalesOrder: " + oT.sNumPedido + ", Fecha del documento:" + oT.sFechaPedido.S() + ", Base: " + oT.sBASE);
            }
          }
          else
            Utils.GuardarBitacora("No encontró cliente:" + oT.sNombreCliente + ", Error:" + oT.oEstatus.sMensaje + "SalesOrder: " + oT.sOrdenCompra + ", Numero SalesOrder: " + oT.sNumPedido + ", Fecha del documento:" + oT.sFechaPedido.S() + ", Base: " + oT.sBASE);
        }
        return flag;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public List<Pedido> ArmaListaPedidos
    {
      get
      {
        try
        {
          List<Pedido> armaListaPedidos = new List<Pedido>();
          DataTable getObtienePedidos = new DBCorridor().DBGetObtienePedidos;
          DataTable obtieneDetallePedidos = new DBCorridor().DBGetObtieneDetallePedidos;
          if (getObtienePedidos != null && getObtienePedidos.Rows.Count > 0)
          {
            foreach (DataRow row in (InternalDataCollectionBase) getObtienePedidos.Rows)
            {
              Pedido pedido = new Pedido();
              string empty = string.Empty;
              string str = row.S("PEDIDO");
              pedido.sCardCode = row.S("CLIENTE");
              pedido.sPedido = str;
              pedido.sEstado = row.S("ESTADO");
              pedido.sFechaPedido = row.S("FECHA_PEDIDO");
              pedido.sNumPedido = row.S("PEDIDO");
              pedido.sOrdenCompra = row.S("ORDEN_COMPRA");
              pedido.sFechaOrden = row.S("FECHA_ORDEN");
              pedido.sComentarios = row.S("OBSERVACIONES");
              pedido.sClaveDirEmbarque = row.S("DIREC_EMBARQUE");
              pedido.sDireccionFactura = row.S("DIRECCION_FACTURA");
              pedido.sRubro1 = row.S("RUBRO1");
              pedido.sRubro2 = row.S("RUBRO2");
              pedido.sRubro3 = row.S("RUBRO3");
              pedido.sRubro4 = row.S("RUBRO4");
              pedido.sMoneda = row.S("MONEDA");
              pedido.iCliente = row.S("CLIENTE").I();
              pedido.sPais = row.S("PAIS");
              pedido.sNombreCliente = row.S("NOMBRE_CLIENTE");
              pedido.sRFC = row.S("NIT");
              pedido.IsIvaExempt = row.S("TAXEXEMPT") == "1";
              pedido.sBASE = row.S("BASE");
              pedido.Site = row.S("BODEGA");
              pedido.sProyectoH = row.S("RUBRO1");
              DataRow[] dataRowArray = obtieneDetallePedidos.Select("PEDIDO = '" + str + "' AND BASE = '" + pedido.sBASE + "'");
              if (dataRowArray != null && dataRowArray.Length > 0)
              {
                for (int index = 0; index < dataRowArray.Length; ++index)
                  pedido.olsLineas.Add(new LineasPedido()
                  {
                    sPedido = str,
                    iPedidoLinea = dataRowArray[index]["PEDIDO_LINEA"].S().I(),
                    sBodega = dataRowArray[index]["BODEGA"].S(),
                    sArticulo = dataRowArray[index]["ARTICULO"].S(),
                    dPrecioUnitario = dataRowArray[index]["PRECIO_UNITARIO"].S().D(),
                    iCantidadPedida = dataRowArray[index]["CANTIDAD_PEDIDA"].S().I(),
                    iCantidadFacturar = dataRowArray[index]["CANTIDAD_A_FACTURA"].S().I(),
                    dPorcDescuento = dataRowArray[index]["PORC_DESCUENTO"].S().D(),
                    sDescripcion = dataRowArray[index]["DESCRIPCION"].S(),
                    sCentroCosto = dataRowArray[index]["CENTRO_COSTO"].S(),
                    sCuentaContable = dataRowArray[index]["CUENTA_CONTABLE"].S(),
                    sBASE = dataRowArray[index]["BASE"].S(),
                    sUbicacion = dataRowArray[index]["BODEGA"].S()
                  });
              }
              armaListaPedidos.Add(pedido);
            }
          }
          return armaListaPedidos;
        }
        catch (Exception ex)
        {
          throw ex;
        }
      }
    }

        public bool CreateSapDoc(Pedido oT)
        {
      
            SAPbobsCOM.Documents oSapObject = MyGlobals.oCompany.GetBusinessObject(BoObjectTypes.oOrders);
            oSapObject.DocType = BoDocumentTypes.dDocument_Items;
            string configApp1 = Globales.GetConfigApp<string>("IvaNalCXP");
            string configApp2 = Globales.GetConfigApp<string>("IvaInt");
            string configApp3 = Globales.GetConfigApp<string>("IvaExCXP");
            string str1 = configApp1.Split('|')[0];
            string str2 = configApp1.Split('|')[1];
            string str3 = configApp2.Split('|')[0];
            string str4 = configApp2.Split('|')[1];

      try
      {
        if (string.IsNullOrEmpty(oT.sSerie))
        {
          object valueByQuery = new DBAccesoSAP().GetValueByQuery("SELECT Series FROM NNM1 WHERE SeriesName='Corridor' AND ObjectCode = 17");
          if (!string.IsNullOrEmpty(valueByQuery.S()))
            oSapObject.Series = valueByQuery.S().I();
        }
        oSapObject.CardCode = oT.sCardCode;
        oSapObject.DocDate = oT.sFechaPedido.S().Dt();
        oSapObject.DocDueDate = oT.sFechaPedido.S().Dt();
        oSapObject.NumAtCard = oT.sOrdenCompra;
        if (oT.sMoneda == "D")
          oSapObject.DocCurrency = "USD";
        else
          oSapObject.DocCurrency = "MXP";
        oSapObject.Comments = oT.sComentarios;
        oSapObject.DiscountPercent = 0.0;
        // ISSUE: reference to a compiler-generated method
        oSapObject.UserFields.Fields.Item("U_TipoDocu").Value = "2";
        string empty1 = string.Empty;
        string str5;
        if (oT.IsIvaExempt)
        {
          str5 = configApp3;
        }
        else
        {
          if (oT.dBaseImpuesto1 == 0M)
            oT.dBaseImpuesto1 = str1.S().D();
          str5 = !(oT.dBaseImpuesto1.S() == str1) ? str4 : str2;
        }
        // ISSUE: reference to a compiler-generated method
        oSapObject.UserFields.Fields.Item("U_Serie").Value = oT.sRubro2.S();
        // ISSUE: reference to a compiler-generated method
        oSapObject.UserFields.Fields.Item("U_Modelo").Value = oT.sRubro3.S();
        // ISSUE: reference to a compiler-generated method
        oSapObject.UserFields.Fields.Item("U_Fabricante").Value = oT.sRubro4.S();
        // ISSUE: reference to a compiler-generated method
        oSapObject.UserFields.Fields.Item("U_UBICACION").Value = oT.Site;
        // ISSUE: reference to a compiler-generated method
        oSapObject.UserFields.Fields.Item("U_Matricula").Value = oT.sProyectoH;
        foreach (LineasPedido olsLinea in oT.olsLineas)
        {
          string str6 = this.ObtieneClaveArticulo(olsLinea.sArticulo);
          oSapObject.Lines.ItemCode = !(str6 == string.Empty) ? str6 : throw new Exception("No existe el item " + olsLinea.sArticulo + " en SAP. DB[" + MyGlobals.oCompany.CompanyDB + "] ");
          oSapObject.Lines.ItemDescription = olsLinea.sDescripcion;
          oSapObject.Lines.Quantity = olsLinea.iCantidadPedida.S().Db();
          oSapObject.Lines.UnitPrice = olsLinea.dPrecioUnitario.S().Db();
          oSapObject.Lines.DiscountPercent = olsLinea.dPorcDescuento.S().Db();
          oSapObject.Lines.TaxCode = str5;
          oSapObject.Lines.CostingCode = olsLinea.sUbicacion;
          oSapObject.Lines.CostingCode2 = olsLinea.sDimension2;
          oSapObject.Lines.CostingCode3 = olsLinea.sDimension3;
          oSapObject.Lines.CostingCode4 = olsLinea.sDimension4;
          oSapObject.Lines.CostingCode5 = olsLinea.sDimension5;
          oSapObject.Lines.ProjectCode = olsLinea.sProyecto;
          // ISSUE: reference to a compiler-generated method
          oSapObject.Lines.Add();
        }
        string empty2 = string.Empty;
        // ISSUE: reference to a compiler-generated method
        if (oSapObject.Add() != 0)
        {
          oT.oEstatus.iEstatus = 0;
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          throw new Exception("Error al guardar el documento en SAP.  [" + MyGlobals.oCompany.GetLastErrorCode().S() + "] - " + MyGlobals.oCompany.GetLastErrorDescription());
        }
        bool sapDoc = true;
        oT.oEstatus.iEstatus = 1;
        // ISSUE: reference to a compiler-generated method
        oT.oEstatus.iSapDoc = MyGlobals.oCompany.GetNewObjectKey().S().I();
        if (oT.oEstatus.iSapDoc < 1)
          oT.oEstatus.iSapDoc = new DBAccesoSAP().DBGetObtieneDatosDeQuery("SELECT DocNum FROM ORDR WHERE DataSource='O' AND DocEntry = " + oT.oEstatus.iSapDoc.S() + " AND UserSign=" + MyGlobals.oCompany.UserSignature.S()).S().I();
        oT.oEstatus.sMensaje = "Se creo la orden de venta (SalesOrder) en SAP. DB[" + MyGlobals.oCompany.CompanyDB + "] - DocNum[" + oT.oEstatus.iSapDoc.S() + "] - ID_Tabla[ORDR]";
        new DBPurchase().InsertaDocumentoProcesado("SalesOrder", oT.sNumPedido, oT.oEstatus.sMensaje, oT.sBASE);
        return sapDoc;
      }
      catch (Exception ex)
      {
        string empty = string.Empty;
        string str7 = "Error al importar la orden de venta (SalesOrder). ID_Tabla[ORDR] - Mensaje de Error: " + ex.Message;
        oT.oEstatus.iEstatus = 0;
        oT.oEstatus.sMensaje = str7.Replace("'", "");
        return false;
      }
      finally
      {
        Utils.DestroyCOMObject((object) oSapObject);
      }
    }

    public string ObtieneClaveArticulo(string sNombre)
    {
      try
      {
        DataTable descripcionArticuloSap = new DBPurchase().DBGetObtieneDescripcionArticuloSAP(sNombre);
        string str = string.Empty;
        if (descripcionArticuloSap.Rows.Count > 0)
          str = descripcionArticuloSap.Rows[0]["DescripcionSAP"].S();
        DataSet obtieneDatosDeQuery = new DBAccesoSAP().DBGetObtieneDatosDeQuery("SELECT ItemCode, ItemName, ItmsGrpCod FROM OITM (NOLOCK) WHERE ItemName = '" + str + "'");
        return obtieneDatosDeQuery != null && obtieneDatosDeQuery.Tables.Count > 0 && obtieneDatosDeQuery.Tables[0].Rows.Count > 0 ? obtieneDatosDeQuery.Tables[0].Rows[0]["ItemCode"].S() : string.Empty;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
