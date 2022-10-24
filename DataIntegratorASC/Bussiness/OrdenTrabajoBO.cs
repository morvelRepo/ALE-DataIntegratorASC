// Decompiled with JetBrains decompiler
// Type: DataIntegratorASC.Bussiness.OrdenTrabajoBO
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
  public class OrdenTrabajoBO
  {
    public bool Import()
    {
      try
      {
        bool flag = false;
        MyGlobals.sStepLog = "Arma lista de Ordenes de Trabajo";
        List<OrdenTrabajo> listaOrdenesTrabajo = this.ArmaListaOrdenesTrabajo;
        Utils.GuardarBitacora("Armó la lista de WorkOrders con:" + listaOrdenesTrabajo.Count.S());
        foreach (OrdenTrabajo oT in listaOrdenesTrabajo)
        {
                    //VALIDACIONES
          string empty = string.Empty;
          string str = new PurchaseOrders().ObtieneCardCodeClienteporRFC(oT.sRFC, oT.sNombreCliente);
          if (str != string.Empty)
          {
            oT.sCardCode = str;
            if (!new DBPurchase().DBGetValidaExisteDocumentoEnSAP_WOSO("WorkOrder", oT.sPedido))// esta validacion se quita porque  la validacion se hace desde el query
            {
              if (this.CreateSapDoc(oT))
                Utils.GuardarBitacora("Exito: " + oT.oEstatus.sMensaje + ", Numero WorkOrder: " + oT.sOrdenCompra + ", Base: " + oT.sBase);
              else
                Utils.GuardarBitacora("Error: " + oT.oEstatus.sMensaje + ", Numero WorkOrder: " + oT.sOrdenCompra + ", Fecha documento: " + oT.sFechaPedido + ", Base: " + oT.sBase);
            }
          }
          else
            Utils.GuardarBitacora("No se encontró el cliente RFC: " + oT.sRFC + ", Nombre Cliente: " + oT.sNombreCliente + ", Numero WorkOrder: " + oT.sOrdenCompra + ", Base: " + oT.sBase);
        }
        return flag;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

        public List<OrdenTrabajo> ArmaListaOrdenesTrabajo
        {
            get
            {
                try
                {
                    List<OrdenTrabajo> listaOrdenesTrabajo = new List<OrdenTrabajo>();
                    DataTable obtieneOrdenesTrabajo = new DBCorridor().DBGetObtieneOrdenesTrabajo;
                    DataTable detalleOrdenesTrabajo = new DBCorridor().DBGetObtieneDetalleOrdenesTrabajo;
                    if (obtieneOrdenesTrabajo != null && obtieneOrdenesTrabajo.Rows.Count > 0)
                    {
                        foreach (DataRow row in obtieneOrdenesTrabajo.Rows)
                        {
                            OrdenTrabajo ordenTrabajo = new OrdenTrabajo();
                            string empty = string.Empty;
                            string sNumPerido = row.S("PEDIDO");
                            ordenTrabajo.sCardCode = row["CLIENTE"].S();
                            ordenTrabajo.sPedido = sNumPerido;
                            ordenTrabajo.sEstado =row.S("ESTADO");
                            ordenTrabajo.sFechaPedido = row.S("FECHA_PROMETIDA");
                            ordenTrabajo.sFechaPrometida = row.S("PEDIDO");
                            ordenTrabajo.sFechaProxEmbarque = row.S("FECHA_PROX_EMBARQU");
                            ordenTrabajo.sOrdenCompra = row.S("ORDEN_COMPRA");
                            ordenTrabajo.sFechaOrden = row.S("FECHA_ORDEN");
                            ordenTrabajo.sEmbarcar_a = row.S("EMBARCAR_A");
                            ordenTrabajo.sClaveDirEmbarque = row.S("DIREC_EMBARQUE");
                            ordenTrabajo.sDireccionFactura = row.S("DIRECCION_FACTURA");
                            ordenTrabajo.sRubro1 = row.S("RUBRO1");
                            ordenTrabajo.sRubro2 = row.S("RUBRO2");
                            ordenTrabajo.sRubro3 = row.S("RUBRO3");
                            ordenTrabajo.sRubro4 = row.S("RUBRO4");
                            ordenTrabajo.sComentario_cxc = row.S("COMENTARIO_CXC");
                            ordenTrabajo.dTotalMercaderia = row.S("TOTAL_MERCADERIA").D();
                            ordenTrabajo.dTotalImpuesto1 = row.S("TOTAL_IMPUESTO1").D();
                            ordenTrabajo.dTotalImpuesto2 = row.S("TOTAL_IMPUESTO2").D();
                            ordenTrabajo.dTotalAFacturar = row.S("TOTAL_A_FACTURAR").D();
                            ordenTrabajo.sMoneda = row.S("MONEDA");
                            ordenTrabajo.iCliente = row.S("CLIENTE").I();
                            ordenTrabajo.iClienteDireccion = row.S("CLIENTE_DIRECCION").I();
                            ordenTrabajo.iClienteCorporacion = row.S("CLIENTE_CORPORAC").I();
                            ordenTrabajo.iClienteOrigen = row.S("CLIENTE_ORIGEN").I();
                            ordenTrabajo.sPais = row.S("PAIS");
                            ordenTrabajo.sDescDirEmbarque = row.S("DESC_DIREC_EMBARQUE");
                            ordenTrabajo.dBaseImpuesto1 = row.S("BASE_IMPUESTO1").D();
                            ordenTrabajo.dBaseImpuesto2 = row.S("BASE_IMPUESTO2").D();
                            ordenTrabajo.sNombreCliente = row.S("NOMBRE_CLIENTE");
                            ordenTrabajo.sFechaProyectada = row.S("FECHA_PROYECTADA");
                            ordenTrabajo.sRFC = row.S("NIT");
                            ordenTrabajo.IsIvaExempt = row.S("TAXEXEMPT") == "1";
                            ordenTrabajo.sBase = row.S("BASE");
                            ordenTrabajo.Site = row.S("BODEGA");
                            ordenTrabajo.sProyectoH = row.S("RUBRO1");
              DataRow[] dataRowArray = detalleOrdenesTrabajo.Select("PEDIDO = '" + sNumPerido + "'");
              if (dataRowArray != null && dataRowArray.Length > 0)
              {
                for (int index = 0; index < dataRowArray.Length; ++index)
                  ordenTrabajo.olsLineas.Add(new LineasOrdenTrabajo()
                  {
                    sPedido = sNumPerido,
                      //iPedidoLinea = Extensiones.I((object) Extensiones.S(dataRowArray[index], "PEDIDO_LINEA")),
                    iPedidoLinea = dataRowArray[index]["PEDIDO_LINEA"].S().I(),
                    sBodega = dataRowArray[index]["BODEGA"].S(),
                    sArticulo = dataRowArray[index]["ARTICULO"].S(),
                    sEstado = dataRowArray[index]["ESTADO"].S(),
                    sFechaEntrega = dataRowArray[index]["FECHA_ENTREGA"].S(),
                    iLineaUsuario = dataRowArray[index]["LINEA_USUARIO"].S().I(),
                    dPrecioUnitario = dataRowArray[index]["PRECIO_UNITARIO"].S().D(),
                    iCantidadPedida = dataRowArray[index]["CANTIDAD_PEDIDA"].S().I(),
                    iCantidadFacturar = dataRowArray[index]["CANTIDAD_A_FACTURA"].S().I(),
                    dPorcDescuento = 0M,
                    sDescripcion = dataRowArray[index]["DESCRIPCION"].S(),
                    sComentario = string.Empty,
                    sFechaPrometida = dataRowArray[index]["FECHA_PROMETIDA"].S(),
                    iLineaOrdenCompra = dataRowArray[index]["LINEA_ORDEN_COMPRA"].S().I(),
                    sCentroCosto = string.Empty,
                    sCuentaContable = dataRowArray[index]["CUENTA_CONTABLE"].S(),
                    sProyecto = ordenTrabajo.sRubro1,
                    sUbicacion = dataRowArray[index]["BODEGA"].S()
                  });
              }
              listaOrdenesTrabajo.Add(ordenTrabajo);
            }
          }
          return listaOrdenesTrabajo;
        }
        catch (Exception ex)
        {
          throw ex;
        }
      }
    }

        public bool CreateSapDoc(OrdenTrabajo oT)
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
          object valueByQuery = new DBAccesoSAP().GetValueByQuery("SELECT Series FROM NNM1 WHERE SeriesName='Corridor' AND ObjectCode = 17 ");
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
        oSapObject.Comments = oT.sComentario_cxc;
        oSapObject.DiscountPercent = 0.0;
        // ISSUE: reference to a compiler-generated method
        oSapObject.UserFields.Fields.Item("U_TipoDocu").Value = "1";
        string empty1 = string.Empty;
        string str5 = !oT.IsIvaExempt && !(oT.dBaseImpuesto1.S().D() == 0M) ? (!(oT.dBaseImpuesto1.S().D().S() == str1) ? str4 : str2) : configApp3;
        // ISSUE: reference to a compiler-generated method
        oSapObject.UserFields.Fields.Item("U_Serie").Value = oT.sRubro2.S();
        // ISSUE: reference to a compiler-generated method
        oSapObject.UserFields.Fields.Item("U_Modelo").Value = oT.sRubro3.S();
        // ISSUE: reference to a compiler-generated method
        oSapObject.UserFields.Fields.Item("U_Fabricante").Value = oT.sRubro4.S();
        // ISSUE: reference to a compiler-generated method
        oSapObject.UserFields.Fields.Item("U_UBICACION").Value = oT.Site.S();
        // ISSUE: reference to a compiler-generated method
        oSapObject.UserFields.Fields.Item("U_Matricula").Value = oT.sProyectoH;
        bool flag = true;
        foreach (LineasOrdenTrabajo olsLinea in oT.olsLineas)
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
          if (olsLinea.sProyecto == "N/A" || olsLinea.sProyecto == "n/a")
            olsLinea.sProyecto = string.Empty;
          oSapObject.Lines.ProjectCode = olsLinea.sProyecto;
          // ISSUE: reference to a compiler-generated method
          oSapObject.Lines.Add();
          if (olsLinea.sProyecto == string.Empty)
          {
            flag = false;
            break;
          }
        }
        if (flag)
        {
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
            oT.oEstatus.iSapDoc = new DBAccesoSAP().DBGetObtieneDatosDeQuery("SELECT MAX(DocEntry) FROM ORDR WHERE DataSource='O' AND UserSign=" + MyGlobals.oCompany.UserSignature.S()).S().I();
          else
            oT.oEstatus.sMensaje = "Se creo una orden de venta(WorkOrder) en SAP. DB[" + MyGlobals.oCompany.CompanyDB + "] - DocEntry[" + oT.oEstatus.iSapDoc.S() + "] - ID_Tabla[ORDR]";
          new DBPurchase().InsertaDocumentoProcesado("WorkOrder", oT.sPedido, oT.oEstatus.sMensaje, oT.sBase);
          return sapDoc;
        }
        oT.oEstatus.iEstatus = 0;
        throw new Exception("Error al guardar el documento en SAP. [No todas las lineas tienen Proyecto] - Orden de Compra : " + oT.sOrdenCompra + " CardCode: " + oT.sCardCode);
      }
      catch (Exception ex)
      {
        string empty = string.Empty;
        string str7 = "Error al importar la WorkOrder. Tabla[" + oT.oEstatus.sTabla + "] - ID[" + oT.oEstatus.iID.S() + "] Mensaje de Error: " + ex.Message;
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
