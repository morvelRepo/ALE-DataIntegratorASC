// Decompiled with JetBrains decompiler
// Type: DataIntegratorASC.Objetos.PurchaseOrder
// Assembly: DataIntegratorASC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CACEFAEB-425A-4474-9813-A6D977B283DD
// Assembly location: C:\Users\morat\OneDrive\Escritorio\DataIntegratorASC.exe

using System;

namespace DataIntegratorASC.Objetos
{
  public class PurchaseOrder : BaseObjeto
  {
    private int _iProveedor = 0;
    private string _sCardCode = string.Empty;
    private string _sDocumento = string.Empty;
    private string _sFactura = string.Empty;
    private DateTime _dtFecha = DateTime.Now;
    private string _sSerie = string.Empty;
    private string _sComentarios = string.Empty;
    private string _sAplicacion = string.Empty;
    private Decimal _dMonto = 0M;
    private Decimal _dSaldo = 0M;
    private Decimal _dMontoLocal = 0M;
    private Decimal _dSaldoLocal = 0M;
    private Decimal _dMontoDolar = 0M;
    private Decimal _dSaldoDolar = 0M;
    private Decimal _dTipoCambioMoneda = 0M;
    private string _dTipoCambioDolar = string.Empty;
    private Decimal _dMontoProv = 0M;
    private Decimal _dSaldoProv = 0M;
    private Decimal _dTipoCambioProv = 0M;
    private Decimal _dSubtotal = 0M;
    private Decimal _dDescuento = 0M;
    private string _sMoneda = string.Empty;
    private string _sCodigoImpuesto = string.Empty;
    private string _sPais = string.Empty;
    private string _sProyecto = string.Empty;
    private string _sDimension1 = string.Empty;
    private string _sDimension2 = string.Empty;
    private string _sDimension3 = string.Empty;
    private string _sBase = string.Empty;
    private string _sVim = string.Empty;
    private string _sTipoDoc = string.Empty;
    private string _sMsg = string.Empty;
    private string _sRFC = string.Empty;
    private string _sRazonSocial = string.Empty;

    public int iProveedor
    {
      get => this._iProveedor;
      set => this._iProveedor = value;
    }

    public string sCardCode
    {
      get => this._sCardCode;
      set => this._sCardCode = value;
    }

    public string sDocumento
    {
      get => this._sDocumento;
      set => this._sDocumento = value;
    }

    public string sFactura
    {
      get => this._sFactura;
      set => this._sFactura = value;
    }

    public DateTime dtFecha
    {
      get => this._dtFecha;
      set => this._dtFecha = value;
    }

    public string sSerie
    {
      get => this._sSerie;
      set => this._sSerie = value;
    }

    public string sComentarios
    {
      get => this._sComentarios;
      set => this._sComentarios = value;
    }

    public string sAplicacion
    {
      get => this._sAplicacion;
      set => this._sAplicacion = value;
    }

    public Decimal dMonto
    {
      get => this._dMonto;
      set => this._dMonto = value;
    }

    public Decimal dSaldo
    {
      get => this._dSaldo;
      set => this._dSaldo = value;
    }

    public Decimal dMontoLocal
    {
      get => this._dMontoLocal;
      set => this._dMontoLocal = value;
    }

    public Decimal dSaldoLocal
    {
      get => this._dSaldoLocal;
      set => this._dSaldoLocal = value;
    }

    public Decimal dMontoDolar
    {
      get => this._dMontoDolar;
      set => this._dMontoDolar = value;
    }

    public Decimal dSaldoDolar
    {
      get => this._dSaldoDolar;
      set => this._dSaldoDolar = value;
    }

    public Decimal dTipoCambioMoneda
    {
      get => this._dTipoCambioMoneda;
      set => this._dTipoCambioMoneda = value;
    }

    public string dTipoCambioDolar
    {
      get => this._dTipoCambioDolar;
      set => this._dTipoCambioDolar = value;
    }

    public Decimal dMontoProv
    {
      get => this._dMontoProv;
      set => this._dMontoProv = value;
    }

    public Decimal dSaldoProv
    {
      get => this._dSaldoProv;
      set => this._dSaldoProv = value;
    }

    public Decimal dTipoCambioProv
    {
      get => this._dTipoCambioProv;
      set => this._dTipoCambioProv = value;
    }

    public Decimal dSubtotal
    {
      get => this._dSubtotal;
      set => this._dSubtotal = value;
    }

    public Decimal dDescuento
    {
      get => this._dDescuento;
      set => this._dDescuento = value;
    }

    public string sMoneda
    {
      get => this._sMoneda;
      set => this._sMoneda = value;
    }

    public string sCodigoImpuesto
    {
      get => this._sCodigoImpuesto;
      set => this._sCodigoImpuesto = value;
    }

    public string sPais
    {
      get => this._sPais;
      set => this._sPais = value;
    }

    public string sProyecto
    {
      get => this._sProyecto;
      set => this._sProyecto = value;
    }

    public string sDimension1
    {
      get => this._sDimension1;
      set => this._sDimension1 = value;
    }

    public string sDimension2
    {
      get => this._sDimension2;
      set => this._sDimension2 = value;
    }

    public string sDimension3
    {
      get => this._sDimension3;
      set => this._sDimension3 = value;
    }

    public string sBase
    {
      get => this._sBase;
      set => this._sBase = value;
    }

    public string sVim
    {
      get => this._sVim;
      set => this._sVim = value;
    }

    public string sTipoDoc
    {
      get => this._sTipoDoc;
      set => this._sTipoDoc = value;
    }

    public string sMsg
    {
      get => this._sMsg;
      set => this._sMsg = value;
    }

    public string sRFC
    {
      get => this._sRFC;
      set => this._sRFC = value;
    }

    public string sRazonSocial
    {
      get => this._sRazonSocial;
      set => this._sRazonSocial = value;
    }
  }
}
