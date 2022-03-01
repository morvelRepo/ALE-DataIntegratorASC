// Decompiled with JetBrains decompiler
// Type: DataIntegratorASC.Objetos.Pedido
// Assembly: DataIntegratorASC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CACEFAEB-425A-4474-9813-A6D977B283DD
// Assembly location: C:\Users\morat\OneDrive\Escritorio\DataIntegratorASC.exe

using System;
using System.Collections.Generic;

namespace DataIntegratorASC.Objetos
{
  public class Pedido : BaseObjeto
  {
    private string _sCardCode = string.Empty;
    private string _sSerie = string.Empty;
    private string _sPedido = string.Empty;
    private string _sEstado = string.Empty;
    private string _sFechaPedido = string.Empty;
    private string _sNumPedido = string.Empty;
    private string _sOrdenCompra = string.Empty;
    private string _sFechaOrden = string.Empty;
    private string _sComentarios = string.Empty;
    private string _sClaveDirEmbarque = string.Empty;
    private string _sDireccionFactura = string.Empty;
    private string _sRubro1 = string.Empty;
    private string _sRubro2 = string.Empty;
    private string _sRubro3 = string.Empty;
    private string _sRubro4 = string.Empty;
    private string _sMoneda = string.Empty;
    private int _iCliente = 0;
    private string _sPais = string.Empty;
    private Decimal _dBaseImpuesto1 = 0M;
    private string _sNombreCliente = string.Empty;
    private string _sRFC = string.Empty;
    private string _sBASE = string.Empty;
    private bool _IsIvaExempt = false;
    private List<LineasPedido> _olsLineas = new List<LineasPedido>();
    private string _Site = string.Empty;
    private string _sProyectoH = string.Empty;

    public string sCardCode
    {
      set => this._sCardCode = value;
      get => this._sCardCode;
    }

    public string sSerie
    {
      set => this._sSerie = value;
      get => this._sSerie;
    }

    public string sPedido
    {
      set => this._sPedido = value;
      get => this._sPedido;
    }

    public string sEstado
    {
      set => this._sEstado = value;
      get => this._sEstado;
    }

    public string sFechaPedido
    {
      set => this._sFechaPedido = value;
      get => this._sFechaPedido;
    }

    public string sNumPedido
    {
      set => this._sNumPedido = value;
      get => this._sNumPedido;
    }

    public string sOrdenCompra
    {
      set => this._sOrdenCompra = value;
      get => this._sOrdenCompra;
    }

    public string sFechaOrden
    {
      set => this._sFechaOrden = value;
      get => this._sFechaOrden;
    }

    public string sComentarios
    {
      set => this._sComentarios = value;
      get => this._sComentarios;
    }

    public string sClaveDirEmbarque
    {
      set => this._sClaveDirEmbarque = value;
      get => this._sClaveDirEmbarque;
    }

    public string sDireccionFactura
    {
      set => this._sDireccionFactura = value;
      get => this._sDireccionFactura;
    }

    public string sRubro1
    {
      set => this._sRubro1 = value;
      get => this._sRubro1;
    }

    public string sRubro2
    {
      set => this._sRubro2 = value;
      get => this._sRubro2;
    }

    public string sRubro3
    {
      set => this._sRubro3 = value;
      get => this._sRubro3;
    }

    public string sRubro4
    {
      set => this._sRubro4 = value;
      get => this._sRubro4;
    }

    public string sMoneda
    {
      set => this._sMoneda = value;
      get => this._sMoneda;
    }

    public int iCliente
    {
      set => this._iCliente = value;
      get => this._iCliente;
    }

    public string sPais
    {
      set => this._sPais = value;
      get => this._sPais;
    }

    public Decimal dBaseImpuesto1
    {
      set => this._dBaseImpuesto1 = value;
      get => this._dBaseImpuesto1;
    }

    public string sNombreCliente
    {
      set => this._sNombreCliente = value;
      get => this._sNombreCliente;
    }

    public string sRFC
    {
      set => this._sRFC = value;
      get => this._sRFC;
    }

    public string sBASE
    {
      set => this._sBASE = value;
      get => this._sBASE;
    }

    public bool IsIvaExempt
    {
      set => this._IsIvaExempt = value;
      get => this._IsIvaExempt;
    }

    public List<LineasPedido> olsLineas
    {
      set => this._olsLineas = value;
      get => this._olsLineas;
    }

    public string Site
    {
      set => this._Site = value;
      get => this._Site;
    }

    public string sProyectoH
    {
      set => this._sProyectoH = value;
      get => this._sProyectoH;
    }
  }
}
