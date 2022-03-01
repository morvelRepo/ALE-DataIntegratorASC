// Decompiled with JetBrains decompiler
// Type: DataIntegratorASC.Bussiness.DBPurchase
// Assembly: DataIntegratorASC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CACEFAEB-425A-4474-9813-A6D977B283DD
// Assembly location: C:\Users\morat\OneDrive\Escritorio\DataIntegratorASC.exe

using DataIntegratorASC.DomainModel;
using DataIntegratorASC.Objetos;
using NucleoBase.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataIntegratorASC.Bussiness
{
  public class DBPurchase
  {
    public List<PurchaseOrder> DBGetPurchasePendientes()
    {
      try
      {
        return new DBCorridor().DBGetObtienePurchaseOrders.AsEnumerable().Select<DataRow, PurchaseOrder>((Func<DataRow, PurchaseOrder>) (r => new PurchaseOrder()
        {
          iProveedor = r["PROVEEDOR"].S().I(),
          sCardCode = (r["BASE"].S() == "TLC" ? "PT" : "PM") + r["PROVEEDOR"].S().PadLeft(5, '0'),
          sDocumento = r["DOCUMENTO"].S(),
          sFactura = r["FACTURA"].S(),
          dtFecha = r["FECHA_DOCUMENTO"].S().Dt(),
          sAplicacion = r["APLICACION"].S(),
          dMonto = r["MONTO"].S().D(),
          dSaldo = r["SALDO"].S().D(),
          dMontoLocal = r["MONTO_LOCAL"].S().D(),
          dSaldoLocal = r["SALDO_LOCAL"].S().D(),
          dMontoDolar = r["MONTO_DOLAR"].S().D(),
          dSaldoDolar = r["SALDO_DOLAR"].S().D(),
          dTipoCambioMoneda = r["TIPO_CAMBIO_MONEDA"].S().D(),
          dTipoCambioDolar = r["TIPO_CAMBIO_DOLAR"].S(),
          dMontoProv = r["MONTO_PROV"].S().D(),
          dSaldoProv = r["SALDO_PROV"].S().D(),
          dTipoCambioProv = r["TIPO_CAMBIO_PROV"].S().D(),
          dSubtotal = r["SUBTOTAL"].S().D(),
          dDescuento = r["DESCUENTO"].S().D(),
          sMoneda = r["MONEDA"].S(),
          sCodigoImpuesto = ["CODIGO_IMPUESTO"].S(),
          sPais = r["PAIS"].S(),
          sBase = r["BASE"].S(),
          sVim = r["VIM"].S(),
          sTipoDoc = r["TIPODOC"].S(),
          sRazonSocial = r["RAZONSOCIAL"].S(),
          sRFC = r["RFC"].S(),
          sMsg = string.Empty
        })).ToList<PurchaseOrder>();
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public bool InsertaDocumentoProcesado(
      string sTipoDocumento,
      string sIdentificador,
      string sMensaje,
      string sBase)
    {
      try
      {
        return new DBIntegrator().oBD_SP.EjecutarValor("[dbo].[spI_DI_InsertaDocumentoProcesado]", new object[10]
        {
          (object) "@TipoDocumento",
          (object) sTipoDocumento,
          (object) "@Identificador",
          (object) sIdentificador,
          (object) "@Mensaje",
          (object) sMensaje,
          (object) "@UsuarioCreacion",
          (object) "DataIntegratorASC",
          (object) "@Base",
          (object) sBase
        }) != null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public bool DBGetValidaExisteDocumentoEnSAP(
      string sTipoDocumento,
      string sIdentificador,
      string sBase)
    {
      try
      {
        return new DBIntegrator().oBD_SP.EjecutarValor("[dbo].[spS_DI_ConsultaDocumentoCreadoEnSAP]", new object[6]
        {
          (object) "@TipoDocumento",
          (object) sTipoDocumento,
          (object) "@Identificador",
          (object) sIdentificador,
          (object) "@Base",
          (object) sBase
        }).S().I() > 0;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public bool DBGetValidaExisteDocumentoEnSAP_WOSO(string sTipoDocumento, string sIdentificador)
    {
      try
      {
        return new DBIntegrator().oBD_SP.EjecutarValor("[dbo].[spS_DI_ConsultaDocumentoCreadoEnSAP_WOSO]", new object[4]
        {
          (object) "@TipoDocumento",
          (object) sTipoDocumento,
          (object) "@Identificador",
          (object) sIdentificador
        }).S().I() > 0;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable DBGetObtieneDescripcionArticuloSAP(string sDesc)
    {
      try
      {
        return new DBIntegrator().oBD_SP.EjecutarDT("[dbo].[ObtieneNombreDeArticuloEnSAP]", new object[2]
        {
          (object) "@Descripcion",
          (object) sDesc
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
